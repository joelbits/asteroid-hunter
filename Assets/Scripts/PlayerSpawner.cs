using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject playerInstance;
	public GameObject motherShipInstance;
	[SerializeField] GameObject motherShipPrefab;
	[SerializeField] GameObject motherShipSpawnPos;
	[SerializeField] GameObject playerPrefab;
	[SerializeField] GameObject playerStartPos;

	
	void Awake() 
	{
		// ResetPlayer();
	}

	
	void OnEnable() 
	{
		EventManager.onStartGame += ResetPlayer;
		EventManager.onPlayerDeath += RemovePlayer;
	}


	void OnDisable() 
	{
		EventManager.onStartGame -= ResetPlayer;
		EventManager.onPlayerDeath -= RemovePlayer;
	}


	void ResetPlayer()
	{
		if (playerInstance != null)
			Destroy(playerInstance);
		playerInstance = Instantiate(playerPrefab, playerStartPos.transform.position, playerStartPos.transform.rotation);

		if (motherShipInstance != null)
			Destroy(motherShipInstance);
		motherShipInstance = Instantiate(motherShipPrefab, motherShipSpawnPos.transform.position, motherShipSpawnPos.transform.rotation);
	}


	void RemovePlayer()
	{
		if (playerInstance != null)
			Destroy(playerInstance);
		playerInstance = null;
	}


}
