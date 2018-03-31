using UnityEngine;

namespace model
{
    public interface IEffect
    {
        void Resolve(Game game, MonoBehaviour source);
    }
}