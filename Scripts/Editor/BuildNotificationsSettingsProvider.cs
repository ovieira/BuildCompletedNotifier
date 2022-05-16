using System.Collections.Generic;
using UnityEditor;

namespace JoaoVieira.BuildCompletedNotifier
{
    class BuildNotificationsSettingsProvider : SettingsProvider
    {
        public BuildNotificationsSettingsProvider(string path, SettingsScope scopes,
            IEnumerable<string> keywords = null) : base(path, scopes, keywords) { }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new BuildNotificationsSettingsProvider(
                "Project/JoaoVieira.BuildCompletedNotifications",
                SettingsScope.Project
            );
            provider.keywords = new List<string>(
                new[]
                {
                    "Build", "Notifications"
                }
            );
            return provider;
        }

        public override void OnGUI(string searchContext)
        {
            var settings = BuildCompletedNotifierSettings.Instance;
            var serialized = new SerializedObject(settings);
            var editor = Editor.CreateEditor(settings);

            bool isNotificationsEnabled = BuildCompletedNotifierSettings.Instance.IsEnabled;

            EditorGUI.BeginChangeCheck();
            editor.OnInspectorGUI();
            isNotificationsEnabled = EditorGUILayout.Toggle(
                "Enable notifications",
                isNotificationsEnabled
            );
            if (EditorGUI.EndChangeCheck())
            {
                BuildCompletedNotifierSettings.Instance.IsEnabled = isNotificationsEnabled;
                serialized.ApplyModifiedProperties();
            }
        }
    }
}