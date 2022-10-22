using UnityEngine;

public abstract class PowerupBase : ScriptableObject
{
    public string PowerupName; 
    public float Multiplier;

    public virtual void ApplyPowerup() { Debug.Log(PowerupName); }
}
