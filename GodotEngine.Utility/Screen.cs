using Godot;
using System;
using System.Text;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.IO.Serialization.Json;
using Cobilas.GodotEngine.Utility.IO;

namespace Cobilas.GodotEngine.Utility; 

/// <summary>Gets or changes game screen information.</summary>
public static class Screen {

    private readonly static Dictionary<Vector2, Resolution> _gameScreenResolution = [];

    /// <summary>Gets all detected screens.</summary>
    /// <remarks>This property will only return all screens that were detected from the start of the application,
    /// if another screen is connected during the execution of the application it will not be detected.</remarks>
    /// <returns>Returns all screens that were detected.</returns>
    public static DisplayInfo[] Displays { get; private set; }
    /// <summary>The current screen.</summary>
    /// <returns>Returns a <seealso cref="DisplayInfo"/> with the information of the current screen.</returns>
    public static DisplayInfo CurrentDisplay => GetDisplay(OS.CurrentScreen);
    /// <summary>Number of screens detected.</summary>
    /// <returns>Returns the number of screens detected when starting the application.</returns>
    public static int DisplayCount => ArrayManipulation.ArrayLength(Displays);
    /// <summary>This property contains all custom resolution lists that are not tied to a <seealso cref="DisplayInfo"/>.</summary>
    /// <returns>Returns lists of custom resolutions that are not tied to a <seealso cref="DisplayInfo"/>.</returns>
    public static CustonResolutionList[] OrphanList { get; private set; }

    /// <summary>Represents current game screen mode.</summary>
    /// <value>The current game screen mode.</value>
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
    /// <summary>The current frequency of the game screen.</summary>
    /// <returns>Returns the current game screen frequency as a floating point.</returns>
    public static float ScreenRefreshRate => OS.GetScreenRefreshRate();
    /// <summary>Represents the game screen resolutions.</summary>
    /// <returns>Returns all stored resolutions.</returns>
    public static Resolution[] Resolutions => GetDisplay(OS.CurrentScreen).Resolutions;
    /// <summary>The current resolution of the game screen.</summary>
    /// <returns>Returns the current resolution of the game screen as Vector2D.</returns>
    public static Resolution CurrentResolution => Mode == ScreenMode.Fullscreen ? CurrentDisplay.CurrentResolution : GetGameScreenResolution();

