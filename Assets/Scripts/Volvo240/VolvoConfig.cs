using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class VolvoConfig
{
    private static VolvoConfig Curr;
    public static VolvoConfig Get { get => Curr; }

    //for perma powerup
    public float baseAcceleration;
    public float baseMaxSpeed;

    public float baseDamage;
    public float baseHealth;

    //for temp powerup
    public float currAcceleration;
    public float currMaxSpeed;

    public float currDamage;
    public float currHealth;

    public MonoBehaviour SomePlayerComponent;

    public static void Init()
    {
        if (Curr == null)
            Curr = new VolvoConfig();
    }
}
