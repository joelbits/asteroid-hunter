using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LaserBolt : MonoBehaviour {

	[SerializeField] float moveSpeed = 100.0f;
	[SerializeField] float crashForce = 100.0f;
	[SerializeField] float maxDist = 2500.0f;
	[SerializeField] float boltSpeed = 10.0f;
	private LineRenderer lr;
	private Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		if (rb != null)
			rb.velocity = transform.forward * moveSpeed;
	}


	void OnTriggerEnter(Collider other) 
	{
<<<<<<< HEAD
		Debug.Log("OnTriggerEnter: colliding with: " + other.name + " src: " + gameObject.name);
=======
		Debug.Log("LASERBOLT: OnTriggerEnter: colliding with: " + other.name + " src: " + gameObject.name);
>>>>>>> Initial commit

		Rigidbody otherRb = other.GetComponent<Rigidbody>();

		if (otherRb != null)
		{
			otherRb.AddForce(-transform.forward * crashForce, ForceMode.Impulse);
		}
	}

}
