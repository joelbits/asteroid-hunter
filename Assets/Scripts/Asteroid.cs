using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Explosion))]
public class Asteroid : MonoBehaviour {

	[SerializeField] float minScale = 5f;
	[SerializeField] float maxScale = 15f;
	[SerializeField] float rotationOffset = 50f;
	
	public static float destructionDelay = 3.0f;

	Transform myT;
	Vector3 randRotation;

	void Awake() {
		myT = transform;
	}

	void Start() {
		// Random size
		Vector3 scale = Vector3.one;
		scale.x = Random.Range(minScale, maxScale);
		scale.y = Random.Range(minScale, maxScale);
		scale.z = Random.Range(minScale, maxScale);

		myT.localScale = scale;

		// Random rotation
		randRotation.x = Random.Range(-rotationOffset, rotationOffset);
		randRotation.y = Random.Range(-rotationOffset, rotationOffset);
		randRotation.z = Random.Range(-rotationOffset, rotationOffset);
	}

	void Update() {
		myT.Rotate(randRotation * Time.deltaTime);
	}


	public void SelfDestruct()
	{
		Debug.Log("SelfDestruct in " + destructionDelay);

		float timer = Random.Range(0, destructionDelay);

		if (gameObject != null)
			// Invoke("DestroyAsteroid", timer);
			DestroyAsteroid();
	}


	public void DestroyAsteroid()
	{
		if (gameObject == null)
			return;

		// GetComponent<Explosion>().BlowUp();
		
		if (gameObject != null)
			Destroy(gameObject);
	}
}
