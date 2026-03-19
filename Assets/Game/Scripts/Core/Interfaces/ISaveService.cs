using System;
using UniRx;
using UnityEngine;

namespace Game.Core
{
    public interface ISaveService
    {
        public void Save(PlayerSaveData data);
        public PlayerSaveData Load();
    }
}