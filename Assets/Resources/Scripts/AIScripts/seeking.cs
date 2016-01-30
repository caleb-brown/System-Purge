using UnityEngine;
using System.Collections;
using System;

public class seeking : state {
    private Enemy enemy;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

    }
    
    public void Exit()
    {
    }
    
    public void onTriggerEnter(Collider2D other)
    {
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
