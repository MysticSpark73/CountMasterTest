using CountMasters.Core;
using UnityEngine;

namespace CountMasters.Game.Level.Obstacles
{
    public abstract class Obstacle : MonoBehaviour, IInitable, IResetable
    {
        public abstract void Reset();
        public abstract void Init(params object[] args);
    }
}