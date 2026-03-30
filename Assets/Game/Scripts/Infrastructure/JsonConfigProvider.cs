using Game.Core;
using UnityEngine;

namespace Game.Infrastructure
{
    public class JsonConfigProvider : IConfigProvider
    {
        private const string ConfigsFolder = "Configs";

        public T Get<T>() where T : ScriptableObject
        {
            var fileName = $"{ConfigsFolder}/{typeof(T).Name}";
            var textAsset = Resources.Load<TextAsset>(fileName);

            var instance = ScriptableObject.CreateInstance<T>();

            if (textAsset != null)
                JsonUtility.FromJsonOverwrite(textAsset.text, instance);
            else
                Debug.LogWarning($"[JsonConfigProvider] Config file not found: Resources/{fileName}.json");

            return instance;
        }
    }
}
