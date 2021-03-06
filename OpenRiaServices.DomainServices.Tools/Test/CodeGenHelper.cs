﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenRiaServices.DomainServices.Server.Test.Utilities;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using OpenRiaServices.DomainServices.Tools.SharedTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace OpenRiaServices.DomainServices.Tools.Test
{

    public static class CodeGenHelper
    {
        // Computed list of SL framework and SDK paths
        private static List<string> silverlightPaths;

        /// <summary>
        /// Gets the Silverlight Version to be used for unit tests.
        /// </summary>
        /// <returns>The version of Silverlight to be tested.</returns>
        internal static decimal SilverlightVersionToTest
        {
            get
            {
                return 5.0m;
            }
        }

        public static void AssertGenerated(string generatedCode, string expected)
        {
            string normalizedGenerated = TestHelper.NormalizeWhitespace(generatedCode);
            string normalizedExpected = TestHelper.NormalizeWhitespace(expected);
            Assert.IsTrue(normalizedGenerated.IndexOf(normalizedExpected) >= 0, "Expected <" + expected + "> but saw\r\n<" + generatedCode + ">");
        }

        public static void AssertNotGenerated(string generatedCode, string notExpected)
        {
            string normalizedGenerated = TestHelper.NormalizeWhitespace(generatedCode);
            string normalizedNotExpected = TestHelper.NormalizeWhitespace(notExpected);
            Assert.IsTrue(normalizedGenerated.IndexOf(normalizedNotExpected) < 0, "Did not expect <" + notExpected + ">");
        }

        /// <summary>
        /// Assert that all the files named by shortNames exist in the files collection
        /// </summary>
        /// <param name="files">list of files to check</param>
        /// <param name="projectPath">project owning the short named files</param>
        /// <param name="shortNames">collection of file short names that must be in list</param>
        public static void AssertContainsOnlyFiles(List<string> files, string projectPath, string[] shortNames)
        {
            Assert.AreEqual(shortNames.Length, files.Count);
            AssertContainsFiles(files, projectPath, shortNames);
        }

        /// <summary>
        /// Assert that all the files named by shortNames exist in the files collection
        /// </summary>
        /// <param name="files">list of files to check</param>
        /// <param name="projectPath">project owning the short named files</param>
        /// <param name="shortNames">collection of file short names that must be in list</param>
        public static void AssertContainsFiles(List<string> files, string projectPath, string[] shortNames)
        {
            foreach (string shortName in shortNames)
            {
                string fullName = Path.Combine(Path.GetDirectoryName(projectPath), shortName);
                bool foundIt = false;
                foreach (string file in files)
                {
                    if (file.Equals(fullName, StringComparison.OrdinalIgnoreCase))
                    {
                        foundIt = true;
                        break;
                    }
                }
                if (!foundIt)
                {
                    string allFiles = string.Empty;
                    foreach (string file in files)
                        allFiles += ("\r\n" + file);

                    Assert.Fail("Expected to find " + fullName + " in list of files, but saw instead:" + allFiles);
                }
            }
        }

        public static string GetOutputFile(ITaskItem[] items, string shortName)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                if (Path.GetFileName(items[i].ItemSpec).Equals(shortName, StringComparison.OrdinalIgnoreCase))
                {
                    string fileName = items[i].ItemSpec;
                    Assert.IsTrue(File.Exists(fileName), "Expected file " + fileName + " to have been created.");
                    return fileName;
                }
            }
            Assert.Fail("Expected to find output file " + shortName);
            return null;
        }

        /// <summary>
        /// Returns the name of the assembly built by the server project
        /// </summary>
        /// <param name="serverProjectPath"></param>
        /// <returns></returns>
        public static string ServerClassLibOutputAssembly(string serverProjectPath)
        {
            // We need to map any server side assembly references back to our deployment directory
            // if we have the same assembly there, otherwise the assembly load from calls end up
            // with multiple assemblies with the same types
            string deploymentDir = Path.GetDirectoryName(typeof(CodeGenHelper).Assembly.Location);
            string assembly = MsBuildHelper.GetOutputAssembly(serverProjectPath);
            return MapAssemblyReferenceToDeployment(deploymentDir, assembly);
        }

        /// <summary>
        /// Returns the collection of assembly references from the server project
        /// </summary>
        /// <param name="serverProjectPath"></param>
        /// <returns></returns>
        public static List<string> ServerClassLibReferences(string serverProjectPath)
        {
            // We need to map any server side assembly references back to our deployment directory
            // if we have the same assembly there, otherwise the assembly load from calls end up
            // with multiple assemblies with the same types
            string deploymentDir = Path.GetDirectoryName(typeof(CodeGenHelper).Assembly.Location);
            List<string> assemblies = MsBuildHelper.GetReferenceAssemblies(serverProjectPath);
            MapAssemblyReferencesToDeployment(deploymentDir, assemblies);
            return assemblies;
        }

        /// <summary>
        /// Returns the collection of source files from the server
        /// </summary>
        /// <param name="serverProjectPath"></param>
        /// <returns></returns>
        public static List<string> ServerClassLibSourceFiles(string serverProjectPath)
        {
            return MsBuildHelper.GetSourceFiles(serverProjectPath);
        }

        /// <summary>
        /// Returns the collection of assembly references from the client project
        /// </summary>
        /// <param name="clientProjectPath"></param>
        /// <returns></returns>
        public static List<string> ClientClassLibReferences(string clientProjectPath, bool includeClientOutputAssembly)
        {
            List<string> references = MsBuildHelper.GetReferenceAssemblies(clientProjectPath);

            // Note: we conditionally add the output assembly to enable this unit test to
            // define some shared types 
            if (includeClientOutputAssembly)
            {
                references.Add(MsBuildHelper.GetOutputAssembly(clientProjectPath));
            }

            // Remove mscorlib -- it causes problems using ReflectionOnlyLoad ("parent does not exist")
            for (int i = 0; i < references.Count; ++i)
            {
                if (Path.GetFileName(references[i]).Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase))
                {
                    references.RemoveAt(i);
                    break;
                }
            }
            return references;
        }

        /// <summary>
        /// Returns the collection of source files from the client
        /// </summary>
        /// <param name="clientProjectPath"></param>
        /// <returns></returns>
        public static List<string> ClientClassLibSourceFiles(string clientProjectPath)
        {
            return MsBuildHelper.GetSourceFiles(clientProjectPath);
        }


        /// <summary>
        /// Returns the full path of the server project based on our current project
        /// </summary>
        /// <param name="currentProjectPath"></param>
        /// <returns></returns>
        public static string ServerClassLibProjectPath(string currentProjectPath)
        {
            return Path.Combine(Path.GetDirectoryName(currentProjectPath), @"ServerClassLib\ServerClassLib.csproj");
        }

        /// <summary>
        /// Returns the full path of the server WAP project based on our current project
        /// </summary>
        /// <param name="currentProjectPath"></param>
        /// <returns></returns>
        public static string ServerWapProjectPath(string currentProjectPath)
        {
            return Path.Combine(Path.GetDirectoryName(currentProjectPath), @"TestWAP\TestWAP.csproj");
        }

        /// <summary>
        /// Returns the full path of the 2nd server project based on our current project
        /// </summary>
        /// <param name="currentProjectPath"></param>
        /// <returns></returns>
        public static string ServerClassLib2ProjectPath(string currentProjectPath)
        {
            return Path.Combine(Path.GetDirectoryName(currentProjectPath), @"ServerClassLib2\ServerClassLib2.csproj");
        }

        /// <summary>
        /// Returns the full path of the client project based on our current project
        /// </summary>
        /// <param name="currentProjectPath"></param>
        /// <returns></returns>
        public static string ClientClassLibProjectPath(string currentProjectPath)
        {
            return Path.Combine(Path.GetDirectoryName(currentProjectPath), @"ClientClassLib\ClientClassLib.csproj");
        }

        /// <summary>
        /// Returns the full path of the 2nd client project based on our current project
        /// </summary>
        /// <param name="currentProjectPath"></param>
        /// <returns></returns>
        public static string ClientClassLib2ProjectPath(string currentProjectPath)
        {
            return Path.Combine(Path.GetDirectoryName(currentProjectPath), @"ClientClassLib2\ClientClassLib2.csproj");
        }

        /// <summary>
        /// When running unit tests, assemblies we are analyzing may come from one place,
        /// but VSTT has copied a version locally that we are running.  This will cause
        /// confusion, so map all assembly references that have a local equivalent to
        /// that local version.
        /// </summary>
        /// <param name="referenceAssemblies"></param>
        public static void MapAssemblyReferencesToDeployment(string deploymentDir, IList<string> assemblies)
        {
            for (int i = 0; i < assemblies.Count; ++i)
            {
                assemblies[i] = MapAssemblyReferenceToDeployment(deploymentDir, assemblies[i]);
            }
        }

        public static string MapAssemblyReferenceToDeployment(string deploymentDir, string assembly)
        {
            string localPath = Path.Combine(deploymentDir, Path.GetFileName(assembly));
            if (File.Exists(localPath))
            {
                assembly = localPath;
            }
            return assembly;
        }

        internal static SharedCodeService CreateSharedCodeService(string clientProjectPath, ILoggingService logger)
        {
            return CodeGenHelper.CreateSharedCodeService(clientProjectPath, logger, CodeGenHelper.SilverlightVersionToTest);
        }

        internal static SharedCodeService CreateSharedCodeService(string clientProjectPath, ILoggingService logger, decimal silverlightVersion)
        {
            List<string> sourceFiles = CodeGenHelper.ClientClassLibSourceFiles(clientProjectPath);
            List<string> assemblies = CodeGenHelper.ClientClassLibReferences(clientProjectPath, true);

            SharedCodeServiceParameters parameters = new SharedCodeServiceParameters()
            {
                SharedSourceFiles = sourceFiles.ToArray(),
                ClientAssemblies = assemblies.ToArray(),
                ClientAssemblyPathsNormalized = CodeGenHelper.GetSilverlightPaths(silverlightVersion).ToArray()
            };

            SharedCodeService sts = new SharedCodeService(parameters, logger);
            return sts;
        }

        /// <summary>
        /// Generate a temporary folder for generating code
        /// </summary>
        /// <returns></returns>
        public static string GenerateTempFolder()
        {
            string rootPath = Path.GetTempPath();
            string tempFolder = Path.Combine(rootPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

        /// <summary>
        /// Delete the temporary folder provided by GenerateTempFolder
        /// </summary>
        /// <param name="tempFolder"></param>
        public static void DeleteTempFolder(string tempFolder)
        {
            try
            {
                if (tempFolder.StartsWith(Path.GetTempPath()))
                {
                    RecursiveDelete(tempFolder);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                System.Diagnostics.Debug.WriteLine("Failed to delete temp folder: " + tempFolder);
            }
        }

        /// <summary>
        /// Deletes all the files and folders created by the given CreteRiaClientFilesTask
        /// </summary>
        /// <param name="task"></param>
        public static void DeleteTempFolder(CreateOpenRiaClientFilesTask task)
        {
            if (task != null)
            {
                string tempFolder = Path.GetDirectoryName(task.OutputPath);
                DeleteTempFolder(tempFolder);
            }
        }

        /// <summary>
        /// Deletes the given folder and everything inside it
        /// </summary>
        /// <param name="dir"></param>
        public static void RecursiveDelete(string dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                return;
            }
            //get all the subdirectories in the given directory
            string[] dirs = Directory.GetDirectories(dir);
            for (int i = 0; i < dirs.Length; i++)
            {
                RecursiveDelete(dirs[i]);
            }
            string[] files = Directory.GetFiles(dir);

            foreach (string file in files)
            {
                FileInfo fInfo = new FileInfo(file);
                fInfo.Attributes &= ~(FileAttributes.ReadOnly);
                File.Delete(file);
            }

            Directory.Delete(dir);
        }

        /// <summary>
        /// Creates a new CreateOpenRiaClientFilesTask instance to use to generate code
        /// </summary>
        /// <param name="relativeTestDir"></param>
        /// <param name="includeClientOutputAssembly">if <c>true</c> include clients own output assembly in analysis</param>
        /// <returns>A new task instance that can be invoked to do code gen</returns>
        public static CreateOpenRiaClientFilesTask CreateOpenRiaClientFilesTaskInstance(string relativeTestDir, bool includeClientOutputAssembly)
        {
            string deploymentDir = Path.GetDirectoryName(typeof(CodeGenHelper).Assembly.Location);
            string projectPath = null;
            string outputPath = null;
            TestHelper.GetProjectPaths(relativeTestDir, out projectPath, out outputPath);

            Assert.IsTrue(File.Exists(projectPath), "Could not locate " + projectPath + " necessary for test.");
            string serverProjectPath = CodeGenHelper.ServerClassLibProjectPath(projectPath);
            string clientProjectPath = CodeGenHelper.ClientClassLibProjectPath(projectPath);

            return CodeGenHelper.CreateOpenRiaClientFilesTaskInstance(serverProjectPath, clientProjectPath, includeClientOutputAssembly);
        }

        /// <summary>
        /// Creates a new CreateOpenRiaClientFilesTask instance to use to generate code
        /// </summary>
        /// <param name="serverProjectPath">The file path to the ASP.NET server project</param>
        /// <param name="clientProjectPath">The file path to the Silverlight client project</param>
        /// <param name="includeClientOutputAssembly">if <c>true</c> include client's own output assembly in analysis</param>
        /// <returns>A new task instance that can be invoked to do code gen</returns>
        public static CreateOpenRiaClientFilesTask CreateOpenRiaClientFilesTaskInstance(string serverProjectPath, string clientProjectPath, bool includeClientOutputAssembly)
        {
            CreateOpenRiaClientFilesTask task = new CreateOpenRiaClientFilesTask();

            MockBuildEngine mockBuildEngine = new MockBuildEngine();
            task.BuildEngine = mockBuildEngine;

            task.Language = "C#";

            task.ServerProjectPath = serverProjectPath;
            task.ServerAssemblies = new TaskItem[] { new TaskItem(CodeGenHelper.ServerClassLibOutputAssembly(task.ServerProjectPath)) };
            task.ServerReferenceAssemblies = MsBuildHelper.AsTaskItems(CodeGenHelper.ServerClassLibReferences(task.ServerProjectPath)).ToArray();

            task.ClientProjectPath = clientProjectPath;
            task.ClientReferenceAssemblies = MsBuildHelper.AsTaskItems(CodeGenHelper.ClientClassLibReferences(clientProjectPath, includeClientOutputAssembly)).ToArray();
            task.ClientSourceFiles = MsBuildHelper.AsTaskItems(CodeGenHelper.ClientClassLibSourceFiles(clientProjectPath)).ToArray();
            task.ClientFrameworkPath = CodeGenHelper.GetSilverlightRuntimeDirectory();

            // Generate the code to our deployment directory
            string tempFolder = CodeGenHelper.GenerateTempFolder();
            task.OutputPath = Path.Combine(tempFolder, "FileWrites");
            task.GeneratedCodePath = Path.Combine(tempFolder, "Generated_Code");

            return task;
        }

        /// <summary>
        /// Creates a new CreateOpenRiaClientFilesTask instance to use to generate code
        /// </summary>
        /// <param name="relativeTestDir"></param>
        /// <param name="includeClientOutputAssembly">if <c>true</c> include clients own output assembly in analysis</param>
        /// <returns>A new task instance that can be invoked to do code gen</returns>
        public static CreateOpenRiaClientFilesTask CreateOpenRiaClientFilesTaskInstance_CopyClientProjectToOutput(string relativeTestDir, bool includeClientOutputAssembly)
        {
            string deploymentDir = Path.GetDirectoryName(typeof(CodeGenHelper).Assembly.Location);
            string projectPath = null;
            string outputPath = null;
            TestHelper.GetProjectPaths(relativeTestDir, out projectPath, out outputPath);

            Assert.IsTrue(File.Exists(projectPath), "Could not locate " + projectPath + " necessary for test.");
            string serverProjectPath = CodeGenHelper.ServerClassLibProjectPath(projectPath);
            string clientProjectPath = CodeGenHelper.ClientClassLibProjectPath(projectPath);

            return CodeGenHelper.CreateOpenRiaClientFilesTaskInstance_CopyClientProjectToOutput(serverProjectPath, clientProjectPath, includeClientOutputAssembly);
        }

        /// <summary>
        /// Creates a new CreateOpenRiaClientFilesTask instance to use to generate code
        /// </summary>
        /// <param name="relativeTestDir"></param>
        /// <param name="includeClientOutputAssembly">if <c>true</c> include clients own output assembly in analysis</param>
        /// <returns>A new task instance that can be invoked to do code gen</returns>
        public static CreateOpenRiaClientFilesTask CreateOpenRiaClientFilesTaskInstance_CopyClientProjectToOutput(string serverProjectPath, string clientProjectPath, bool includeClientOutputAssembly)
        {
            CreateOpenRiaClientFilesTask task = new CreateOpenRiaClientFilesTask();

            MockBuildEngine mockBuildEngine = new MockBuildEngine();
            task.BuildEngine = mockBuildEngine;

            task.Language = "C#";

            task.ServerProjectPath = serverProjectPath;
            task.ServerAssemblies = new TaskItem[] { new TaskItem(CodeGenHelper.ServerClassLibOutputAssembly(task.ServerProjectPath)) };
            task.ServerReferenceAssemblies = MsBuildHelper.AsTaskItems(CodeGenHelper.ServerClassLibReferences(task.ServerProjectPath)).ToArray();
            task.ClientFrameworkPath = CodeGenHelper.GetSilverlightRuntimeDirectory();

            // Generate the code to our deployment directory
            string tempFolder = CodeGenHelper.GenerateTempFolder();
            task.OutputPath = Path.Combine(tempFolder, "FileWrites");
            task.GeneratedCodePath = Path.Combine(tempFolder, "Generated_Code");

            string clientProjectFileName = Path.GetFileName(clientProjectPath);
            string clientProjectDestPath = Path.Combine(tempFolder, clientProjectFileName);
            File.Copy(clientProjectPath, clientProjectDestPath);
            task.ClientProjectPath = clientProjectDestPath;
            task.ClientReferenceAssemblies = MsBuildHelper.AsTaskItems(CodeGenHelper.ClientClassLibReferences(clientProjectPath, includeClientOutputAssembly)).ToArray();
            task.ClientSourceFiles = MsBuildHelper.AsTaskItems(CodeGenHelper.ClientClassLibSourceFiles(clientProjectPath)).ToArray();

            return task;
        }

        /// <summary>
        /// Creates a new CreateOpenRiaClientFilesTask instance to use to generate code
        /// using the TestWap project.
        /// </summary>
        /// <param name="relativeTestDir"></param>
        /// <returns>A new task instance that can be invoked to do code gen</returns>
        public static CreateOpenRiaClientFilesTask CreateOpenRiaClientFilesTaskInstanceForWAP(string relativeTestDir)
        {
            string deploymentDir = Path.GetDirectoryName(typeof(CodeGenHelper).Assembly.Location);
            string projectPath = null;
            string outputPath = null;
            TestHelper.GetProjectPaths(relativeTestDir, out projectPath, out outputPath);

            Assert.IsTrue(File.Exists(projectPath), "Could not locate " + projectPath + " necessary for test.");
            string serverProjectPath = CodeGenHelper.ServerWapProjectPath(projectPath);
            string clientProjectPath = CodeGenHelper.ClientClassLibProjectPath(projectPath);

            return CodeGenHelper.CreateOpenRiaClientFilesTaskInstance(serverProjectPath, clientProjectPath, false);
        }

        /// <summary>
        /// Creates a new <see cref="ValidateDomainServicesTask"/> instance
        /// </summary>
        /// <param name="relativeTestDir">The relative output directory of the test</param>
        /// <returns>A new <see cref="ValidateDomainServicesTask"/> instance</returns>
        public static ValidateDomainServicesTask CreateValidateDomainServicesTask(string relativeTestDir)
        {
            string deploymentDir = Path.GetDirectoryName(typeof(CodeGenHelper).Assembly.Location);
            string projectPath = null;
            string outputPath = null;
            TestHelper.GetProjectPaths(relativeTestDir, out projectPath, out outputPath);

            Assert.IsTrue(File.Exists(projectPath), "Could not locate " + projectPath + " necessary for test.");
            string serverProjectPath = CodeGenHelper.ServerClassLibProjectPath(projectPath);

            ValidateDomainServicesTask task = new ValidateDomainServicesTask();

            MockBuildEngine mockBuildEngine = new MockBuildEngine();
            task.BuildEngine = mockBuildEngine;

            task.ProjectPath = serverProjectPath;
            task.Assembly = new TaskItem(CodeGenHelper.ServerClassLibOutputAssembly(task.ProjectPath));
            task.ReferenceAssemblies = MsBuildHelper.AsTaskItems(CodeGenHelper.ServerClassLibReferences(task.ProjectPath)).ToArray();

            return task;
        }

        /// <summary>
        /// Gets the full path of the Silverlight runtime framework folder.
        /// </summary>
        /// <returns>The Silverlight platform runtime folder or null if it cannot be found.</returns>
        public static string GetSilverlightRuntimeDirectory()
        {
            return CodeGenHelper.GetSilverlightRuntimeDirectory(CodeGenHelper.SilverlightVersionToTest);
        }

        /// <summary>
        /// Gets the full path of the Silverlight runtime framework folder.
        /// </summary>
        /// <param name="silverlightVersion">The version of Silverlight to target for code gen</param>
        /// <returns>The Silverlight platform runtime folder or null if it cannot be found.</returns>
        public static string GetSilverlightRuntimeDirectory(decimal silverlightVersion)
        {
            string runtimePath = null;
            try
            {
                string name = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"Software\Microsoft\Microsoft SDKs\Silverlight\v{0:0.0}\ReferenceAssemblies", silverlightVersion);
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(name))
                {
                    if (key != null)
                    {
                        runtimePath = (string)key.GetValue("SLRuntimeInstallPath");
                    }
                    return runtimePath;
                }
            }
            catch (System.Security.SecurityException)
            {
            }
            return runtimePath;
        }

        /// <summary>
        /// Gets the full path of the Silverlight SDK folder.
        /// This implementation is taken verbatim from Microsoft.Silverlight.Build.Tasks
        /// and is what normally set $(TargetFrameworkSDKDirectory)
        /// during a build of a Silverlight project
        /// </summary>
        /// <returns>The Silverlight SDK folder or null if it cannot be found.</returns>
        public static string GetSilverlightSDKDirectory()
        {
            return CodeGenHelper.GetSilverlightSDKDirectory(CodeGenHelper.SilverlightVersionToTest);
        }

        /// <summary>
        /// Gets the full path of the Silverlight SDK folder.
        /// This implementation is taken verbatim from Microsoft.Silverlight.Build.Tasks
        /// and is what normally set $(TargetFrameworkSDKDirectory)
        /// during a build of a Silverlight project
        /// </summary>
        /// <param name="silverlightVersion">The version of Silverlight to target for code gen</param>
        /// <returns>The Silverlight SDK folder or null if it cannot be found.</returns>
        public static string GetSilverlightSDKDirectory(decimal silverlightVersion)
        {
            string sdkPath = null;
            try
            {
                string name = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"Software\Microsoft\Microsoft SDKs\Silverlight\v{0:0.0}\AssemblyFoldersEx\Silverlight SDK Client Libraries", silverlightVersion);
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(name))
                {
                    if (key != null)
                    {
                        string keyValue = (string)key.GetValue(null);
                        if (!string.IsNullOrEmpty(keyValue) && Directory.Exists(keyValue))
                        {
                            sdkPath = keyValue;
                        }
                    }
                    return sdkPath;
                }
            }
            catch (System.Security.SecurityException)
            {
            }
            return sdkPath;
        }

        /// <summary>
        /// Returns the set of Silverlight runtime and SDK paths
        /// </summary>
        /// <returns>The set of Silverlight runtime and SDK paths</returns>
        public static IEnumerable<string> GetSilverlightPaths()
        {
            return CodeGenHelper.GetSilverlightPaths(CodeGenHelper.SilverlightVersionToTest);
        }

        /// <summary>
        /// Returns the set of Silverlight runtime and SDK paths
        /// </summary>
        /// <param name="silverlightVersion">The version of Silverlight to target for code gen</param>
        /// <returns>The set of Silverlight runtime and SDK paths</returns>
        public static IEnumerable<string> GetSilverlightPaths(decimal silverlightVersion)
        {
            if (CodeGenHelper.silverlightPaths == null)
            {
                CodeGenHelper.silverlightPaths = new List<string>();
                string s = GetSilverlightRuntimeDirectory(silverlightVersion);
                if (!string.IsNullOrEmpty(s))
                {
                    CodeGenHelper.silverlightPaths.Add(s);
                }

                // The SDK path is optional -- okay to unit test without it
                s = GetSilverlightSDKDirectory(silverlightVersion);
                if (!string.IsNullOrEmpty(s) && Directory.Exists(s))
                {
                    CodeGenHelper.silverlightPaths.Add(s);
                }
            }
            return CodeGenHelper.silverlightPaths;
        }

        /// <summary>
        /// Basic success test. Method verifies that domain service compiles.
        /// </summary>
        /// <param name="domainServices">DomainService to compile</param>
        /// <param name="codeContains">strings that this code must contain, can be c>null</c>.</param>
        /// <param name="codeNotContains">strings that this code must not contain, can be <c>null</c>.</param>
        public static void BaseSuccessTest(Type[] domainServices, string[] codeContains, string[] codeNotContains)
        {
            MockSharedCodeService sts = TestHelper.CreateCommonMockSharedCodeService();
            ConsoleLogger logger = new ConsoleLogger();
            string generatedCode = TestHelper.GenerateCodeAssertSuccess("C#", domainServices, logger, sts);

            if (codeContains != null)
            {
                TestHelper.AssertGeneratedCodeContains(generatedCode, codeContains);
            }

            if (codeNotContains != null)
            {
                TestHelper.AssertGeneratedCodeDoesNotContain(generatedCode, codeNotContains);
            }
        }
    }
}
