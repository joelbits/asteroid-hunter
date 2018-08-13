using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

<<<<<<< HEAD
	[SerializeField] Laser laser;
=======
	[SerializeField] GameObject laser;
>>>>>>> Initial commit

	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
		{
<<<<<<< HEAD
			// Vector3 pos = transform.position + transform.forward * laser.Distance;
			laser.FireLaser();
=======
			laser.GetComponent<Laser>().FireLaser();
>>>>>>> Initial commit
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Vector3 pos = transform.position + transform.forward * laser.Distance;
			EventManager.PauseGame();
		}
	}

}
