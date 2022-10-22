using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Affecting Powerup", menuName = "Gameplay/Powerups/Enemy Affecting Powerup")]
public class EnemyAffectingPowerupBase : PowerupBase
{
	public override void ApplyPowerup()
	{
		base.ApplyPowerup();
		//Find all enemies in scene
		//Freeze
	}
}
