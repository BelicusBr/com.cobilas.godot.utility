using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Cobilas.GodotEditor.Utility.Tasks;

public class UtilityTask : Task{
	public override bool Execute() => false;

	protected bool LogError(System.Exception ex)
	{
		Log.LogErrorFromException(ex);
		return !Log.HasLoggedErrors;
	}

	protected bool LogError(string message)
	{
		Log.LogError(message);
		return !Log.HasLoggedErrors;
	}

	protected bool LogMessage(string message)
	{
		Log.LogMessage(MessageImportance.High, message);
		return !Log.HasLoggedErrors;
	}
}
