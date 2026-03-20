using System;
using Game.Core;
using UniRx;
using UnityEngine;

namespace Game.Infrastructure
{
    public class PCInputProvider : IInputProvider
    {
        public IObservable<Vector2> LookInput()
        {
            return Observable.EveryUpdate()
                .Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")))
                .Where(v => v != Vector2.zero);
        }

        public IObservable<Vector2> MoveInput()
        {
            return Observable.EveryUpdate()
                .Select(_ => Vector2.ClampMagnitude(
                    new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1f));
        }

        public IObservable<Unit> ShootInput()
        {
            return Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Fire1"))
                .Select(_ => Unit.Default);
        }

        public IObservable<Unit> JumpInput()
        {
            return Observable.EveryUpdate()
                .Where(_ => Input.GetButtonDown("Jump"))
                .Select(_ => Unit.Default);
        }

        public IObservable<Unit> UpgradeUIInput()
        {
            return Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Tab))
                .Select(_ => Unit.Default);
        }
    }
}