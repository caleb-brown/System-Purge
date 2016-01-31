using UnityEngine;
using System.Collections;

public class soundTest : MonoBehaviour {
	public AudioClip mClip;
	int playing = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((GameObject.Find ("Player").transform.position - transform.position).magnitude < 1.0) {
			AudioSource.PlayClipAtPoint (mClip, transform.position, 1.0f);
		}
	}
}
