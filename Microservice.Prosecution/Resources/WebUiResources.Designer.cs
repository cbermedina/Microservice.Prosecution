﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microservice.Prosecution.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class WebUiResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WebUiResources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microservice.Prosecution.Resources.WebUiResources", typeof(WebUiResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a This {0} extension is not allowed!, Only txt files allowed..
        /// </summary>
        public static string AllowTxt {
            get {
                return ResourceManager.GetString("AllowTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Identity document.
        /// </summary>
        public static string Document {
            get {
                return ResourceManager.GetString("Document", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a File.
        /// </summary>
        public static string InformationFile {
            get {
                return ResourceManager.GetString("InformationFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a ([0-9]+).
        /// </summary>
        public static string RegularExpressionValidNumber {
            get {
                return ResourceManager.GetString("RegularExpressionValidNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The maximum length of {0} is {1}..
        /// </summary>
        public static string Validations_MaxLength {
            get {
                return ResourceManager.GetString("Validations_MaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The {0} is required..
        /// </summary>
        public static string Validations_Required {
            get {
                return ResourceManager.GetString("Validations_Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Please enter valid Number.
        /// </summary>
        public static string ValidNumber {
            get {
                return ResourceManager.GetString("ValidNumber", resourceCulture);
            }
        }
    }
}
