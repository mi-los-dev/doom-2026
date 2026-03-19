using UniRx;

namespace Game.Core
{
    public class PlayerModel
    {
        public ReactiveProperty<float> CurrentHp;
        public ReactiveProperty<float> MaxHp;
        public ReactiveProperty<float> MoveSpeed;
        public ReactiveProperty<float> Damage;
        public ReactiveProperty<int> UpgradePoints;
    }
}