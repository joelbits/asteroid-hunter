using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float spawnTimer = 5f;
	[SerializeField] int maxEnemiesCount = 2;
	[SerializeField] int curEnemiesCount = 0;
	[SerializeField] List<GameObject> enemy;
	[SerializeField] GameObject[] spawnPoints;
	[SerializeField] float spawnOffset = 300.0f;
	private float lastSpawnOffset = 250.0f;
	private float lastSpawnOffsetV = 250.0f;


	void Start() {
		// StartSpawning();
	}


	void OnEnable() 
	{
		EventManager.onStartGame += StartSpawning;
		EventManager.onPlayerDeath += StopSpawning;
	}


	void OnDisable() 
	{
		// StopSpawning();
		EventManager.onStartGame -= StartSpawning;
		EventManager.onPlayerDeath -= StopSpawning;
	}


	void CountCurrentEnemies()
	{
		curEnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
	}


	void SpawnEnemyDelayed()
	{
		CountCurrentEnemies();

		Invoke("CreateAnEnemy", Random.Range(1f, 3f));

		CountCurrentEnemies();
	}


	void CreateAnEnemy() 
	{
		if (curEnemiesCount >= maxEnemiesCount)
			return;
			
		Transform spawnPoint;
		spawnPoint = GetRandomSpawnPoint(); // From all global spawnpoints

		float randOffsetH = Random.Range(0f, 3000f);
		float randOffsetV = Random.Range(0f, 3000f);
		float randOffsetZ = Random.Range(0f, 3000f);

		Vector3 finalPos = spawnPoint.position + 
			(spawnPoint.forward * randOffsetZ) + 
			(spawnPoint.right * randOffsetH) + 
			(spawnPoint.up * randOffsetV);

		//TODO: Fix random rotation for Enemy instances' 
		Quaternion randomRotation = Quaternion.Euler( Random.Range(0, 360), 
														Random.Range(0, 360), 
														Random.Range(0, 360) );

		GameObject enemyInstance = Instantiate(enemyPrefab, finalPos, spawnPoint.rotation * randomRotation);
		// enemyInstance.transform.Rotate(new Vector3(Random.Range(0, 360), 0, 0), Space.World);

		enemyInstance.transform.rotation = enemyInstance.transform.rotation * randomRotation;

		enemy.Add(enemyInstance);

		CountCurrentEnemies();
	}


	Transform GetRandomSpawnPoint()
	{
		GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

		// TODO: Check if objects are active to only spawn from active points

		int randomPoint = Random.Range(0, spawnerObjects.Length);

		GameObject randGo = (GameObject) spawnerObjects.GetValue(randomPoint);
		return randGo.transform;
	}


	public void RemoveEnemy(GameObject target)
	{
		enemy.Remove(target);
		// enemy.Clear();
		CountCurrentEnemies();
	}


	// void SpawnEnemy()
	// {
	// 	Instantiate(enemyPrefab, transform.position, Quaternion.identity);
	// }


	void StartSpawning()
	{
		curEnemiesCount = 0;
		InvokeRepeating("SpawnEnemyDelayed", spawnTimer, spawnTimer);
	}
	 

	void StopSpawning()
	{
		CancelInvoke();
	}
}
