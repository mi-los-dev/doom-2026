using System;
using UniRx;
using UnityEngine;

namespace Game.Core
{
    public interface IInputProvider
    {
        IObservable<Vector2> MoveInput();
        IObservable<Vector2> LookInput();
        IObservable<Unit> ShootInput();
        IObservable<Unit> UpgradeUIInput();
    }
}