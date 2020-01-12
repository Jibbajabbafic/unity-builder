using UnityEditor;
using System.Linq;
using System;
using System.IO;
using UnityEngine;
using UnityEditor.Build.Reporting;

static class BuildScript
{
	// // Command Line Arg Build Methods
	// // ===============================

	// private static string EOL = Environment.NewLine;

	// static string GetArgument (string name)
	// {
	// 	string[] args = Environment.GetCommandLineArgs ();
	// 	for (int i = 0; i < args.Length; i++) {
	// 		if (args [i].Contains (name)) {
	// 			return args [i + 1];
	// 		}
	// 	}
	// 	return null;
	// }

	// static string[] GetEnabledScenes ()
	// {
	// 	return (
	// 		from scene in EditorBuildSettings.scenes
	// 		 where scene.enabled
	// 		 where !string.IsNullOrEmpty(scene.path)
	// 		 select scene.path
	// 	).ToArray ();
	// }

	// static BuildTarget GetBuildTarget ()
	// {
	// 	string buildTargetName = GetArgument ("customBuildTarget");
	// 	Console.WriteLine (":: Received customBuildTarget " + buildTargetName);

	// 	if (buildTargetName.ToLower () == "android") {
	// 		#if !UNITY_5_6_OR_NEWER
	// 		// https://issuetracker.unity3d.com/issues/buildoptions-dot-acceptexternalmodificationstoplayer-causes-unityexception-unknown-project-type-0
	// 		// Fixed in Unity 5.6.0
	// 		// side effect to fix android build system:
	// 		EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;
	// 		#endif
	// 	}

	// 	return ToEnum<BuildTarget> (buildTargetName, BuildTarget.NoTarget);
	// }

	// static string GetBuildPath ()
	// {
	// 	string buildPath = GetArgument ("customBuildPath");
	// 	Console.WriteLine (":: Received customBuildPath " + buildPath);
	// 	if (buildPath == "") {
	// 		throw new Exception ("customBuildPath argument is missing");
	// 	}
	// 	return buildPath;
	// }

	// static string GetBuildName ()
	// {
	// 	string buildName = GetArgument ("customBuildName");
	// 	Console.WriteLine (":: Received customBuildName " + buildName);
	// 	if (buildName == "") {
	// 		throw new Exception ("customBuildName argument is missing");
	// 	}
	// 	return buildName;
	// }

	// static string GetFixedBuildPath (BuildTarget buildTarget, string buildPath, string buildName) {
	// 	var targetString = buildTarget.ToString().ToLower();
	// 	if (targetString.Contains("windows"))
	// 	{
	// 		buildName += ".exe";
	// 	}
	// 	else if (targetString.Contains("osx"))
	// 	{
	// 		buildName += ".app";
	// 	}
	// 	else if (targetString.Contains("webgl"))
	// 	{
	// 		// webgl produces a folder with index.html inside, there is no executable name for this buildTarget
	// 		buildName = "";
	// 	}
	// 	return buildPath + buildName;
	// }

	// static BuildOptions GetBuildOptions ()
	// {
	// 	string buildOptions = GetArgument ("customBuildOptions");
	// 	return buildOptions == "AcceptExternalModificationsToPlayer" ? BuildOptions.AcceptExternalModificationsToPlayer : BuildOptions.None;
	// }

	// // https://stackoverflow.com/questions/1082532/how-to-tryparse-for-enum-value
	// static TEnum ToEnum<TEnum> (this string strEnumValue, TEnum defaultValue)
	// {
	// 	if (!Enum.IsDefined (typeof(TEnum), strEnumValue)) {
	// 		return defaultValue;
	// 	}

	// 	return (TEnum)Enum.Parse (typeof(TEnum), strEnumValue);
	// }

	// static string getEnv (string key, bool secret = false, bool verbose = true)
	// {
	// 	var env_var = Environment.GetEnvironmentVariable (key);
	// 	if (verbose) {
	// 		if (env_var != null) {
	// 			if (secret) {
	// 				Console.WriteLine (":: env['" + key + "'] set");
	// 			} else {
	// 				Console.WriteLine (":: env['" + key + "'] set to '" + env_var + "'");
	// 			}
	// 		} else {
	// 			Console.WriteLine (":: env['" + key + "'] is null");
	// 		}
	// 	}
	// 	return env_var;
	// }

