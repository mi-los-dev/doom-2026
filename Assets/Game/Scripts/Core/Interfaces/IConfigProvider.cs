using System;
using UniRx;
using UnityEngine;

namespace Game.Core
{
    public interface IConfigProvider
    {
        T Get<T>() where T : ScriptableObject;
    }
}