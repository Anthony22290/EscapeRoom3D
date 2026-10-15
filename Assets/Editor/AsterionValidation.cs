using System;
using System.IO;
using Asterion;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// File commands are restricted to these project-local development operations.
[InitializeOnLoad]
public static class AsterionValidation
{
    static double nextCheck;
    static AsterionValidation()
    {
        EditorApplication.update += Poll;
        EditorApplication.playModeStateChanged += OnPlayState;
    }
    static void Poll()
    {
        if (EditorApplication.timeSinceStartup < nextCheck) return;
        nextCheck = EditorApplication.timeSinceStartup + 1;
        const string request = "Library/AsterionCommand.txt";
        if (!File.Exists(request) || EditorApplication.isCompiling || EditorApplication.isUpdating || EditorApplication.isPlayingOrWillChangePlaymode) return;
        // An unfocused editor may not have imported the latest external edits yet.
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
        for (int i = 0; i < SceneManager.sceneCount; i++)
            if (SceneManager.GetSceneAt(i).isDirty) return;
        string command = File.ReadAllText(request).Trim();
        File.Delete(request);
        try
        {
            if (command == "build") AsterionBuilder.Build();
            else if (command == "verify") Verify();
            else if (command == "build-player") BuildPlayer();
            File.WriteAllText("Logs/Asterion-Command.log", "OK: " + command);
        }
        catch (Exception ex)
        {
            File.WriteAllText("Logs/Asterion-Command.log", ex.ToString());
            Debug.LogException(ex);
        }
    }
    [MenuItem("Asterion/Verificar recorrido completo")]
    public static void Verify()
    {
        if (EditorApplication.isPlaying) return;
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
        EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity");
        SessionState.SetBool("Asterion.Verify", true);
        EditorApplication.EnterPlaymode();
    }
    static void OnPlayState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode && SessionState.GetBool("Asterion.Verify", false))
        {
            SessionState.SetBool("Asterion.Verify", false);
            new GameObject("Validación temporal").AddComponent<AsterionPlayValidation>();
        }
    }
    [MenuItem("Asterion/Compilar juego para Windows")]
    public static void BuildPlayer()
    {
        Directory.CreateDirectory("Builds/Windows");
        var result = BuildPipeline.BuildPlayer(new[] { "Assets/Scenes/MainScene.unity" }, "Builds/Windows/Asterion.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        if (result.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            throw new Exception("La compilación falló: " + result.summary.result);
        File.WriteAllText("Logs/Asterion-PlayerBuild.txt", "Build succeeded: " + result.summary.totalSize + " bytes; " + result.summary.totalTime);
    }
}
