using UnityEngine;
using System.Collections;
using System;
using GameUtils;

public class idle : state
{
    private ISceneObject manager;
    private Enemy enemy;
    private float time = 0;
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
        if (time >= 3)
            enemy.changestate(new wander());
    }
}
