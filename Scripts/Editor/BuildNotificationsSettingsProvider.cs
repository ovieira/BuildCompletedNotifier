using System.Collections.Generic;
using UnityEditor;

namespace JoaoVieira.BuildCompletedNotifier
{
    public class BuildNotificationsSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider("Project/JoaoVieira.BuildCompletedNotifications", SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    var settings = BuildNotificationsSettings.Instance;
                    var serialized = new SerializedObject(settings);
                    var editor = Editor.CreateEditor(settings);

                    bool isNotificationsEnabled = BuildNotificationsSettings.Instance.IsEnabled;

                    EditorGUI.BeginChangeCheck();
                    editor.OnInspectorGUI();

                    isNotificationsEnabled = EditorGUILayout.Toggle(
                        "Enable notifications",
                        isNotificationsEnabled
                    );

                    if (EditorGUI.EndChangeCheck())
                    {
                        BuildNotificationsSettings.Instance.IsEnabled = isNotificationsEnabled;
                        serialized.ApplyModifiedProperties();
                    }
                },
                keywords = new HashSet<string>(
                    new[]
                    {
                        "Build", "Notifications"
                    }
                )
            };
        }
    }
}