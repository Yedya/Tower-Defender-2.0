using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class C_EnemyParent : MonoBehaviour {

	public abstract void setSpeed(float speedParam);

    public abstract void setHealth(float speedParam);

    public abstract float getSpeed();

    public abstract float getHealth();

    public void die()
	{
		
	}
}
