using System.Threading.Tasks;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        [Inject] private readonly ISceneLoader _sceneLoader;

        public void Initialize()
        {
            _ = BootAsync();
        }

        private async Task BootAsync()
        {
            await _sceneLoader.LoadAsync(SceneNames.Game);
        }
    }
}
