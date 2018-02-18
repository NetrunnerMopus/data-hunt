using UnityEngine;

public interface IEffect
{
    void Resolve(Game game, MonoBehaviour source);
}