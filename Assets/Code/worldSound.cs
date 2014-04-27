using UnityEngine;
using System.Collections;

public class worldSound : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	AudioSource[] sounds;

	// Use this for initialization
	void Start () {

		this.gameObject.AddComponent<AudioSource>();
		this.gameObject.AddComponent<AudioSource>();
		this.gameObject.AddComponent<AudioSource>();

		sounds = GetComponents<AudioSource>();

		sounds[0].clip = Resources.Load("sounds/ambiant") as AudioClip;
		sounds[0].playOnAwake = true;
		sounds[0].rolloffMode = AudioRolloffMode.Linear;
		sounds[0].pitch = 1f;
		sounds[0].volume = 0.5f;
		sounds[0].loop = true;
		sounds[0].Play();

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
	
	// Update is called once per frame
	void Update () {

		if (sounds[1].isPlaying==false && sounds[2].isPlaying==false)
		{
			Debug.Log("im herreeee 222222222222222");
			sounds[2].Play();

		}

	}


}
