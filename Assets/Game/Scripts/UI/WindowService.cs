using System;
using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class WindowService : IWindowService
    {
        private readonly Subject<Type> _windowOpened = new Subject<Type>();
        private readonly Subject<Type> _windowClosed = new Subject<Type>();
        private readonly ReactiveProperty<int> _openCount = new ReactiveProperty<int>(0);

        public IObservable<Type> WindowOpened => _windowOpened;
        public IObservable<Type> WindowClosed => _windowClosed;
        public IObservable<bool> AnyWindowOpen => _openCount.Select(c => c > 0);

        public void Open<TWindow>()
        {
            _openCount.Value++;
            _windowOpened.OnNext(typeof(TWindow));
        }

        public void Close<TWindow>()
        {
            _openCount.Value = Mathf.Max(0, _openCount.Value - 1);
            _windowClosed.OnNext(typeof(TWindow));
        }
    }
}
