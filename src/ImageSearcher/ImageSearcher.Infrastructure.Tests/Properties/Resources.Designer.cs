﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageSearcher.Infrastructure.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImageSearcher.Infrastructure.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    &quot;photo&quot;: {
        ///        &quot;id&quot;: &quot;46482105201&quot;,
        ///        &quot;secret&quot;: &quot;3b1d0bcc23&quot;,
        ///        &quot;server&quot;: &quot;4867&quot;,
        ///        &quot;farm&quot;: 5,
        ///        &quot;dateuploaded&quot;: &quot;1545891052&quot;,
        ///        &quot;isfavorite&quot;: 0,
        ///        &quot;license&quot;: &quot;0&quot;,
        ///        &quot;safety_level&quot;: &quot;0&quot;,
        ///        &quot;rotation&quot;: 0,
        ///        &quot;owner&quot;: {
        ///            &quot;nsid&quot;: &quot;143537090@N02&quot;,
        ///            &quot;username&quot;: &quot;Flexible Negativity&quot;,
        ///            &quot;realname&quot;: &quot;&quot;,
        ///            &quot;location&quot;: &quot;&quot;,
        ///            &quot;iconserver&quot;: &quot;973&quot;,
        ///            &quot;iconfarm&quot;: 1,
        ///            &quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetInfoSampleResponse {
            get {
                return ResourceManager.GetString("GetInfoSampleResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    &quot;photos&quot;: {
        ///        &quot;page&quot;: 1,
        ///        &quot;pages&quot;: 1529,
        ///        &quot;perpage&quot;: 100,
        ///        &quot;total&quot;: &quot;152838&quot;,
        ///        &quot;photo&quot;: [
        ///            {
        ///                &quot;id&quot;: &quot;46482105201&quot;,
        ///                &quot;owner&quot;: &quot;143537090@N02&quot;,
        ///                &quot;secret&quot;: &quot;3b1d0bcc23&quot;,
        ///                &quot;server&quot;: &quot;4867&quot;,
        ///                &quot;farm&quot;: 5,
        ///                &quot;title&quot;: &quot;White Cat&quot;,
        ///                &quot;ispublic&quot;: 1,
        ///                &quot;isfriend&quot;: 0,
        ///                &quot;isfamily&quot;: 0
        ///            },
        ///            {
        ///                &quot;id&quot;: &quot;464820 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SearchSampleResponse {
            get {
                return ResourceManager.GetString("SearchSampleResponse", resourceCulture);
            }
        }
    }
}
