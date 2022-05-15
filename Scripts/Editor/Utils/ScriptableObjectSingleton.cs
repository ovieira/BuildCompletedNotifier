using UnityEngine;

namespace JoaoVieira.BuildCompletedNotifier
{
    public class ScriptableObjectSingleton<T> : ScriptableObject
    where T : ScriptableObject

    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = EditorUtils.FindScriptableObject<T>();
                }

                return _instance;
            }
        }
    }
}