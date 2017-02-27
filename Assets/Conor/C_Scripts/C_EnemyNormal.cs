﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyNormal : C_EnemyParent {


	public float speed;
	public float health;

	GameObject pathNode;
	Transform targetPathNode;
	private int pathNodeIndex;

	public void Start () 
	{
		setSpeed(speed);
		setHealth(health);
		pathNode = GameObject.Find("Path");
	}

	public void Update () 
	{
		moveTowardsNode();
	}

	/**
	* Set the speed of the enemy
	*/
	public override void setSpeed(float speedParam)
    {
		speed = speedParam;
    }

	/**
	* Set the enemies health 
	*/
	public override void setHealth(float healthParam)
    {
        health = healthParam;
    }

	/**
	* Get the enemies speed
	*/
	public override float getSpeed()
    {
        return speed;
    }

	/**
	* Get the enemies health
	*/
	public override float getHealth()
    {
        return health;
    }

	/**
	* Get the next node in the path
	*/
	void GetNextPathNode() {
		if(pathNodeIndex < pathNode.transform.childCount) 
		{
			targetPathNode = pathNode.transform.GetChild(pathNodeIndex);
			pathNodeIndex++;
		}
		else 
		{
			targetPathNode = null;
			ReachedGoal();
		}
	}

	/**
	* Enemey has reached the final node in the path of nodes
	*/
	void ReachedGoal()
	{
		//		GameObject.FindObjectOfType<ScoreManager>().LoseLife();
		//Destroy(gameObject);
	}

	/**
	* Move the enemy towards the next node
	*/
	void moveTowardsNode()
	{
		if(targetPathNode==null)
		{
			GetNextPathNode();
			if(targetPathNode== null)
			{
				// No More path
				ReachedGoal();
				return;
			}	
		}
		Vector3 direction =  targetPathNode.position- this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if(direction.magnitude<= distThisFrame)
		{
			//Reached the node 
			targetPathNode = null;
		}
		else
		{
			//TODO:Smooth Motion
			//Move towards node
			transform.Translate(direction.normalized * distThisFrame,Space.World);
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation,targetRotation,Time.deltaTime*5);
		}

	}

}
