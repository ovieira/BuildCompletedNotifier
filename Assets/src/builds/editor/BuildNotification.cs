using System;
using System.IO;
using System.Media;
using System.Threading;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class BuildNotification
{

    [PostProcessBuild]
    public static void OnBuildComplete(BuildTarget buildTarget, string pathToBuiltProject)
    {
        var clip = Resources.Load<AudioClip>("buildcompleted");
        var clipPath = AssetDatabase.GetAssetPath(clip);
        _t = new Thread(() => PlaySound(clipPath))
        {
            IsBackground = true
        };
        _t.Start();
    }

    static Thread _t;

    public static void PlaySound(string clipPath)
    {
        var absolutePath = Path.Combine(Environment.CurrentDirectory, clipPath);
        using (SoundPlayer player = new SoundPlayer(absolutePath))
        {
            player.PlaySync();
        }
        _t.Join();
    }
}