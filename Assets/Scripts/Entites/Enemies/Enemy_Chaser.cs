﻿using UnityEngine;
using System.Collections;

public class Enemy_Chaser : Enemy<Enemy_Chaser> {

	public float pauseTime, chaseTime;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().freezeRotation = true;
		setStats (15, 5, 0, 2, 20, 0, 0);
		fsm = new StateMachine<Enemy_Chaser> (this);
		fsm.changeState (new Chaser_RunToPlayer());
		pauseTime = 3f;
		chaseTime = 5f;
	}

	public bool checkStateChange(float _startTime, float _timeInterval){
		if(Time.time > _startTime + _timeInterval){
			return true;
		}
		return false;
	}

}

public class Chaser_RunToPlayer : State<Enemy_Chaser>{
	float startTime;

	public void enter(Enemy_Chaser agent){
		startTime = Time.time;
	}

	public void execute(Enemy_Chaser agent){
		agent.runToPlayer ();
		if(agent.checkStateChange(startTime, agent.chaseTime)){
			agent.fsm.changeState (new Chaser_Pause());
		}
	}

	public void exit(Enemy_Chaser agent){
	}
}

public class Chaser_Pause : State<Enemy_Chaser>{
	float startTime;

	public void enter(Enemy_Chaser agent){
		startTime = Time.time;
	}

	public void execute(Enemy_Chaser agent){
		if(agent.checkStateChange(startTime, agent.pauseTime)){
			agent.fsm.changeState (new Chaser_RunToPlayer());
		}
	}

	public void exit(Enemy_Chaser agent){
	}
}