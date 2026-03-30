using System.Threading.Tasks;
using Game.Core;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        public async Task LoadAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
                await Task.Yield();
        }
    }
}
