using UniRx;

namespace Game.Core
{
    public class EnemyModel
    {
        public ReactiveProperty<float> CurrentHp;
        public IReadOnlyReactiveProperty<bool> IsDead;
    }
}