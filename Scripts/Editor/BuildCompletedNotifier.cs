using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier
{
    class BuildCompletedNotifier : IPostprocessBuildWithReport
    {
        private const int notificationDelayInMilliseconds = 1000;

        public int callbackOrder => int.MaxValue;

        public async void OnPostprocessBuild(BuildReport report)
        {
            if (BuildNotificationsSettings.Instance.IsEnabled)
            {
                await PlaySuccessAudio().ConfigureAwait(false);
            }
        }

        [MenuItem("Test/Play")]
        private static async Task Test()
        {
            if (BuildNotificationsSettings.Instance.IsEnabled)
            {
                await PlaySuccessAudio().ConfigureAwait(false);
            }
        }

        private static async Task PlaySuccessAudio()
        {
            await Task.Delay(notificationDelayInMilliseconds).ConfigureAwait(true);

            EditorUtils.PlayClip(BuildNotificationsSettings.Instance.SuccessAudioClip);
        }
    }
}