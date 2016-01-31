using UnityEngine;
using System.Collections;
using System;
using GameUtils;

public class wander : state
{
    public LayerMask enemyMask;
    public float speed = 1;
    public bool flyPattern=true; // false is x-axis, true is y axis YOU HAPPY???? good
    public float maxFlightDistance = 0; // if flight distance is 0, fly foreva, probably should never do.. lol
    float distanceFlown=0;
    Rigidbody2D myBody;
    Transform myTrans;
    BoxCollider2D hitBox;
    float myWidth, myHeight;
    float lastPos;
    bool goingUp = false;
    private Enemy enemy;
    public float time;
    public bool isSee;
    private int dis;
    private int r;

    public void Execute()
    {
        time += Time.deltaTime;
		if (time >= enemy.time [1]) {
			time = 0;
			enemy.idle = true;
			enemy.wander = false;
			enemy.changestate (new idle ());
		}
        seeplayer();
        if (enemy.isAir)
        {
            flyPattern = true;
            hover();
        }
        else
        {
            CheckHead();
            Move();
        }
    }
    public void Enter(Enemy enemy)
    {
		time = 0;
		enemy.wander = true;
		enemy.idle = false;
        Debug.Log("Wander");
        this.enemy = enemy;
        this.enemyMask = enemy.enemyMask;
        r = Mathf.CeilToInt((UnityEngine.Random.Range(0.0F, 1.0F)));
        if (r == 1.0F)
            this.enemy.rightdirection = false;
        else
            this.enemy.rightdirection = true;
        dis = 5;
        myTrans = enemy.transform;
        myBody = enemy.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = enemy.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        speed = enemy.movementSpeed;
    }
    public void Exit(){}
    public void onTriggerEnter(Collider2D other){}
    public void CheckHead()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        lineCastPos.y = lineCastPos.y - (myHeight * 1.2f);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f, enemyMask);
        if (!isGrounded || isBlocked)
        {
            Vector3 currentRot = myTrans.eulerAngles;
            currentRot.y += 180;
            myTrans.eulerAngles = currentRot;
        }
    }
            
    public void Move()
    {
        Vector2 myVel = myBody.velocity;
        if (!enemy.rightdirection)
        {
            myVel.x = myTrans.right.x * -speed;
        }
        else
        {
            myVel.x = myTrans.right.x * speed;
        }
        myBody.velocity = myVel;
    }
    public void seeplayer()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        lineCastPos.y = lineCastPos.y - (myHeight * 1.2f);
        if(!enemy.rightdirection)
        {
            Debug.DrawLine(lineCastPos, lineCastPos + myTrans.right.toVector2() * -dis);
            isSee = Physics2D.Linecast(lineCastPos, lineCastPos + myTrans.right.toVector2() * -dis, enemy.enemyMask);
        }
        else
        {
            Debug.DrawLine(lineCastPos, lineCastPos + myTrans.right.toVector2() * dis);
            isSee = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * dis, enemy.enemyMask);
        }
        if (isSee)
        {
            enemy.changestate(new seeking());
        }
    }
    public void hover()
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
            if (goingUp)
                lineCastPos = myTrans.position.toVector2() + myTrans.up.toVector2() * myWidth - Vector2.up * myHeight;
            else
            {
                lineCastPos = myTrans.position.toVector2() - myTrans.up.toVector2() * myWidth + Vector2.right * myHeight;
                lineCastPos.x -= myWidth;
                lineCastPos.y += myHeight;
            }
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
            myTrans.eulerAngles = currentRot;
            distanceFlown = 0;
        }
        //if hit wall on y-axis ground turn around
        else if ((isBlockedY && flyPattern == true) || (distanceFlown > maxFlightDistance && maxFlightDistance != 0 && flyPattern == true))
        {
            //Vector3 currentRot = myTrans.eulerAngles;
            //currentRot.x += 180;
            //myTrans.eulerAngles = currentRot;
            distanceFlown = 0;
            speed = -speed;
            goingUp = !goingUp;
        }
        //Vertickle movement
        if (flyPattern == true)
        {
            Vector2 myVel = myBody.velocity;
            myVel.y = -myTrans.up.y * speed;
            distanceFlown += Mathf.Abs(lastPos - myTrans.position.y);
            Debug.Log(myVel);
            myBody.velocity = myVel;
            lastPos = myTrans.position.y;
        }
    }
}
