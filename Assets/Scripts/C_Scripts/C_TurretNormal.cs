using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TurretNormal : C_TurretParent {

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

	public override void setCoolDownTime(float coolDownParam)
	{
		fireCooldown = coolDownParam;
	}

	public override void setAttackRadius(float attackRadiusParam)
	{
		attackRadius = attackRadiusParam;
	}

	public override void setCost(int costParam)
	{
		cost = costParam;
	}

	public override void setDamage(float damageParam)
	{
		damage = damageParam;
	}

	public override void setRangeFromEnemy(float rangeParam)
	{
		rangeFromEnemy = rangeParam;
	}

	public override float getCoolDownTime()
	{
		return fireCooldown;
	}

	public override float getAttackRadius()
	{
		return attackRadius;
	}

	public override int getCost()
	{
		return cost;
	}

	public override float getDamage()
	{
		return damage;
	}

	public override float getRangeFromEnemy()
	{
		return rangeFromEnemy;
	}

	public void scanForEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject nearestEnemy = null;


		foreach(GameObject e in enemies)
		{
			float distanceFromEnemy = Vector3.Distance(this.transform.position, e.transform.position);
			Debug.Log(distanceFromEnemy);
			if(nearestEnemy== null && distanceFromEnemy<attackRadius)	
			{
				nearestEnemy = e;
//				attackRadius = distanceFromEnemy;
			}
			if(distanceFromEnemy>attackRadius)	
			{
				changeToInitialState();
				return;
			}
		}


		enemyDirection = (nearestEnemy.transform.position - this.transform.position);
		lookRot = Quaternion.LookRotation(enemyDirection);
		turretTrasform.transform.rotation = Quaternion.Euler( lookRot.eulerAngles.x, lookRot.eulerAngles.y, Time.deltaTime*1.2f );
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

	void changeToInitialState()
	{
		initRot = Quaternion.LookRotation(initDir);
		turretTrasform.transform.rotation = Quaternion.Lerp(turretTrasform.transform.rotation,initRot,Time.deltaTime*2);
	}

}
