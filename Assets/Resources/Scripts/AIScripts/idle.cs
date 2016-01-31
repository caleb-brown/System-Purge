using UnityEngine;
using System.Collections;
using System;
using GameUtils;

public class idle : state
{
    private ISceneObject manager;
    private Enemy enemy;
    public float time;
    private Rigidbody2D myBody;

    public void Execute()
    {
        Idle();
    }
    public void Enter(Enemy enemy)
    {
        Debug.Log("idle");
        this.enemy = enemy;
    }
    public void Exit(){}
    public void onTriggerEnter(Collider2D other){}
    public void Idle()
    {
        time += Time.deltaTime;
        if (time >= enemy.time[0])
            enemy.changestate(new wander());
    }
}
