using UnityEngine;
using System.Collections;

public class hover : MonoBehaviour
{

    public LayerMask enemyMask;
    public float speed = 1;
    public bool flyPattern; // false is x-axis, true is y axis YOU HAPPY???? good
    public float maxFlightDistance = 0; // if flight distance is 0, fly foreva, probably should never do.. lol
    float distanceFlown;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    float lastPos;
   

    // Use this for initialization
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lineCastPos;
        //check to see if there's something in front of us before moving forward
        if (flyPattern == false)
        {
            lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
            lineCastPos.y = lineCastPos.y - (myHeight * 1.2f);
        }
            

        else
        {
            lineCastPos = myTrans.position.toVector2() - myTrans.up.toVector2() * myWidth + Vector2.right * myHeight;
            lineCastPos.x -= myWidth;
        }
           

        
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * 0.05f);
        bool isBlockedY = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * 0.05f, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f);
        bool isBlockedX = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f, enemyMask);

        //if hit wall on x-axis ground turn around
        if ((isBlockedX && flyPattern == false) || (distanceFlown > maxFlightDistance && maxFlightDistance != 0 && flyPattern == false))
        {
            Vector3 currentRot = myTrans.eulerAngles;
            currentRot.y += 180;
            print(currentRot.x);
            myTrans.eulerAngles = currentRot;
            distanceFlown = 0;
            print("did it");
        }

        //if hit wall on y-axis ground turn around
        else if((isBlockedY && flyPattern == true) || (distanceFlown > maxFlightDistance && maxFlightDistance != 0 && flyPattern == true))
        {
            Vector3 currentRot = myTrans.eulerAngles;
            currentRot.x += 180;
            print(currentRot.x);
            myTrans.eulerAngles = currentRot;
            print(myTrans.eulerAngles);
            distanceFlown = 0;
            print("did it");
        }





        //Horizontal movement
        if (flyPattern == false)
        {
            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * speed;
            distanceFlown += Mathf.Abs(lastPos - transform.position.x);
            //print(distanceFlown);
            myBody.velocity = myVel;
            lastPos = transform.position.x;

        }
        //Vertickle movement
        if(flyPattern == true)
        {
            Vector2 myVel = myBody.velocity;
            myVel.y = -myTrans.up.y * speed;
            distanceFlown += Mathf.Abs(lastPos - transform.position.y);
            //print(distanceFlown);
            myBody.velocity = myVel;
            lastPos = transform.position.y;
        }
       

    }

}