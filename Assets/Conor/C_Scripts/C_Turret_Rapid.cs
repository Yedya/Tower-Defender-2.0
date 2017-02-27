using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Turret_Rapid : C_TurretParent {

	private Transform turretTrasform ;
	public GameObject bulletPrefab;

	float fireCoolDownLeft;
	public float fireCooldown;
	public float attackRadius;
	public float damage;
	public int cost;
	public float rangeFromEnemy;

	private Vector3 enemyDirection;
	private Quaternion lookRot ;
	private Vector3 initDir = new Vector3(0,0);
	private Quaternion initRot = new Quaternion(0.0f,0.0f,0.0f,0.0f);


	// Use this for initialization
	void  Start () 
	{
		turretTrasform = transform.Find("Turret");
		setCoolDownTime(fireCooldown);
		setAttackRadius(attackRadius);
		setCost(cost);
		setDamage(damage);
		setRangeFromEnemy(rangeFromEnemy);

	}

	// Update is called once per frame
	void Update () 
	{
		scanForEnemies();
	}


	/**
	* Set Cooldown time
	*/
	public override void setCoolDownTime(float coolDownParam)
	{
		fireCooldown = coolDownParam;
	}

	/**
	* Set attack radius for turret
	*/
	public override void setAttackRadius(float attackRadiusParam)
	{
		attackRadius = attackRadiusParam;
	}

	/**
	* Set the buying cost for a turret
	*/
	public override void setCost(int costParam)
	{
		cost = costParam;
	}

	/**
	* Set the damage for a turret
	*/
	public override void setDamage(float damageParam)
	{
		damage = damageParam;
	}

	/**
	* Set the range, the range would be the distance from the turret to the enemy
	*/
	public override void setRangeFromEnemy(float rangeParam)
	{
		rangeFromEnemy = rangeParam;
	}

	/**
	* Set the buying cost for a turret
	*/
	public override float getCoolDownTime()
	{
		return fireCooldown;
	}

	/**
	* Get the attack radius
	*/
	public override float getAttackRadius()
	{
		return attackRadius;
	}

	/**
	* Get the the cost
	*/
	public override int getCost()
	{
		return cost;
	}

	/**
	* Get the the damage
	*/
	public override float getDamage()
	{
		return damage;
	}

	/**
	* Get the the range
	*/
	public override float getRangeFromEnemy()
	{
		return rangeFromEnemy;
	}

	/**
	* Function to scan for enemies within the attack radius
	*/
	public void scanForEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearestEnemy = null;


		foreach(GameObject e in enemies)
		{
			float distanceFromEnemy = Vector3.Distance(this.transform.position, e.transform.position);

			if(nearestEnemy== null && distanceFromEnemy<attackRadius)	
			{
				nearestEnemy = e;
				//				attackRadius = distanceFromEnemy;
			}
			if(nearestEnemy== null)	
			{
				changeToInitialState();
				return;
			}
		}


		enemyDirection = (nearestEnemy.transform.position - this.transform.position);
		lookRot = Quaternion.LookRotation(enemyDirection);

		turretTrasform.transform.rotation = Quaternion.Lerp(turretTrasform.transform.rotation, lookRot, Time.deltaTime*2);

//		Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime*2);
		//turretTrasform.transform.rotation = Quaternion.Euler( lookRot.eulerAngles.x, lookRot.eulerAngles.y, Time.deltaTime*0.2f );
		fireCoolDownLeft -= Time.deltaTime;

		if(fireCoolDownLeft<=0 && enemyDirection.magnitude <=attackRadius)
		{
			//			fireCoolDownLeft = fireCooldown;
			//			ShootAt(nearestEnemy);
		}

	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}

	/**
	* Move the turret position back to its initial state after following an enemies position
	*/
	void changeToInitialState()
	{
		initRot = Quaternion.LookRotation(initDir);
		turretTrasform.transform.rotation = Quaternion.Lerp(turretTrasform.transform.rotation,initRot,Time.deltaTime*2);
	}

}
