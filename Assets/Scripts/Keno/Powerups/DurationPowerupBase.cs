using UnityEngine;
using System.Collections;

public abstract class DurationPowerupBase : PowerupBase
{
	public float DurationSeconds;

	public virtual void StartTimer() { }
}
