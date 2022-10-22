using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

[CreateAssetMenu(fileName = "New Health Duration Powerup", menuName = "Gameplay/Duration Powerups/Health Duration Powerup")]
public class HealthDurationPowerupBase : DurationPowerupBase
{
	[HideInInspector]
	public static Coroutine Timer;
	[HideInInspector]
	public static float CurrSeconds; 

	public override void ApplyPowerup()
	{
		base.ApplyPowerup();
		VolvoConfig.Get.currHealth = int.MaxValue;
		CurrSeconds += DurationSeconds;		
	}

	public override void StartTimer()
	{
		if (Timer != null)
			return;

		Timer = VolvoConfig.Get.SomePlayerComponent.StartCoroutine(DurationTimer()); 
	}

	private IEnumerator DurationTimer()
	{
		Debug.Log("Duration: " + CurrSeconds);
		yield return new WaitForSeconds(CurrSeconds);
		VolvoConfig.Get.currHealth = VolvoConfig.Get.baseHealth;
		Debug.Log("Duration Over"); 
	}
}
