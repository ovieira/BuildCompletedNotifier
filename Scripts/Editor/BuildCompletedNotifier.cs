using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier.Editor
{
    class BuildCompletedNotifier : IPostprocessBuildWithReport
    {
        private const int notificationDelayInMilliseconds = 1000;

        public int callbackOrder => int.MaxValue;

        public async void OnPostprocessBuild(BuildReport report)
        {
           await PlaySuccessAudio().ConfigureAwait(false);
        }

        [MenuItem("Test/Play")]
        private static async Task Test()
        {
            await PlaySuccessAudio().ConfigureAwait(false);
        }

        private static async Task PlaySuccessAudio()
        {
            await Task.Delay(notificationDelayInMilliseconds).ConfigureAwait(true);
            AudioClip clip =
                EditorGUIUtility.Load(
                        "Packages/com.ovieira.buildcompletednotifier/Scripts/Editor/buildcompleted.wav"
                    ) as
                    AudioClip;
            if (clip == null)
            {
                Debug.Log("Unable to load audio clip");
            }

            EditorAudioUtils.PlayClip(clip);
        }
    }
}