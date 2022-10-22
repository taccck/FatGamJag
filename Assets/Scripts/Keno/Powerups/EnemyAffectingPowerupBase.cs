using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Affecting Powerup", menuName = "Gameplay/Powerups/Enemy Affecting Powerup")]
public class EnemyAffectingPowerupBase : PowerupBase
{
	public override void ApplyPowerup()
	{
		base.ApplyPowerup();

		Object[] enemies = FindObjectsOfType<EnemyBase>();
		if (enemies.Length == 0)
			return; 

		foreach(Object o in enemies)
		{
			//Do stuff
		}
	}
}
