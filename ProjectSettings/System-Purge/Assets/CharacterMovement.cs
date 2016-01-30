using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public bool grounded = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	
	}

    void Movement()
    {
        //Is going to handle all the character movement
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 4f * Time.deltaTime);  //Makes it to where the player can travel to the right
            transform.eulerAngles = new Vector2(0,0); //this allows the "rotation" of the sprite, facing right / left
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * -4f * Time.deltaTime);  //Makes it to where the player can travel to the left
            transform.eulerAngles = new Vector2(0, 180);
        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector2.up * Time.deltaTime);  //Makes it to where the player can jump up
            transform.eulerAngles = new Vector2(90, 0);
        }
        */

    }
}
