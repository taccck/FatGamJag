using UnityEngine;

public abstract class PowerupBase : ScriptableObject
{
    public float Multiplier;

    public virtual void ApplyPowerup() { }
}
