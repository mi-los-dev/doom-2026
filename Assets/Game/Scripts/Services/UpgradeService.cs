using Game.Core;
using Zenject;

namespace Game.Services
{
    public class UpgradeService
    {
        [Inject] private PlayerModel _playerModel;
        [Inject] private StatsTableConfig _statsTableConfig;
        [Inject] private ISaveService _saveService;
        [Inject] private StatCalculationService _statCalculationService;


        public UpgradeSessionModel OpenSession()
        {
            return new UpgradeSessionModel();
        }

        public void Apply(UpgradeSessionModel session)
        {

        }

        public void Discard(UpgradeSessionModel session)
        {

        }
    }
}