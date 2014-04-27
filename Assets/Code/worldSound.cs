/// <summary>
/// Is placed on a empty gameobject somewhere in the scene (remember that the audiolistner should be able to here it)
/// </summary>

using UnityEngine;
using System.Collections;

public class worldSound : MonoBehaviour {

	AudioSource[] sounds;												//Creates an Array of the type AudioSource

	void Awake()														//Runs before nything else
	{
		DontDestroyOnLoad(this.gameObject);								//Tells that the object should stay when loading a new level
	}
	
	void Start () 														// Use this for initialization
	{
		this.gameObject.AddComponent<AudioSource>();					//Adds three AudioSource components to the object
		this.gameObject.AddComponent<AudioSource>();
		this.gameObject.AddComponent<AudioSource>();

		sounds = GetComponents<AudioSource>();							//Intializes all AudioSource components in order in an array

		sounds[0].clip = Resources.Load("sounds/ambiant") as AudioClip;	//Intializes a sound from the Resources folder to the Audiosoure on the first placement in the array
		sounds[0].playOnAwake = true;									//Initializes different attributetes in the Audiosoure on the first placement in the array
		sounds[0].rolloffMode = AudioRolloffMode.Linear;
		sounds[0].pitch = 1f;
		sounds[0].volume = 0.5f;
		sounds[0].loop = true;
		sounds[0].Play();												//Plays the sound in the Audiosoure on the first placement in the array

		sounds[1].clip = Resources.Load("sounds/buildingMusic") as AudioClip;
		sounds[1].playOnAwake = true;
		sounds[1].rolloffMode = AudioRolloffMode.Linear;
		sounds[1].pitch = 1f;
		sounds[1].volume = 0.3f;
		sounds[1].loop = false;
		sounds[1].Play();

		sounds[2].clip = Resources.Load("sounds/bgSound") as AudioClip;
		sounds[2].playOnAwake = true;
		sounds[2].rolloffMode = AudioRolloffMode.Linear;
		sounds[2].pitch = 0.9f;
		sounds[2].volume = 0.1f;
		sounds[2].loop = true;
	}

	void Update () 														// Update is called once per frame
	{
		if (sounds[1].isPlaying==false && sounds[2].isPlaying==false) 	//checks if the sound in sounds[1] is finished and the sound in sounds[2] is not playing to ensure that the followin is only run once in the update
		{
			sounds[2].Play(); 											//Starts the sound in sounds[2]; (hint it loops)
		}
	}
}
