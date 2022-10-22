using UnityEngine;
using UnityEngine.UI; //TEMP
using System.Collections.Generic;

public class KenoManager : MonoBehaviour
{
	public static KenoManager Instance;

	public PowerupBase[] AllPowerups;

	[SerializeField]
	private GameObject[] TempImgs;
	
	public void OnKenoCalled()
	{
		List<string> powerupNames = new List<string>(); 
		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0, AllPowerups.Length);
			string pName = AllPowerups[rand].PowerupName;
			powerupNames.Add(pName); 
			AllPowerups[rand].ApplyPowerup(); 
		}

		StartCoroutine(KenoMachine.Instance.OnKenoStart(powerupNames)); 
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	private void Start()
	{
		OnKenoCalled(); 
	}
}
