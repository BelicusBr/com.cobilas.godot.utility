using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Cobilas.GodotEngine.Utility;
/// <summary>Static class to print messages to the console.</summary>
public static class DebugLog {
    /// <summary>Allows you to print the complete log trace.</summary>
    public static bool PrintTrace = false;

    private static Stream stream = Stream.Null;
    /// <summary>Allows you to print a log to the console.</summary>
    /// <param name="message">The message that will be printed to the console.</param>
    public static void Log(params object[] message) => Print(0, message);
    /// <summary>Allows you to print an error log to the console.</summary>
    /// <param name="message">The message that will be printed to the console.</param>
    public static void ErroLog(params object[] message) => Print(1, message);
    /// <summary>Allows you to print an exception log to the console.</summary>
    /// <param name="ex">The exception that will be printed to the console.</param>
    public static void ExceptionLog(Exception ex) => ErroLog(ex);
    /// <summary>Allows you to redirect logs to a log file.</summary>
    /// <param name="filePath">The path of the log file.</param>
    /// <param name="clear"><c>true</c> to have the log file cleared.</param>
    /// <exception cref="ArgumentNullException">When the FilePath parameter is null.</exception>
    /// <exception cref="InvalidOperationException">Occurs when the flow is still open.</exception>
    public static void LogToStream(string? filePath, bool clear = false) {
        if (filePath is null) throw new ArgumentNullException(nameof(filePath));
        if (stream != Stream.Null) throw new InvalidOperationException($"Flow {filePath} is already open!");
        stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
        if (clear) stream.SetLength(0L);
        else stream.Position = stream.Length - 1;
    }
    /// <summary>Allows the termination of a log stream.</summary>
    /// <exception cref="InvalidOperationException">Occurs when the flow is already closed.</exception>
    public static void CloseStream() {
        if (stream == Stream.Null) throw new InvalidOperationException($"It is not possible to close a flow that has already been closed!");
        stream.Dispose();
        stream = Stream.Null;
    }

    private static void Print(byte typeLog, params object[]? message) {
        if (message is null) throw new ArgumentNullException(nameof(message));
        if (stream != Stream.Null) {
            StringBuilder builder = new();
            switch(typeLog) {
                case 0: builder.AppendLine($"*Log ({DateTime.Now})"); break;
                case 1: builder.AppendLine($"*Error ({DateTime.Now})"); break;
            }
            builder.AppendLine(BuildLog(message));
            stream.Write(builder.ToString());
        } else
            switch(typeLog) {
                case 0: Godot.GD.Print(BuildLog(message)); break;
                case 1: Godot.GD.PrintErr(BuildLog(message)); break;
            }
    }

    private static string BuildLog(params object[] message) {
        StringBuilder builder = new();
        builder.AppendLine(LogLine(2));
        builder.AppendLine("=====<message>=====");
        foreach (object item in message)
            if (item is not null)
                builder.AppendLine(item.ToString());
        return builder.ToString().TrimEnd();
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