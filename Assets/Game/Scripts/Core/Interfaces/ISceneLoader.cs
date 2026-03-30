using System.Threading.Tasks;

namespace Game.Core
{
    public interface ISceneLoader
    {
        Task LoadAsync(string sceneName);
    }
}
