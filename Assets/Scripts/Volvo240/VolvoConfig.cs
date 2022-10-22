public class VolvoConfig
{
    private static VolvoConfig Curr;
    public static VolvoConfig Get { get; }

    //for perma powerup
    public float baseAcceleration;
    public float baseMaxSpeed;
    public float baseTurnSpeed;

    public float baseDamage;
    public uint baseHealth;

    //for temp powerup
    public float currAcceleration;
    public float currMaxSpeed;
    public float currTurnSpeed;

    public float currDamage;
    public uint currHealth;

    public static void Init()
    {
        if (Curr == null)
            Curr = new VolvoConfig();
    }
}
