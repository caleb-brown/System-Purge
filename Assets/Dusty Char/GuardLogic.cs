using UnityEngine;
using System.Collections;

public class GuardLogic : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public bool spotted = false;
    public bool facingLeft = false;
    public GameObject arrow;


	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Patrol", 0f, Random.Range(2f,6f));
	
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
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player")); //Whatever you're wanting to cast to, should be the 'NameToLayer' option (Third parameter)
        
       
    }

    void Behaviours()
    {
        if (spotted == true)
            arrow.SetActive(true);
        else
            arrow.SetActive(false);
    }

    void Patrol()
    {
       
        facingLeft = !facingLeft;

        if (facingLeft == true)
            transform.eulerAngles = new Vector2(0, 0);
        else
            transform.eulerAngles = new Vector2(0, 180);
    }
}
