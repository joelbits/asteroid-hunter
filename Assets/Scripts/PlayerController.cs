using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float moveSpeed = 60;
	[SerializeField] float turnSpeed = 100;

	// [SerializeField] Color playerCrosshairColor = Color.red;
	[SerializeField] float maxDist = 300f;
	[SerializeField] float aimStartWidth = 1.0f;
	[SerializeField] float aimEndWidth = 2.0f;
	[SerializeField] LineRenderer lr;
	[SerializeField] float addedOffset = 4.0f;
	// [SerializeField] GameObject chImg;
	[SerializeField] GameObject playerShip;
	[SerializeField] GameObject laserGameObject;
	private Vector3 laserPos;

	Transform myT;

	void Start () 
	{
		// DrawCrossHair();
		// lr.enabled = false;
	}

	void Awake() 
	{
		myT = transform;
		lr = GetComponent<LineRenderer>();
		laserPos = laserGameObject.transform.position;
		lr.startWidth = aimStartWidth;
		lr.endWidth = aimEndWidth;
	}


	// Update is called once per frame
	void Update() {
<<<<<<< HEAD
		TurnOffLine();
=======
>>>>>>> Initial commit
		DrawCrossHair();
		Thrust();
		Turn();
	}


	void DrawCrossHair()
	{
		Vector3 srcPos = transform.position + (transform.forward * addedOffset);
		Vector3 aimPos = transform.position + (transform.forward * maxDist);

		lr.SetPosition(0, srcPos);
		lr.SetPosition(1, aimPos);
		lr.enabled = true;
		
		// Invoke("TurnOffLine", 5.0f);
	}


<<<<<<< HEAD
	void TurnOffLine()
	{
		lr.enabled = false;
	}


=======
>>>>>>> Initial commit
	void Thrust() 
	{
		myT.position += myT.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
		// Debug.Log("myT.pos = " + myT.position);
	}


	void Turn() 
	{
		float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
		float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");
		myT.Rotate(pitch, yaw, roll);
	}

}
