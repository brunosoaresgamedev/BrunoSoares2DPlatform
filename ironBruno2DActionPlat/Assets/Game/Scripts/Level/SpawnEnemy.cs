using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
	public GameObject enemy;
	public static SpawnEnemy instanceSpawnEnemy;
	public float waitBetweenSpawn;
	private float spawnCounter;

	public int numberToSpawn;

	public int MaxSpawnNumber;
	public int CurrentSpawnNumber;



	private int numberSpawned;
	[SerializeField]
	Transform spawntransform1, spawntransform2, spawntransform3, spawntransform4;
	private PlayerController thePlayer;
	private void Awake()
	{
		if (instanceSpawnEnemy == null)
		{
			instanceSpawnEnemy = this;

		}
		else if (instanceSpawnEnemy != this)
		{
			Destroy(gameObject);
		}


	}
	// Use this for initialization
	void Start()
	{
		spawnCounter = waitBetweenSpawn;
		thePlayer = FindObjectOfType<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!thePlayer.gameObject.activeSelf)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			Instantiate(enemy, spawntransform1.transform.position, spawntransform1.transform.rotation);
			Instantiate(enemy, spawntransform2.transform.position, spawntransform2.transform.rotation);
			Instantiate(enemy, spawntransform3.transform.position, spawntransform3.transform.rotation);
			Instantiate(enemy, spawntransform4.transform.position, spawntransform4.transform.rotation);
		}

		spawnCounter -= Time.deltaTime;

		if (spawnCounter <= 0 && numberSpawned < numberToSpawn && (CurrentSpawnNumber == 0))
		{
			Instantiate(enemy, spawntransform1.transform.position, spawntransform1.transform.rotation);
			Instantiate(enemy, spawntransform2.transform.position, spawntransform2.transform.rotation);
			Instantiate(enemy, spawntransform3.transform.position, spawntransform3.transform.rotation);
			Instantiate(enemy, spawntransform4.transform.position, spawntransform4.transform.rotation);
			spawnCounter = waitBetweenSpawn;
			numberSpawned++;
			CurrentSpawnNumber+=4;
		}
		else
		{
			spawnCounter -= Time.deltaTime;
		}
		if (CurrentSpawnNumber < 0)
		{
			CurrentSpawnNumber = 0;
		}
	}


	
}
