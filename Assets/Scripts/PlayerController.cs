using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] GameObject playerShip;
	[SerializeField] GameObject laserGameObject;

	[SerializeField] float moveSpeed = 60;
	[SerializeField] float turnSpeed = 100;

	[SerializeField] float maxDist = 300f;
	[SerializeField] float aimStartWidth = 1.0f;
	[SerializeField] float aimEndWidth = 2.0f;
	[SerializeField] float addedOffset = 4.0f;
	Transform myT;


	void Start () 
	{
	}


	void Awake() 
	{
		myT = transform;
	}


	void Update() {
		Thrust();
		Turn();
	}


	void Thrust() 
	{
		myT.position += myT.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
	}


	void Turn() 
	{
		float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
		float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");
		myT.Rotate(pitch, yaw, roll);
	}

}
