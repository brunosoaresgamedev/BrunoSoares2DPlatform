using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
    
	public GameObject enemy;

	public float waitBetweenSpawn;
	private float spawnCounter;

	public int numberToSpawn;
	private int numberSpawned;



	// Use this for initialization
	void Start () {
		spawnCounter = waitBetweenSpawn;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.J))
		{
			Instantiate(enemy, transform.position, transform.rotation);
		}

		spawnCounter -= Time.deltaTime;

		if(spawnCounter <= 0 && numberSpawned < numberToSpawn)
		{
			Instantiate(enemy, transform.position, transform.rotation);
			spawnCounter = waitBetweenSpawn;
			numberSpawned++;
		} else {
			spawnCounter -= Time.deltaTime;
		}

	}
}