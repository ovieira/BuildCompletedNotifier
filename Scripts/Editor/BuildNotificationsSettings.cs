using UnityEditor;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier
{
    public class BuildNotificationsSettings : ScriptableObjectSingleton<BuildNotificationsSettings>
    {
        private const string BUILD_NOTIFICATIONS_SETTINGS_KEY = "BuildCompletedNotifier.BuildNotificationsSettings";

        private static readonly string ENABLED_KEY = $"{BUILD_NOTIFICATIONS_SETTINGS_KEY}.enabled";

        [SerializeField]
        private AudioClip buildSuccessfulAudioClip;

        [SerializeField]
        private AudioClip buildFailedAudioClip;

        public bool IsEnabled
        {
            get => EditorPrefs.GetBool(ENABLED_KEY, true);
            set => EditorPrefs.SetBool(ENABLED_KEY, value);
        }

        public AudioClip BuildSuccessfulAudioClip => buildSuccessfulAudioClip;

        public AudioClip BuildFailedAudioClip => buildFailedAudioClip;
    }
}