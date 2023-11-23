using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EToolBar
{
    [MenuItem("E-ToolBar/Reset Prefs")]
    public static void ResetPrefs()
    {
        LocalPrefs.ClearAll();
        LocalPrefs.Save(LocalPrefs.defaultFileName, true);
    }
}
