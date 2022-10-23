using UnityEngine;
using UnityEngine.InputSystem;

public class CarAudio : MonoBehaviour
{
	[SerializeField]
	private AudioClip Idle;
	[SerializeField]
	private AudioClip FstAccel;
	[SerializeField]
	private AudioClip FstLoop;
	[SerializeField]
	private AudioClip FstDecel;
	[SerializeField]
	private AudioClip SndAccel;
	[SerializeField]
	private AudioClip SndLoop;
	[SerializeField]
	private AudioClip SndDecel;
	
	private AudioSource Source;
	private GameObject Car;

	private float CurrentSpeed;
	private float MaxSpeed;


	private void Start()
	{
		Source = GetComponent<AudioSource>();
		MaxSpeed = VolvoConfig.Get.currMaxSpeed;
		Car = GameObject.Find("Volvo240");
	}

	private void Update()
	{
		if (Car)
			CurrentSpeed = Car.GetComponent<Rigidbody>().velocity.magnitude;
		
		if (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed) //Accelerating
		{
			if (Source.clip == Idle)
			{
				Source.clip = FstAccel;
				Source.loop = false; 
				Source.Play();
			}
			else if(!Source.isPlaying && Source.clip == FstDecel)
			{
				Source.clip = FstAccel;
				Source.Play(); 
			}
			else if (!Source.isPlaying && Source.clip == FstAccel)
			{
				Source.clip = SndAccel;				
				Source.Play();
			}
			else if (!Source.isPlaying && Source.clip == SndDecel)
			{
				Source.clip = SndAccel;
				Source.Play();
			}
			else if (!Source.isPlaying && Source.clip == SndAccel)
			{
				Source.clip = FstLoop;
				Source.Play();
			}
			else if(!Source.isPlaying && Source.clip == FstLoop)
			{
				Source.clip = SndLoop;
				Source.loop = true; 
				Source.Play(); 
			}
		}
		else //Decelerating
		{
			if (Source.clip == SndLoop)
			{
				Source.clip = SndDecel;
				Source.loop = false; 
				Source.Play();
			}
			else if(!Source.isPlaying && Source.clip == SndAccel)
			{
				Source.clip = SndDecel;
				Source.Play(); 
			}
			else if (!Source.isPlaying && Source.clip == SndDecel)
			{
				Source.clip = FstDecel;
				Source.Play();
			}
			else if(!Source.isPlaying && Source.clip == FstAccel)
			{
				Source.clip = FstDecel;
				Source.Play(); 
			}
			else if (!Source.isPlaying && Source.clip == FstDecel)
			{
				Source.clip = Idle;
				Source.loop = true; 
				Source.Play();
			}			
		}
	}
}

