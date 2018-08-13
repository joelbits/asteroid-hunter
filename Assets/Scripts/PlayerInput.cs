using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	[SerializeField] GameObject laser;

	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
		{
			laser.GetComponent<Laser>().FireLaser();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Vector3 pos = transform.position + transform.forward * laser.Distance;
			EventManager.PauseGame();
		}
	}

}
