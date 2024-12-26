using System;
using System.Text;
using System.Diagnostics;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Static class to print messages to the console.</summary>
public static class DebugLog {
    /// <summary>Allows you to print the complete log trace.</summary>
    public static bool PrintTrace = false;
    /// <summary>Allows you to print a log to the console.</summary>
    /// <param name="message">The message that will be printed to the console.</param>
    public static void Log(params object[] message) => Godot.GD.Print(BuildLog(message));
    /// <summary>Allows you to print an error log to the console.</summary>
    /// <param name="message">The message that will be printed to the console.</param>
    public static void ErroLog(params object[] message) => Godot.GD.PrintErr(BuildLog(message));
    /// <summary>Allows you to print an exception log to the console.</summary>
    /// <param name="ex">The exception that will be printed to the console.</param>
    public static void ExceptionLog(Exception ex) => ErroLog(ex);

    private static string BuildLog(params object[] message) {
        StringBuilder builder = new();
        builder.AppendLine(LogLine(2));
        builder.AppendLine("=====<message>=====");
        foreach (object item in message)
            builder.AppendLine(item.ToString());
        return builder.ToString();
    }

    private static string LogLine(int skipFrames) {
        StackTrace stackTrace = new(skipFrames, true);
        StackFrame[] frames = stackTrace.GetFrames();
        if (!PrintTrace) {
            StackFrame frame = frames[frames.Length - 1];
            return $"{frame.GetFileName()} (Ln:{frame.GetFileLineNumber()} Col:{frame.GetFileColumnNumber()})";
        }
        StringBuilder builder = new();
        for (int I = skipFrames; I < frames.Length; I++)
            builder.AppendLine($"{frames[I].GetFileName()} (Ln:{frames[I].GetFileLineNumber()} Col:{frames[I].GetFileColumnNumber()})");
        return builder.ToString();
    }
}