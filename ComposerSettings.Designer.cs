﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DefinitionComposer {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    internal sealed partial class ComposerSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static ComposerSettings defaultInstance = ((ComposerSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ComposerSettings())));
        
        public static ComposerSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string OwnerId {
            get {
                return ((string)(this["OwnerId"]));
            }
            set {
                this["OwnerId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DefinitionUnderTestPath {
            get {
                return ((string)(this["DefinitionUnderTestPath"]));
            }
            set {
                this["DefinitionUnderTestPath"] = value;
            }
        }
    }
}
