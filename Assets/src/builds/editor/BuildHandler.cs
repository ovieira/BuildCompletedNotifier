using System;
using System.IO;
using System.Media;
using System.Threading;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildHandler : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{



    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        //add build number to app version
        AppVersion appVersion = getCurrentVersion();


        //add build number to bundle version for version printing on build
        PlayerSettings.bundleVersion = appVersion.ConvertToString(AppVersion.VersionMode.FULL);
    }


    public void OnPostprocessBuild(BuildReport report)
    {
        AppVersion appVersion = getCurrentVersion();
        appVersion.IncrementBuild();
        //
        printVersion();
        //
        PlayerSettings.bundleVersion = appVersion.ConvertToString(AppVersion.VersionMode.RELEASEMAJORMINOR);
        PlayerSettings.macOS.buildNumber = appVersion.ConvertToString(AppVersion.VersionMode.BUILD);
        //
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

    private static AppVersion getCurrentVersion()
    {
        return new AppVersion(PlayerSettings.bundleVersion, PlayerSettings.macOS.buildNumber);
    }

    #region Print
    [MenuItem("Tekuchi/Check App Version")]
    public static void printVersion()
    {
        getCurrentVersion().print();
    }
    #endregion


}