	// static void PerformBuild ()
	// {
	// 	Console.WriteLine (":: Performing build with these options:");
	// 	//PlayerSettings.keystorePass = getEnv ("KEYSTORE_PASS", true);
	// 	//PlayerSettings.keyaliasPass = getEnv ("KEY_ALIAS_PASS", true);
	// 	//EditorSetup.AndroidSdkRoot = getEnv ("ANDROID_SDK_HOME");
	// 	//EditorSetup.JdkRoot = getEnv ("JAVA_HOME");
	// 	//EditorSetup.AndroidNdkRoot = getEnv ("ANDROID_NDK_HOME");
	// 	// var buildScenes = new[] {"Assets/scenes/Lobby.unity", "Assets/scenes/OutpostStation.unity"};
	// 	var buildScenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();
	// 	var buildTarget = GetBuildTarget();
	// 	var buildPath = GetBuildPath();
	// 	var buildName = GetBuildName();
	// 	var fixedBuildPath = GetFixedBuildPath (buildTarget, buildPath, buildName);

	// 	Console.WriteLine ($":: scenes: {buildScenes}");
	// 	Console.WriteLine ($":: locationPathName: {fixedBuildPath}");
	// 	Console.WriteLine ($":: target: {buildTarget}");

	// 	BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
	// 	buildPlayerOptions.scenes = buildScenes;
	// 	buildPlayerOptions.locationPathName = fixedBuildPath;
	// 	buildPlayerOptions.target = buildTarget;
	// 	buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging; //| BuildOptions.CompressWithLz4HC;
	// 	// BuildPreferences.SetRelease(true);

	// 	// Perform build
	// 	BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

	// 	// Summary
	// 	BuildSummary summary = buildReport.summary;
	// 	ReportSummary(summary);

	// 	// Result
	// 	BuildResult result = summary.result;
	// 	ExitWithResult(result);
	// }

	// private static void ReportSummary(BuildSummary summary)
	// {
	// 	Console.WriteLine(
	// 	$"{EOL}" +
	// 	$"###########################{EOL}" +
	// 	$"#      Build results      #{EOL}" +
	// 	$"###########################{EOL}" +
	// 	$"{EOL}" +
	// 	$"Duration: {summary.totalTime.ToString()}{EOL}" +
	// 	$"Warnings: {summary.totalWarnings.ToString()}{EOL}" +
	// 	$"Errors: {summary.totalErrors.ToString()}{EOL}" +
	// 	$"Size: {summary.totalSize.ToString()} bytes{EOL}" +
	// 	$"{EOL}"
	// 	);
	// }

	// private static void ExitWithResult(BuildResult result)
	// {
	// 	if (result == BuildResult.Succeeded) {
	// 		Console.WriteLine("Build succeeded!");
	// 		EditorApplication.Exit(0);
	// 	}

	// 	if (result == BuildResult.Failed) {
	// 		Console.WriteLine("Build failed!");
	// 		EditorApplication.Exit(101);
	// 	}

	// 	if (result == BuildResult.Cancelled) {
	// 		Console.WriteLine("Build cancelled!");
	// 		EditorApplication.Exit(102);
	// 	}

	// 	if (result == BuildResult.Unknown) {
	// 		Console.WriteLine("Build result is unknown!");
	// 		EditorApplication.Exit(103);
	// 	}
	// }



	private static string EOL = Environment.NewLine;

