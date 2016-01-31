using UnityEngine;
using System.Collections;
using System;
using GameUtils;

public class seeking : state
{
    public Enemy enemy;
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    private int dis = 5;
    public bool isSee;
    public Collider2D p;

    public void Enter(Enemy enemy)
    {
        Debug.Log("seeking");
        this.enemy = enemy;
        init(); 
    }

    public void Execute()
    {
        seeplayer();
        if (!isSee)
            enemy.changestate(new idle());
        else
            Chase();
    }
    public void Exit(){}
    public void onTriggerEnter(Collider2D other){}
    public void init()
    {
        this.enemyMask = enemy.enemyMask;
        myTrans = enemy.transform;
        myBody = enemy.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = enemy.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        speed = enemy.movementSpeed;
    }
    public void seeplayer()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        lineCastPos.y = lineCastPos.y - (myHeight * 1.2f);
        Vector3 currentRot = myTrans.eulerAngles;
        if (enemy.rightdirection)
        {
            Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * dis);
            isSee = Physics2D.Linecast(lineCastPos, lineCastPos + myTrans.right.toVector2() * dis, enemy.enemyMask);
        }
        else
        {
            Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * dis);
            isSee = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * dis, enemy.enemyMask);
        }
    }
    public void Chase()
    {
        Vector2 myVel = myBody.velocity;
        if(enemy.rightdirection)
            myVel.x = myTrans.right.x * speed;
        else
            myVel.x = myTrans.right.x * -speed;
        myBody.velocity = myVel;
    }
}
