using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class C_TurretParent : MonoBehaviour {

	public abstract void setCoolDownTime(float coolDownParam);

	public abstract void setAttackRadius(float attackRadiusParam);

	public abstract void setCost(int costParam);

	public abstract void setDamage(float damageParam);

	public abstract void setRangeFromEnemy(float rangeParam);

	public abstract float getCoolDownTime();

	public abstract float getAttackRadius();

	public abstract int getCost();

	public abstract float getDamage();

	public abstract float getRangeFromEnemy();


}
