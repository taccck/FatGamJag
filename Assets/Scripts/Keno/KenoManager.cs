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
		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0, AllPowerups.Length); 
			TempImgs[i].transform.Find("Text (Legacy)").GetComponent<Text>().text = AllPowerups[rand].PowerupName;
			AllPowerups[rand].ApplyPowerup(); 
		}
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
