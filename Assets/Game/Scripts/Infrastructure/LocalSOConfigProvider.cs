using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using UnityEngine;

namespace Game.Infrastructure
{
    public class LocalSOConfigProvider : IConfigProvider
    {
        private readonly Dictionary<Type, ScriptableObject> _configs;

        public LocalSOConfigProvider(ScriptableObject[] configs)
        {
            _configs = configs.ToDictionary(c => c.GetType());
        }

        public T Get<T>() where T : ScriptableObject
        {
            if (_configs.TryGetValue(typeof(T), out ScriptableObject config))
                return (T)config;

            throw new InvalidOperationException($"[SOConfigProvider] Config '{typeof(T).Name}' is not registered.");
        }
    }
}
