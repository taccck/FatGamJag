using UnityEngine;

public abstract class PowerupBase : ScriptableObject
{
    public string PowerupName; 
    public float Increase;
    public int Rarity; 
    public Color TextColor;

    public virtual void ApplyPowerup() 
    {
        if (VolvoConfig.Get == null)
            return; 

        Debug.Log(PowerupName); 
    }
}
