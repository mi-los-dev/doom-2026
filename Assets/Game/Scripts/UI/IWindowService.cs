using System;
using UniRx;

namespace Game.UI
{
    public interface IWindowService
    {
        IObservable<Type> WindowOpened { get; }
        IObservable<Type> WindowClosed { get; }
        IObservable<bool> AnyWindowOpen { get; }

        void Open<TWindow>();
        void Close<TWindow>();
    }
}
