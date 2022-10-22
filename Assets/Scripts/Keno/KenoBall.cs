using UnityEngine;


public class KenoBall : MonoBehaviour
{
	private Transform TextPivot;

	private void Start()
	{
		TextPivot = transform.Find("TextPivot"); 
	}

	private void Update()
	{
		UpdateTextOrientation(); 
	}

	private void UpdateTextOrientation()
	{
		Vector3 dir = (TextPivot.position - Camera.main.transform.position).normalized;

		TextPivot.forward = -dir; 
	}
}
