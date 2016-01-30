using UnityEngine;
using System.Collections;
using GameUtils;
using System;

public class Enemy : Controller{
    public bool isAir;
    private state currentState;
    public LayerMask enemyMask;
    // Use this for initialization
    void Start ()
    {

        isAir = false;
        print("start");
        changestate(new idle());
        
	}
	// Update is called once per frame
	void FixedUpdate ()
    {
        currentState.Execute();
	}
    public void changestate(state newstate)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newstate;
        currentState.Enter(this);   
    }
}
