using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputController : MonoBehaviour {


	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			// EventManager.EndGame();
			EventManager.StartGame();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// EventManager.EndGame();
			EventManager.PauseGame();
		}
	}
	
}
