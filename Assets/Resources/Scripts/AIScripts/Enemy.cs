using UnityEngine;
using System.Collections;
using GameUtils;
using System;

public class Enemy : Controller, ISceneObject{

    private state currentState;
	// Use this for initialization
	void Start ()
    {
        print("start");
        changestate(new idle());
	}
	
	// Update is called once per frame
	void Update ()
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

    public void ObjectUpdate()
    {
    }

    public void Initialize()
    {

    }
}
