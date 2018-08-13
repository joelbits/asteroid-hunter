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
		// Debug.Log("Counting enemies: " + curEnemiesCount);
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
		spawnPoint = GetRandomSpawnPoint();

		GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
		enemy.Add(enemyInstance);
		// Debug.Log("Created and added enemy to list, cur length: " + enemy.Count);
		CountCurrentEnemies();
	}


	Transform GetRandomSpawnPoint()
	{
		GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

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
