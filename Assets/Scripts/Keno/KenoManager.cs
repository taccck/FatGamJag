using UnityEngine;
using System.Collections.Generic;

public class KenoManager : MonoBehaviour
{
	public static KenoManager Instance;

	public PowerupBase[] LevelUpPowerups;
	public PowerupBase[] PickupPowerups;

	private List<PowerupBase> ActivePowerups = new List<PowerupBase>(); 

	public void OnKenoCalled(KenoType type)
	{
		switch (type)
		{
			case KenoType.LevelUp: PermanentKeno(); break;
			case KenoType.Pickup: TemporaryKeno(); break; 
		}	
	}

	private void PermanentKeno()
	{
		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0, LevelUpPowerups.Length);
			ActivePowerups.Add(LevelUpPowerups[rand]);
			LevelUpPowerups[rand].ApplyPowerup();
		}

		StartCoroutine(KenoMachine.Instance.OnKenoStart(ActivePowerups));
		//StartAllTimers(); 
	}

	private void TemporaryKeno()
	{	
		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0, PickupPowerups.Length);
			ActivePowerups.Add(PickupPowerups[rand]); //Temp change
			PickupPowerups[rand].ApplyPowerup(); //Temp change - Change back to rand
		}

		StartCoroutine(KenoMachine.Instance.OnKenoStart(ActivePowerups));
		//StartAllTimers(); 
	}

	private void StartAllTimers()
	{
		foreach(PowerupBase pb in ActivePowerups)
		{
			if(pb as DurationPowerupBase)
			{
				DurationPowerupBase dpb = pb as DurationPowerupBase;
				dpb.StartTimer(); 
			}
		}
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		//OnKenoCalled(KenoType.Pickup); 
	}

	public enum KenoType
	{
		LevelUp, Pickup
	}
}
