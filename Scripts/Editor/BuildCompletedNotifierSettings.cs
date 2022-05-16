using UnityEditor;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier
{
    internal class BuildCompletedNotifierSettings : ScriptableObjectSingleton<BuildCompletedNotifierSettings>
    {
        private const string SETTINGS_KEY = "BuildCompletedNotifier.Settings";

        private static readonly string ENABLED_KEY = $"{SETTINGS_KEY}.enabled";

        [SerializeField]
        private AudioClip buildSuccessful;

        [SerializeField]
        private AudioClip buildFailed;

        public bool IsEnabled
        {
            get => EditorPrefs.GetBool(ENABLED_KEY, true);
            set => EditorPrefs.SetBool(ENABLED_KEY, value);
        }

        public AudioClip BuildSuccessful => buildSuccessful;

        public AudioClip BuildFailed => buildFailed;
    }
}