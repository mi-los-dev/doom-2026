namespace Game.Core
{
    public interface IUpgradeService
    {
        UpgradeSessionModel OpenSession();
        void Apply(UpgradeSessionModel session);
        void Discard(UpgradeSessionModel session);
    }
}
