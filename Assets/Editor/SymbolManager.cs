using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Compiler option symbol manager.
/// </summary>
public static class SymbolManager {

    /// <summary>
    /// Gets the symbols.
    /// </summary>
    /// <returns>The symbols.</returns>
    public static List<string> GetSymbols(){
        return PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';').ToList();
    }

    /// <summary>
    /// Sets the symbols.
    /// </summary>
    /// <param name="symbols">Symbols.</param>
    public static void SetSymbols(List<string> symbols){
        var symbolStr = string.Empty;
        symbols.ForEach(s => symbolStr += s + ";");
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbolStr);
    }
}
