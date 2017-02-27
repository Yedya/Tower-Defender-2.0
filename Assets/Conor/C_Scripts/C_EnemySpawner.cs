using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemySpawner : MonoBehaviour {


	float spawnBreak = 2.25f; // Time between spawns
	float spawnTimeRemaing =4 ; // Spawn time remaining

	[System.Serializable]
	public class WaveComponent{
		public GameObject enemyPrefab;
		public int num;
		[System.NonSerialized]
		public int spawned =0;

	}

	public WaveComponent[] waveComps;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		spawnTimeRemaing -= Time.deltaTime;
		if(spawnTimeRemaing<0)
		{
			spawnTimeRemaing = spawnBreak;
			bool didSpawn=false;

			//Go Through the wave comps until we find something to spawn
			foreach(WaveComponent wc in waveComps)
			{
				if(wc.spawned < wc.num)
				{
					//Spawn the enemies
					wc.spawned++;

					Instantiate(wc.enemyPrefab,this.transform.position,this.transform.rotation);

					didSpawn = true;
					break;
				}
			}
			if(didSpawn==false) //Wave complete
			{
				if(transform.parent.childCount>1)
				{
					transform.parent.GetChild(1).gameObject.SetActive(true);  // Get second child
				}
				else
				{
					// That was the last wave -- what do we want to do?
					// What if instead of DESTROYING wave objects,
					// we just made them inactive, and then when we run
					// out of waves, we restart at the first one,
					// but double all enemy HPs or something?
				}
					
				//ToDo:Spawn Next wave
				Destroy(gameObject);
			}
		}

	}


}
