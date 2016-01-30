using UnityEngine;
using System.Collections;
using System;

public class idle : state {
    private Enemy enemy;
    private float time = 0;
    public void Execute()
    {
        time += Time.deltaTime;
        if (time >= 1)
            enemy.changestate(new wander());
    }

    public void Enter(Enemy enemy)
    {
        Debug.Log("idle");
        time = 0;
        this.enemy = enemy;
    }

    public void Exit()
    {
    }

    public void onTriggerEnter(Collider2D other)
    {
    }
}
