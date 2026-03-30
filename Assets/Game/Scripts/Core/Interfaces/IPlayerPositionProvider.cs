using UnityEngine;

namespace Game.Core
{
    public interface IPlayerPositionProvider
    {
        Vector3 Position { get; }
    }
}
