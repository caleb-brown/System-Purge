using UnityEngine;
using System.Collections;
using System;
using GameUtils;

public class wander : state
{
    private Enemy enemy;
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    private float time=0;
    public bool isSee;
    private int dis;

    public void Execute()
    {   
        time += Time.deltaTime;
        if (time >= 10)
            enemy.changestate(new idle());
        seeplayer();
        CheckHead();
        Move();
    }

    public void Enter(Enemy enemy)
    {
        Debug.Log("Wander");
        time = 0;
        dis = 5;
        this.enemy = enemy;
        this.enemyMask = enemy.enemyMask;
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
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }
    public void seeplayer()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        lineCastPos.y = lineCastPos.y - (myHeight * 1.2f);
        if(enemy.rightdirection)
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
}
