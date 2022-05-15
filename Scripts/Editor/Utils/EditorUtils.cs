using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier
{
    static class EditorUtils
    {
        internal static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
        {
            if (clip == null)
            {
                return;
            }
            
            MethodInfo method = GetPlayPreviewClipMethodInfo();

            if (method != null)
            {
                method.Invoke(
                    null,
                    new object[]
                    {
                        clip, startSample, loop,
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
                new[]
                {
                    typeof(AudioClip), typeof(int), typeof(bool),
                },
                null
            );
            return method;
        }

        private const string EDITOR_DEFAULT_RESOURCES_PATH = "Assets/Editor Default Resources/";

        public static T FindScriptableObject<T>(bool createIfDoesNotExist = true)
        where T : ScriptableObject
        {
            string filename = typeof(T).Name + ".asset";

            T asset = EditorGUIUtility.Load(filename) as T;

            if (asset != null)
            {
                return asset;
            }

            if (!createIfDoesNotExist)
            {
                return null;
            }

            asset = ScriptableObject.CreateInstance<T>();
            string assetPath = Path.Combine(EDITOR_DEFAULT_RESOURCES_PATH, filename);
                
            if (!Directory.Exists(Path.GetFullPath(EDITOR_DEFAULT_RESOURCES_PATH)))
            {
                Directory.CreateDirectory(Path.GetFullPath(EDITOR_DEFAULT_RESOURCES_PATH));
            }

            AssetDatabase.CreateAsset(asset, assetPath);

            return asset;
        }
    }
}