    static Screen() {
        Displays = Array.Empty<DisplayInfo>();
        OrphanList = Array.Empty<CustonResolutionList>();

        for (int I = 0; I < 5; I++) {
            OpenTK.DisplayDevice display = OpenTK.DisplayDevice.GetDisplay((OpenTK.DisplayIndex)I);
            if (display is not null) {
                DisplayInfo[]? temp = Displays;
                ArrayManipulation.Add(new DisplayInfo(I, display), ref temp);
                Displays = temp ?? [];
            }
        }
        using (Folder folder = Folder.CreateRes()) {
            Archive archive = folder.GetArchive("AddResolution.json");
            if (!archive.IsNull) {
                archive.Read(out string stg);
                AddResolution(Json.Deserialize<CustonResolutionList[]>(stg));
            }
        }

        if (GDFeature.HasRelease) {
            using Folder folder = Folder.Create(GodotPath.CurrentDirectory);
            Archive archive = folder.GetArchive("AddResolution.json");
            if (!archive.IsNull) {
                archive.Read(out string stg);
                AddResolution(Json.Deserialize<CustonResolutionList[]>(stg));
            }
        }
    }
    /// <summary>Add a custom resolution.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    public static void AddResolution(in float width, in float height)
        => AddResolution(width, height, (int)ScreenRefreshRate);
    /// <summary>Add a custom resolution.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    /// <param name="refreshRate">The refresh rate of the monitor.</param>
    public static void AddResolution(in float width, in float height, in int refreshRate) {
        DisplayInfo display = CurrentDisplay;
        Displays[GetIndexDisplay(OS.CurrentScreen)] = display =
            DisplayInfo.AddCustonResolution(new(new Numerics.Vector2D(width, height), refreshRate), display);
        
        using Folder folder = Folder.CreateRes();
        Archive archive = folder.GetArchive("AddResolution.json");
        if (archive.IsNull) archive = folder.CreateArchive("AddResolution.json");
        archive.ReplaceBuffer(Encoding.UTF8.GetBytes(Json.Serialize(Array.ConvertAll(Displays, d => d.CustonResolutions), true)));
        archive.Flush();
    }
    /// <summary>Defines which screen will be used.</summary>
    /// <param name="index">The target index of the screen.</param>
    public static void SetCurrentDisplay(in int index) => OS.CurrentScreen = index;
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="size">The size of the screen.</param>
    /// <param name="mode">Screen display mode.</param>
    /// <param name="refreshRate">The refresh rate of the monitor.</param>
    /// <exception cref="ArgumentException">description</exception>
    public static void SetResolution(in Vector2 size, in int refreshRate, in ScreenMode mode) {
        DisplayInfo display = CurrentDisplay;
        Resolution temp = new(size, refreshRate);
        if ((Mode = mode) == ScreenMode.Fullscreen) {
            if (!display.Contains(temp))
                throw new ArgumentException($"This resolution '{temp}' does not exist.");
        }
        Displays[GetIndexDisplay(OS.CurrentScreen)] = DisplayInfo.ChangeCurrentResolution(temp, display);
        OS.WindowSize = size;
        Engine.TargetFps = refreshRate;
    }
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="size">The size of the screen.</param>
    /// <param name="mode">Screen display mode.</param>
    public static void SetResolution(in Vector2 size, in ScreenMode mode)
        => SetResolution(size, (int)ScreenRefreshRate, mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    /// <param name="refreshRate">The refresh rate of the monitor.</param>
    /// <param name="mode">Screen display mode.</param>
    public static void SetResolution(in float width, in float height, in int refreshRate, in ScreenMode mode)
        => SetResolution(new Vector2(width, height), refreshRate, mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    /// <param name="mode">Screen display mode.</param>
    public static void SetResolution(in float width, in float height, in ScreenMode mode)
        => SetResolution(new Vector2(width, height), mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="size">The size of the screen.</param>
    /// <param name="refreshRate">The refresh rate of the monitor.</param>
    public static void SetResolution(in Vector2 size, in int refreshRate)
        => SetResolution(size, refreshRate, Mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="size">The size of the screen.</param>
    public static void SetResolution(in Vector2 size)
        => SetResolution(size, Mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    public static void SetResolution(in float width, in float height)
        => SetResolution(width, height, Mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="width">The width of the screen.</param>
    /// <param name="height">The height of the screen.</param>
    /// <param name="refreshRate">The refresh rate of the monitor.</param>
    public static void SetResolution(in float width, in float height, in int refreshRate)
        => SetResolution(width, height, refreshRate, Mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="resolution">The new screen resolution.</param>
    /// <param name="mode">Screen display mode.</param>
    public static void SetResolution(in Resolution resolution, in ScreenMode mode)
        => SetResolution(resolution.Width, resolution.Height, resolution.Frequency, mode);
    /// <summary>sets the current screen resolution.</summary>
    /// <param name="resolution">The new screen resolution.</param>
    public static void SetResolution(in Resolution resolution)
        => SetResolution(resolution.Width, resolution.Height, resolution.Frequency);

    private static Resolution GetGameScreenResolution() {
        Vector2 size = OS.WindowSize;
        if (_gameScreenResolution.TryGetValue(size, out Resolution resolution))
            return resolution;
        resolution = new(size, (int)ScreenRefreshRate);
        _gameScreenResolution.Add(size, resolution);
        return resolution;
    }

    private static void AddResolution(in CustonResolutionList[]? resolutions) {
        if (resolutions is null) return;
        CustonResolutionList[]? orphanList = resolutions;
        int[]? hashs = [];
        for (int I = 0; I < DisplayCount; I++) {
            int hash = DisplayInfo.GetHash(Displays[I]);
            foreach (CustonResolutionList item2 in resolutions)
                if (hash == (int)item2) {
                    foreach (Resolution item3 in item2)
                        Displays[I] = DisplayInfo.AddCustonResolution(item3, Displays[I]);
                    ArrayManipulation.Add((int)item2, ref hashs);
                    break;
                }
        }

        if (ArrayManipulation.EmpytArray(hashs)) {
            OrphanList = [];
            return;
        }

        for (int A = 0; A < hashs.Length; A++)
            for (int B = 0; B < ArrayManipulation.ArrayLength(orphanList); B++)
                if (hashs[A] == (int)orphanList![B]) {
                    ArrayManipulation.Remove(B, ref orphanList);
                    break;
                }

        OrphanList = orphanList!;
        ArrayManipulation.ClearArray(ref hashs);
    }

    private static int GetIndexDisplay(in int index) {
        for (int I = 0; I < DisplayCount; I++)
            if (Displays[I].Index == index)
                return I;
        return -1;
    }

    private static DisplayInfo GetDisplay(in int index) {
        foreach (DisplayInfo item in Displays)
            if (item.Index == index)
                return item;
        return DisplayInfo.None;
    }
}