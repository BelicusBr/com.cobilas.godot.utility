using Godot;

namespace Cobilas.GodotEngine.Utility;

public static class GDFeature {
    /// <summary>Running on a release build.</summary>
    public static bool HasRelease => OS.HasFeature("release");

    /// <summary>Running on a debug build (including the editor).</summary>
    public static bool HasDebug => OS.HasFeature("debug");

    /// <summary>Running on an editor build.</summary>
    public static bool HasEditor => OS.HasFeature("editor");

    /// <summary>Running on a non-editor build.</summary>
    public static bool HasStandalone => OS.HasFeature("standalone");
    
    /// <summary>Running on the headless server platform.</summary>
    public static bool HasServer => OS.HasFeature("Server");

    /// <summary>Running on X11 (Linux/BSD desktop).</summary>
    public static bool HasX11 => OS.HasFeature("X11");
    
    /// <summary>Running on Windows.</summary>
    public static bool HasWindows => OS.HasFeature("Windows");
    
    /// <summary>Running on UWP.</summary>
    public static bool HasUWP => OS.HasFeature("UWP");
    
    /// <summary>Running on iOS.</summary>
    public static bool HasIOS => OS.HasFeature("iOS");
    
    /// <summary>Running on macOS.</summary>
    public static bool HasOSX => OS.HasFeature("OSX");
    
    /// <summary>JavaScript singleton is available.</summary>
    public static bool HasJavaScript => OS.HasFeature("JavaScript");
    
    /// <summary>Running on HTML5.</summary>
    public static bool HasHTML5 => OS.HasFeature("HTML5");
    
    /// <summary>Running on Android.</summary>
    public static bool HasAndroid => OS.HasFeature("Android");
    
    /// <summary>Running on a 32-bit build (any architecture).</summary>
    public static bool HasX32 => OS.HasFeature("32");
    
    /// <summary>Running on a 64-bit build (any architecture).</summary>
    public static bool HasX64 => OS.HasFeature("64");
    
    /// <summary>Running on a 32-bit x86 build.</summary>
    public static bool HasX86_32 => OS.HasFeature("x86");
    
    /// <summary>Running on a 64-bit x86 build.</summary>
    public static bool HasX86_64 => OS.HasFeature("x86_64");

    /// <summary>Running on a 64-bit ARM build.</summary>
    public static bool HasARM64 => OS.HasFeature("arm64");

    /// <summary>Running on a 32-bit ARM build.</summary>
    public static bool HasARM32 => OS.HasFeature("arm");
    
    /// <summary>Host OS is a mobile platform.</summary>
    public static bool HasMobile => OS.HasFeature("mobile");

    /// <summary>Host OS is a PC platform (desktop/laptop).</summary>
    public static bool HasPC => OS.HasFeature("pc");
    
    /// <summary>Host OS is a Web browser.</summary>
    public static bool HasWeb => OS.HasFeature("web");
    
    /// <summary>Textures using ETC1 compression are supported.</summary>
    public static bool HasETC1 => OS.HasFeature("etc");
    
    /// <summary>Textures using ETC2 compression are supported.</summary>
    public static bool HasETC2 => OS.HasFeature("etc2");
    
    /// <summary>Textures using S3TC (DXT/BC) compression are supported.</summary>
    public static bool HasS3TC => OS.HasFeature("s3tc");
    
    /// <summary>Textures using PVRTC compression are supported.</summary>
    public static bool HasPVRTC => OS.HasFeature("pvrtc");

    /// <summary>
    /// <para>Returns <c>true</c> if the feature for the given feature tag is supported in the currently running instance, depending on the platform, build etc. Can be used to check whether you're currently running a debug build, on a certain platform or arch, etc. Refer to the <a href="$DOCS_URL/tutorials/export/feature_tags.html">Feature Tags</a> documentation for more details.</para>
    /// <para>Note: Tag names are case-sensitive.</para>
    /// </summary>
    public static bool HasFeature(string? tagName) => OS.HasFeature(tagName ?? string.Empty);
}