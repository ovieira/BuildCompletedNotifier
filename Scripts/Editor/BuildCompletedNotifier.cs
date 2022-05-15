using System;
using System.IO;
using System.Media;
using System.Threading;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier.Editor
{
    class BuildCompletedNotifier : IPostprocessBuildWithReport
    {
        private static Thread thread;

        public int callbackOrder => int.MaxValue;

        public void OnPostprocessBuild(BuildReport report)
        {
            AudioClip clip = Resources.Load<AudioClip>("buildcompleted");
            string clipPath = AssetDatabase.GetAssetPath(clip);
            thread = new Thread(() => PlaySound(clipPath))
            {
                IsBackground = true,
            };
            thread.Start();
        }

        private static void PlaySound(string clipPath)
        {
            string absolutePath = Path.Combine(Environment.CurrentDirectory, clipPath);
            using (SoundPlayer player = new SoundPlayer(absolutePath))
            {
                player.PlaySync();
            }

            thread.Join();
        }
    }
}