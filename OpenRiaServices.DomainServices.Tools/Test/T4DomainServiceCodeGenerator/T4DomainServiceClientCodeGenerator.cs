﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 10.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace OpenRiaServices.DomainServices.Tools.Test.T4Generator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using OpenRiaServices.DomainServices.Server;
    using System.Text;
    using OpenRiaServices.DomainServices.Tools;
    
    
    #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\T4DomainServiceClientCodeGenerator.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class T4DomainServiceClientCodeGenerator : T4DomainServiceClientCodeGeneratorBase
    {
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
        public virtual string TransformText()
        {
            this.GenerationEnvironment = null;
            this.Write("// ");
            
            #line 16 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\T4DomainServiceClientCodeGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(T4DomainServiceClientCodeGenerator.GeneratedBoilerPlate));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 17 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\T4DomainServiceClientCodeGenerator.tt"
 this.GenerateAllDomainServiceDescriptionClasses(this.DomainServiceDescriptions); 
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        #line 18 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\T4DomainServiceClientCodeGenerator.tt"


public virtual void GenerateAllDomainServiceDescriptionClasses(IEnumerable<DomainServiceDescription> domainServiceDescriptions)
{
    foreach (DomainServiceDescription domainServiceDescription in domainServiceDescriptions)
    {
        this.GenerateDomainServiceDescriptionClasses(domainServiceDescription);
    }
}


        
        #line default
        #line hidden
        
        #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainServiceDescription.ttinclude"
 
public virtual void GenerateDomainServiceDescriptionClasses(DomainServiceDescription domainServiceDescription)
{
    this.GenerateDomainContextClass(domainServiceDescription);
    foreach (Type entityType in domainServiceDescription.EntityTypes)
    {
        this.GenerateEntityClass(entityType);
    }
}

        
        #line default
        #line hidden
        
        #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
 

public virtual void GenerateDomainContextClass(DomainServiceDescription domainServiceDescription)
{

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
this.Write("namespace ");

        
        #line default
        #line hidden
        
        #line 6 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(domainServiceDescription.DomainServiceType.Namespace));

        
        #line default
        #line hidden
        
        #line 6 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
this.Write("\r\n{\r\n    using System;\r\n    using System.Collections.Generic;\r\n    using System.C" +
        "omponentModel;\r\n    using System.ComponentModel.DataAnnotations;\r\n    using Syst" +
        "em.Data;\r\n    using System.Linq;\r\n\r\n    public class ");

        
        #line default
        #line hidden
        
        #line 15 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(domainServiceDescription.DomainServiceType.Name));

        
        #line default
        #line hidden
        
        #line 15 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
this.Write("Context : DomainContext\r\n    {\r\n    }\r\n}\r\n");

        
        #line default
        #line hidden
        
        #line 19 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\DomainContext.ttinclude"
 } 
        
        #line default
        #line hidden
        
        #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
 
public virtual void GenerateEntityDeclaration(Type entityType)
{

        
        #line default
        #line hidden
        
        #line 4 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write("    public class ");

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(entityType.Name));

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write(" : Entity");

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"

}

public virtual void GenerateEntityProperties(Type entityType)
{
    foreach (PropertyInfo propertyInfo in entityType.GetProperties())
    {
        this.GenerateEntityProperty(propertyInfo);
    }
}

public virtual void GenerateEntityBody(Type entityType)
{
    this.GenerateEntityProperties(entityType);
}

public virtual void GenerateEntityClass(Type entityType)
{

        
        #line default
        #line hidden
        
        #line 23 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write("namespace ");

        
        #line default
        #line hidden
        
        #line 24 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(entityType.Namespace));

        
        #line default
        #line hidden
        
        #line 24 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write("\r\n{\r\n");

        
        #line default
        #line hidden
        
        #line 26 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"

    this.GenerateEntityDeclaration(entityType);
    this.GenerateEntityBody(entityType);

        
        #line default
        #line hidden
        
        #line 29 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
this.Write("    }\r\n}\r\n");

        
        #line default
        #line hidden
        
        #line 32 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\Entity.ttinclude"
  
} 
        
        #line default
        #line hidden
        
        #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"

public virtual void GenerateEntityPropertyDeclaration(PropertyInfo propertyInfo)
{

        
        #line default
        #line hidden
        
        #line 4 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write("        public  ");

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(propertyInfo.PropertyType.Name));

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write(" ");

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(propertyInfo.Name));

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write(" ");

        
        #line default
        #line hidden
        
        #line 5 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"

}

public virtual void GenerateEntityPropertyGetter(PropertyInfo propertyInfo)
{

        
        #line default
        #line hidden
        
        #line 10 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write("get;");

        
        #line default
        #line hidden
        
        #line 10 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"

}

public virtual void GenerateEntityPropertySetter(PropertyInfo propertyInfo)
{

        
        #line default
        #line hidden
        
        #line 15 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write("set;");

        
        #line default
        #line hidden
        
        #line 15 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"

}

public virtual void GenerateEntityPropertyBody(PropertyInfo propertyInfo)
{

        
        #line default
        #line hidden
        
        #line 20 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write(" {");

        
        #line default
        #line hidden
        
        #line 20 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
 this.GenerateEntityPropertyGetter(propertyInfo); this.GenerateEntityPropertySetter(propertyInfo); 
        
        #line default
        #line hidden
        
        #line 20 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"
this.Write("}\r\n");

        
        #line default
        #line hidden
        
        #line 21 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\EntityProperty.ttinclude"

}

public virtual void GenerateEntityProperty(PropertyInfo propertyInfo) { 
   foreach (object customAttribute in propertyInfo.GetCustomAttributes(false) ) {
       this.GenerateCustomAttribute(customAttribute);
   }
   this.GenerateEntityPropertyDeclaration(propertyInfo);
   this.GenerateEntityPropertyBody(propertyInfo);
 } 
        
        #line default
        #line hidden
        
        #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\CustomAttribute.ttinclude"
 public virtual void GenerateCustomAttribute(object customAttribute) { 
        
        #line default
        #line hidden
        
        #line 1 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\CustomAttribute.ttinclude"
this.Write("        \r\n        [");

        
        #line default
        #line hidden
        
        #line 2 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\CustomAttribute.ttinclude"
this.Write(this.ToStringHelper.ToStringWithCulture(Utilities.AttributeShortName(customAttribute)));

        
        #line default
        #line hidden
        
        #line 2 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\CustomAttribute.ttinclude"
this.Write("]\r\n");

        
        #line default
        #line hidden
        
        #line 3 "c:\dd\Alex_AppFx\AppFx\RiaServices\Main\OpenRiaServices.DomainServices.Tools\Test\T4DomainServiceCodeGenerator\CustomAttribute.ttinclude"
 } 
        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public class T4DomainServiceClientCodeGeneratorBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
    }
    #endregion
}
