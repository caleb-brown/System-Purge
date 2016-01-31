using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundMusic : MonoBehaviour {
	public AudioClip[] mClips;
	int currSong;
	int nextSong;
	// Use this for initialization
	void Start () {
		currSong = 0;
		GetComponent<AudioSource> ().clip = mClips [currSong];
		GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();
		if (Input.GetKeyDown (KeyCode.Minus)) {
			audio.Stop ();
			nextSong = -1;
		}
		if (Input.GetKeyDown (KeyCode.Equals)) {
			audio.Stop ();
			nextSong = 1;
		}
		if (!audio.isPlaying) {
			currSong += nextSong;
			if (currSong == mClips.Length - 1)
				currSong = 0;
			if (currSong < 0)
				currSong = mClips.Length - 1;
			audio.clip = mClips [currSong];
			audio.Play ();
		}
	}
}
