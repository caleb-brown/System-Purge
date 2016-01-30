using UnityEngine;
using System.Collections;

public class GuardLogic : MonoBehaviour {

    public Transform sightStart, sightEnd;

	// Use this for initialization
	void Start ()
    {
        Update();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Raycasting();
        Behaviours();
	}

    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
    }

    void Behaviours()
    {

    }
}
