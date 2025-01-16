using OpenTK;
using System;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility;

/// <summary>Contains information from a specific screen.</summary>
public readonly struct DisplayInfo : IEquatable<DisplayInfo> {
    private readonly int index;
    private readonly Resolution[] resolutions;
    private readonly Resolution currentResolution;
    private readonly CustonResolutionList c_resolutions;

    /// <summary>Screen index</summary>
    /// <returns>Returns the screen index.</returns>
    public int Index => index;
    /// <summary>Screen resolutions.</summary>
    /// <returns>Returns all resolutions supported by the display.</returns>
    public Resolution[] Resolutions => resolutions;
    /// <summary>Current screen resolution.</summary>
    /// <returns>Returns the current resolution of this <seealso cref="DisplayDevice"/>.</returns>
    public Resolution CurrentResolution => currentResolution;
    /// <summary>The list of custom resolutions.</summary>
    /// <returns>Returns a list of custom resolutions from <seealso cref="DisplayInfo"/>.</returns>
    public CustonResolutionList CustonResolutions => c_resolutions;
    /// <summary>Empty display.</summary>
    /// <returns>Returns an empty instance of <seealso cref="DisplayInfo"/>.</returns>
    public static DisplayInfo None => new(-1, null);

    /// <summary>Starts a new instance of the object.</summary>
    /// <param name="index">The monitor index.</param>
    /// <param name="device">The <seealso cref="DisplayDevice"/> that contains the screen information.</param>
    public DisplayInfo(in int index, in DisplayDevice? device) {
        this.index = index;
        if (device is null) {
            resolutions = Array.Empty<Resolution>();
            return;
        }
        IList<DisplayResolution> temp_resolutions = device.AvailableResolutions;
        currentResolution = new(device.Width, device.Height, (int)device.RefreshRate);
        resolutions = new Resolution[temp_resolutions.Count];

        for (int I = 0; I < temp_resolutions.Count; I++)
            resolutions[I] = new(temp_resolutions[I].Width, temp_resolutions[I].Height, (int)temp_resolutions[I].RefreshRate);
        
        c_resolutions = new(GetHash(this), Array.Empty<Resolution>());
    }

    private DisplayInfo(int index, Resolution[] resolutions, CustonResolutionList c_resolutions, Resolution currentResolution) {
        this.index = index;
        this.resolutions = resolutions;
        this.c_resolutions = c_resolutions;
        this.currentResolution = currentResolution;
    }

    /// <inheritdoc/>
    public bool Equals(DisplayInfo other) => other.index == this.index;
    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is DisplayInfo other && Equals(other);
    /// <inheritdoc/>
    public override int GetHashCode() => base.GetHashCode();
    /// <summary>Allows you to check whether a certain resolution exists on this display.</summary>
    /// <param name="resolution">Target resolution.</param>
    /// <param name="includeCustomResolution">Tells the method whether to compare with the custom resolution list.</param>
    /// <returns>If the resolution exists, it will return <c>true</c>.</returns>
    public bool Contains(in Resolution resolution, in bool includeCustomResolution = false) {
        foreach (Resolution item in resolutions)
            if (item == resolution)
                return true;
        if (includeCustomResolution)
            foreach (Resolution item in c_resolutions)
                if (item == resolution)
                    return true;
        return false;
    }
    /// <summary>Generates a hash from <seealso cref="DisplayInfo"/>.</summary>
    /// <param name="display">The object that will be used.</param>
    /// <returns>Returns a hash generated from the <seealso cref="DisplayInfo"/> index and resolution list.</returns>
    public static int GetHash(in DisplayInfo display) {
        int hash = display.index.GetHashCode();
        for (int I = 0; I < display.resolutions.Length; I++) {
            int s_hash = display.resolutions[I].Width.GetHashCode() >> 2 ^ display.resolutions[I].Height.GetHashCode() << 2 ^ 
                display.resolutions[I].Frequency.GetHashCode();
            if (I % 2 == 0) hash = hash >> 2 ^ s_hash;
            else hash = hash << 2 ^ s_hash;
        }
        return hash;
    }
    /// <summary>Adds a custom resolution to <seealso cref="DisplayInfo"/>.</summary>
    /// <param name="resolution">The resolution to be added.</param>
    /// <param name="display">The target <seealso cref="DisplayInfo"/>.</param>
    /// <returns>Returns a new, modified instance of <seealso cref="DisplayInfo"/>.</returns>
    public static DisplayInfo AddCustonResolution(in Resolution resolution, in DisplayInfo display) {
        Resolution[] temp = [.. display.c_resolutions];
        ArrayManipulation.Add(resolution, ref temp);
        return new(display.index, display.resolutions, new((int)display.c_resolutions, temp), resolution);
    }
    /// <summary>Allows you to change the current resolution of this display.</summary>
    /// <param name="resolution">The new resolution.</param>
    /// <param name="display">The target <seealso cref="DisplayInfo"/>.</param>
    /// <returns>Returns a new, modified instance of <seealso cref="DisplayInfo"/>.</returns>
    public static DisplayInfo ChangeCurrentResolution(in Resolution resolution, in DisplayInfo display)
        => new(display.index, display.resolutions, display.c_resolutions, resolution);

    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(DisplayInfo A, DisplayInfo B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(DisplayInfo A, DisplayInfo B) => !A.Equals(B);
}