	private static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments)
	{
		providedArguments = new Dictionary<string, string>();
		string[] args = Environment.GetCommandLineArgs();

		Console.WriteLine(
			$"{EOL}" +
			$"###########################{EOL}" +
			$"#    Parsing settings     #{EOL}" +
			$"###########################{EOL}" +
			$"{EOL}"
		);

		// Extract flags with optional values
		for (int current = 0, next = 1; current < args.Length; current++, next++) {
			// Parse flag
			bool isFlag = args[current].StartsWith("-");
			if (!isFlag) continue;
			string flag = args[current].TrimStart('-');

			// Parse optional value
			bool flagHasValue = next < args.Length && !args[next].StartsWith("-");
			string value = flagHasValue ? args[next].TrimStart('-') : "";

			// Assign
			Console.WriteLine($"Found flag \"{flag}\" with value \"{value}\".");
			providedArguments.Add(flag, value);
		}
	}

	private static Dictionary<string, string> GetValidatedOptions()
	{
		ParseCommandLineArguments(out var validatedOptions);

		if (!validatedOptions.TryGetValue("projectPath", out var projectPath)) {
			Console.WriteLine("Missing argument -projectPath");
			EditorApplication.Exit(110);
		}

		if (!validatedOptions.TryGetValue("buildTarget", out var buildTarget)) {
			Console.WriteLine("Missing argument -buildTarget");
			EditorApplication.Exit(120);
		}

		if (!Enum.IsDefined(typeof(BuildTarget), buildTarget)) {
			EditorApplication.Exit(121);
		}

		if (!validatedOptions.TryGetValue("customBuildPath", out var customBuildPath)) {
			Console.WriteLine("Missing argument -customBuildPath");
			EditorApplication.Exit(130);
		}

		string defaultCustomBuildName = "TestBuild";
		if (!validatedOptions.TryGetValue("customBuildName", out var customBuildName)) {
			Console.WriteLine($"Missing argument -customBuildName, defaulting to {defaultCustomBuildName}.");
			validatedOptions.Add("customBuildName", defaultCustomBuildName);
		}
		else if (customBuildName == "") {
			Console.WriteLine($"Invalid argument -customBuildName, defaulting to {defaultCustomBuildName}.");
			validatedOptions.Add("customBuildName", defaultCustomBuildName);
		}

		return validatedOptions;
	}

	private GetFileExtension(string buildTarget)
	{
		var target = buildTarget.ToLower();

		if (target.Contains("windows")) return ".exe";
		if (target.Contains("osx")) return ".app";

		return;
	}

	public static void BuildProject()
	{
		// Gather values from args
		var options = GetValidatedOptions();

		// Gather values from project
		var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();
		var path = options["customBuildPath"] + GetFileExtension(options["buildTarget"]);
		var target = (BuildTarget) Enum.Parse(typeof(BuildTarget), options["buildTarget"]);

		// Define BuildPlayer Options
		var buildOptions = new BuildPlayerOptions {
			scenes = scenes,
			locationPathName = path,
			target = target,
		};
		ReportOptions(buildOptions);

		// Perform build
		BuildReport buildReport = BuildPipeline.BuildPlayer(buildOptions);

		// Summary
		BuildSummary summary = buildReport.summary;
		ReportSummary(summary);

		// Result
		BuildResult result = summary.result;
		ExitWithResult(result);
	}

	private static void ReportOptions(BuildPlayerOptions options)
	{
		Console.WriteLine(
			$"{EOL}" +
			$"###########################{EOL}" +
			$"#      Build options      #{EOL}" +
			$"###########################{EOL}" +
			$"{EOL}" +
			$"Scenes: {options.scenes.ToString()}{EOL}" +
			$"Path: {options.path.ToString()}{EOL}" +
			$"Target: {options.target.ToString()}{EOL}" +
			$"{EOL}"
		);
	}

	private static void ReportSummary(BuildSummary summary)
	{
		Console.WriteLine(
			$"{EOL}" +
			$"###########################{EOL}" +
			$"#      Build results      #{EOL}" +
			$"###########################{EOL}" +
			$"{EOL}" +
			$"Duration: {summary.totalTime.ToString()}{EOL}" +
			$"Warnings: {summary.totalWarnings.ToString()}{EOL}" +
			$"Errors: {summary.totalErrors.ToString()}{EOL}" +
			$"Size: {summary.totalSize.ToString()} bytes{EOL}" +
			$"{EOL}"
		);
	}

	private static void ExitWithResult(BuildResult result)
	{
		if (result == BuildResult.Succeeded) {
			Console.WriteLine("Build succeeded!");
			EditorApplication.Exit(0);
		}

		if (result == BuildResult.Failed) {
			Console.WriteLine("Build failed!");
			EditorApplication.Exit(101);
		}

		if (result == BuildResult.Cancelled) {
			Console.WriteLine("Build cancelled!");
			EditorApplication.Exit(102);
		}

		if (result == BuildResult.Unknown) {
			Console.WriteLine("Build result is unknown!");
			EditorApplication.Exit(103);
		}
	}
}