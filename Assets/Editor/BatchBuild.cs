using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Unity Batch build sample.
/// </summary>
public class BatchBuild {

    const string DevelopOptions = "DEVELOP";
    const string ReleaseOptions = "RELEASE";

    /// <summary>
    /// Android develop build.
    /// </summary>
    [MenuItem("BatchBuild/Android/Develop")]
    static void AndroidDevelopBuild(){
        SetDevelopCompilerOptions();
        AndroidBuild("Android_Develop.apk", true);
    }

    /// <summary>
    /// Android releasep build.
    /// </summary>
    [MenuItem("BatchBuild/Android/Release")]
    static void AndroidReleasepBuild(){
        SetReleaseCompilerOptions();
        AndroidBuild("Android_Release.apk", false);
    }

    /// <summary>
    /// iOS develop build.
    /// </summary>
    [MenuItem("BatchBuild/iOS/Develop")]
    static void iOSDevelopBuild(){
        SetDevelopCompilerOptions();
        iOSBuild("iOS_Develop_xcodeproject", true);
    }

    /// <summary>
    /// iOS release build.
    /// </summary>
    [MenuItem("BatchBuild/iOS/Release")]
    static void iOSReleaseBuild(){
        SetReleaseCompilerOptions();
        iOSBuild("iOS_Release_xcodeproject", false);
    }

    /// <summary>
    /// Output apk file at project project root.
    /// </summary>
    /// <param name="fileName">output file name.</param>
    /// <param name="isDevelop">develop flag.</param>
    static void AndroidBuild(string fileName, bool isDevelop){
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        var outputFile = Application.dataPath + "/../" + fileName;
        if(File.Exists(outputFile)){
            File.Delete(outputFile);
        }

        var target = BuildTarget.Android;
        var options = isDevelop ? GetDevelopOption() : GetReleaseOption();

        BuildPipeline.BuildPlayer(scenes, outputFile, target, options);
    }

    /// <summary>
    /// Output xcodeproject at project root.
    /// </summary>
    /// <param name="folderName">output xcodeproject folder name.</param>
    /// <param name="isDevelop">develop flag.</param>
    static void iOSBuild(string folderName, bool isDevelop){
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        var outputFile = Application.dataPath + "/../" + folderName;
        if(Directory.Exists(outputFile)){
            Directory.Delete(outputFile, true);
        }

        var target = BuildTarget.iOS;
        var options = isDevelop ? GetDevelopOption() : GetReleaseOption();

        BuildPipeline.BuildPlayer(scenes, outputFile, target, options);
    }

    /// <summary>
    /// Gets the develop build option.
    /// </summary>
    /// <returns>The develop option.</returns>
    static BuildOptions GetDevelopOption(){
        return BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
    }

    /// <summary>
    /// Gets the release build option.
    /// </summary>
    /// <returns>The release option.</returns>
    static BuildOptions GetReleaseOption(){
        return BuildOptions.None;
    }

    /// <summary>
    /// Sets the develop compiler options.
    /// </summary>
    static void SetDevelopCompilerOptions(){
        var symbols = SymbolManager.GetSymbols();
        if(!symbols.Contains(DevelopOptions)){
            symbols.Add(DevelopOptions);
        }
        symbols.Remove(ReleaseOptions);
        SymbolManager.SetSymbols(symbols);
    }

    /// <summary>
    /// Sets the release compiler options.
    /// </summary>
    static void SetReleaseCompilerOptions(){
        var symbols = SymbolManager.GetSymbols();
        if(!symbols.Contains(ReleaseOptions)){
            symbols.Add(ReleaseOptions);
        }
        symbols.Remove(DevelopOptions);
        SymbolManager.SetSymbols(symbols);
    }
}
