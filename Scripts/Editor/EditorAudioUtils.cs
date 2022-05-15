using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier.Editor
{
    static class EditorAudioUtils
    {
        internal static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
        {
            MethodInfo method = GetPlayPreviewClipMethodInfo();

            if (method != null)
            {
                method.Invoke(
                    null,
                    new object[]
                    {
                        clip, startSample, loop
                    }
                );
            }
        }

        private static MethodInfo GetPlayPreviewClipMethodInfo()
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new Type[]
                {
                    typeof(AudioClip), typeof(int), typeof(bool)
                },
                null
            );
            return method;
        }
    }
}