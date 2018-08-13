using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

	[SerializeField] GameObject asteroidPrefab;
	[SerializeField] int maxAsteroidsOnAxis = 5;
	[SerializeField] int gridSpacing = 50;

	public List<Asteroid> asteroid = new List<Asteroid>();


	void Start() {
		// PlaceAsteroids();
	}


	void OnEnable() 
	{
		EventManager.onStartGame += PlaceAsteroids;
		EventManager.onPlayerDeath += DestroyAsteroids;
	}


	void OnDisable() 
	{
		EventManager.onStartGame -= PlaceAsteroids;
		EventManager.onPlayerDeath -= DestroyAsteroids;
	}


	void DestroyAsteroids()
	{
		foreach(Asteroid ast in asteroid)
		{
			if (ast != null)
				ast.SelfDestruct();
		}
		asteroid.Clear();
	}


	void RefreshAsteroidsList()
	{
		GameObject[] allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		foreach (GameObject obj in allAsteroids)
		{
			Asteroid tmpComp = obj.GetComponent<Asteroid>();

			if (tmpComp != null)
				asteroid.Add(tmpComp);
		}

		// Debug.Log("Refreshed asteroid list: " + asteroid.Count);
		// asteroid.Add(go.gameObject.GetComponent<Asteroid>()); 
	}


	void PlaceAsteroids()
	{
		DestroyAsteroids();
		
		for (int x=0; x < maxAsteroidsOnAxis; x++)
		{
			for (int y=0; y < maxAsteroidsOnAxis; y++)
			{
				for (int z=0; z <  maxAsteroidsOnAxis; z++)
				{
					InstantiateAsteroid(x, y, z);
				}
			}
		}
	}


	void InstantiateAsteroid(int x, int y, int z)
	{
		GameObject go = Instantiate(asteroidPrefab, 
					new Vector3(
						transform.position.x + (x * gridSpacing) + AsteroidOffset(), 
						transform.position.y + (y * gridSpacing) + AsteroidOffset(), 
						transform.position.z + (z * gridSpacing) + AsteroidOffset()), 
					Quaternion.identity, 
					transform);

		asteroid.Add(go.gameObject.GetComponent<Asteroid>()); 
	}


	float AsteroidOffset()
	{
		return Random.Range(-gridSpacing/2f, gridSpacing/2f);
	}

}
