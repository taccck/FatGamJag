using UnityEngine;

[CreateAssetMenu(fileName = "New Speed Powerup", menuName = "Gameplay/Powerups/Speed Powerup")]
public class SpeedPowerupBase : PowerupBase
{
	public override void ApplyPowerup()
	{
		base.ApplyPowerup();
		VolvoConfig.Get.baseMaxSpeed += Increase; 
	}
}
