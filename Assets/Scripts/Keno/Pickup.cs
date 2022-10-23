using UnityEngine;

public class Pickup : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.name == "Volvo240")
		{
			KenoManager.Instance.StartCoroutine(other.GetComponent<VolvoKeno>().StartKeno());
			Destroy(gameObject); 
		}
	}
}
