using Godot;
using System;
using System.IO;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Serialization.Json;

using IOFile = System.IO.File;
using IOPath = System.IO.Path;
using SYSEnvironment = System.Environment;

namespace Cobilas.GodotEngine.Utility; 

public static class Screen {
    private static ResolutionItem[] resolutions = System.Array.Empty<ResolutionItem>();

    public static ScreenMode Mode {
        get {
            if (OS.WindowResizable) return ScreenMode.Resizable;
            if (OS.WindowBorderless) return ScreenMode.Borderless;
            return ScreenMode.Fullscreen;
        }
        set {
            OS.WindowResizable = value == ScreenMode.Resizable;
            OS.WindowBorderless = value == ScreenMode.Borderless;
            OS.WindowFullscreen = value == ScreenMode.Fullscreen;
        }
    }
    public static Vector2 CurrentResolution => OS.WindowSize;
    public static Vector2[] Resolutions {
        get {
            Vector2[] list = new Vector2[ArrayManipulation.ArrayLength(resolutions)];
            for (int I = 0; I < list.Length; I++)
                list[I] = resolutions[I].Size;
            return list;
        }
    }

    static Screen() {
        using (GDDirectory directory = GDDirectory.GetGDDirectory()!) {
            using GDFile file = directory.GetFile("AddResolution.json")!;
            if (file != null)
                resolutions = Json.Deserialize<ResolutionItem[]>(file.Read());
        }
        using (GDDirectory directory = GDDirectory.GetGDDirectory(SYSEnvironment.CurrentDirectory)!) {
            using GDFile file = directory.GetFile("AddResolution.json")!;
            if (file != null)
                resolutions = ArrayManipulation.Add(Json.Deserialize<ResolutionItem[]>(file.Read()), resolutions);
        }
    }

    public static void AddResolution(int width, int height) {
        ArrayManipulation.Add(new ResolutionItem(width, height), ref resolutions);
        string filePath = IOPath.Combine(SYSEnvironment.CurrentDirectory, "AddResolution.json");
        using FileStream stream = IOFile.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write);
        stream.SetLength(0L);
        stream.Write(Json.Serialize(resolutions), Encoding.UTF8);
    }

    public static void SetResolution(Vector2 size, ScreenMode mode) {
        OS.WindowSize = size;
        Mode = mode;
    }

    public static void SetResolution(int width, int height, ScreenMode mode)
        => SetResolution(new Vector2(width, height), mode);

    public static void SetResolution(Vector2 size)
        => SetResolution(size, Mode);

    public static void SetResolution(int width, int height)
        => SetResolution(width, height, Mode);
    
    [Serializable] 
    private struct ResolutionItem {
        private int width;
        private int height;

        public readonly Vector2 Size => new(width, height);
        public int Width { readonly get => width; set => width = value; }
        public int Height { readonly get => height; set => height = value; }

        public ResolutionItem(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public ResolutionItem(Vector2 resolution) : this((int)resolution.x, (int)resolution.y) {}
    }
}