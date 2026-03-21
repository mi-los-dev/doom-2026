
using Game.Core;
using UnityEngine;

namespace Game.Infrastructure
{
    public class StubLocalizationService : ILocalizationService
    {
        public string Get(string key)
        {
            return key;
        }
    }
}