using System.Threading.Tasks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier
{
    class BuildCompletedNotifier : IPostprocessBuildWithReport
    {
        private const int NOTIFICATION_DELAY_IN_MILLISECONDS = 1000;

        public int callbackOrder => int.MaxValue;

        public async void OnPostprocessBuild(BuildReport report)
        {
            if (BuildNotificationsSettings.Instance.IsEnabled)
            {
                await PlayAudioClip(report.summary.result).ConfigureAwait(false);
            }
        }

        private static async Task PlayAudioClip(BuildResult buildResult)
        {
            await Task.Delay(NOTIFICATION_DELAY_IN_MILLISECONDS).ConfigureAwait(true);

            EditorUtils.PlayClip(GetAudioClip(buildResult));
        }

        private static AudioClip GetAudioClip(BuildResult buildResult)
        {
            switch (buildResult)
            {
                case BuildResult.Succeeded:
                    return BuildNotificationsSettings.Instance.BuildSuccessfulAudioClip;
                case BuildResult.Failed:
                    return BuildNotificationsSettings.Instance.BuildFailedAudioClip;
                default:
                    return null;
            }
        }
    }
}