using UnityEngine;
using System.Collections;

public class FlyingAI : MonoBehaviour {



    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;

    // Use this for initialization
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }
   
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        //check to see if there's ground in front of us before moving forward
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        //bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2(), enemyMask);

        //if no ground turn around
        if(!isGrounded)
        {
            Vector3 currentRot = myTrans.eulerAngles;
            currentRot.y += 180;
            myTrans.eulerAngles = currentRot;
        }


        //always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
	}

}
