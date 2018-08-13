using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Explosion : MonoBehaviour {
	
<<<<<<< HEAD
	[SerializeField] Rigidbody rigidBody;
	[SerializeField] float laserHitModifier = 300f;

	[SerializeField] Shield shield;

	[SerializeField] GameObject takeDamagePrefab;
	[SerializeField] GameObject explosion;
	[SerializeField] GameObject explosionSound;
	[SerializeField] float explosionScale = 1f;

	[SerializeField] GameObject blowUp;
=======
	[SerializeField] Shield shield;
	[SerializeField] Rigidbody rigidBody;

	[SerializeField] GameObject blowUpPrefab;
	[SerializeField] GameObject takeDamagePrefab;

	[SerializeField] GameObject explosionPrefab;
	[SerializeField] GameObject explosionSoundPrefab;
<<<<<<< HEAD
>>>>>>> Initial commit
=======
	private Asteroid targetToDestroy;
>>>>>>> Initial commit - version 2

	// TODO: Tweaking necessary for better shooting/explosion experience
	[SerializeField] float soundTimeOut = 1.05f;
	[SerializeField] float animationTimeOut = 1.05f;
	GameObject animInstance;
	GameObject soundInstance;

<<<<<<< HEAD
	public void HitTaken(Vector3 hitPoint)
	{
		// if (animInstance == null)
		// {
		// 	GameObject expBigger = explosion;
		// 	Vector3 targetScale = new Vector3(explosionScale, explosionScale, explosionScale);
		// 	expBigger.transform.localScale = targetScale;
		// 	animInstance = Instantiate(expBigger, hitPoint, Quaternion.identity, transform);
		// 	soundInstance = Instantiate(explosionSound, hitPoint, Quaternion.identity, transform) as GameObject;
		// 	Destroy(soundInstance, soundTimeOut);
		// 	Destroy(animInstance, animationTimeOut);
		// }

=======
	[SerializeField] float laserHitModifier = 300f;
	[SerializeField] float explosionScale = 15.0f;


	public void HitTaken(Vector3 hitPoint)
	{
>>>>>>> Initial commit
		if (shield != null)
		{
			// shield.GetComponent<Shield>().TakeDamage();
			shield.TakeDamage();
			// Debug.Log("Hit Taken to: " + gameObject.name);
		}
		else
		{
			// Debug.Log("No shield (Could be enemy or other explosion)");
		}
	}


	public void ShowTakeDamage()
	{
<<<<<<< HEAD
		// Debug.Log("ShowTakeDamage()...");

<<<<<<< HEAD
		GameObject tmpPrefab = Instantiate(takeDamagePrefab, transform.position, Quaternion.identity);
		tmpPrefab.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
		Destroy(tmpPrefab, animationTimeOut);
=======
=======
>>>>>>> Initial commit - version 2
		if (takeDamagePrefab == null)
		{
			Debug.Log("TakeDamagePrefeb is not set in Explosion!");
			return;
		}
		
		if (takeDamagePrefab != null)
		{
			// Debug.Log("TakeDamagePrefeb is to explode. takeDamagePrefab: " + takeDamagePrefab.name + " trPos: " + transform.position + " trRot: " + transform.rotation);

			GameObject takeDamageExplosion = Instantiate(takeDamagePrefab, transform.position, transform.rotation) as GameObject;
			takeDamageExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

			if (takeDamageExplosion != null)
				Destroy(takeDamageExplosion, animationTimeOut);
		}
>>>>>>> Initial commit
	}


	public void HitTakenFromPlayer(Vector3 hitPoint)
	{
			Debug.Log("Hit Taken from Player to: " + gameObject.transform.parent.gameObject.name);
			shield.TakeDamageFromPlayer();
	}


	void OnCollisionEnter(Collision collision) 
	{
		Debug.Log("Contact: between " + gameObject.name + " and " + collision.transform.name)
		;
		foreach(ContactPoint contact in collision.contacts)
			HitTaken(contact.point);

		if (collision.transform.CompareTag("LaserShot"))
		{
			Destroy(collision.gameObject);
		}
	}


	public void AddForce(Vector3 hitPosition, Transform hitSource)
	{
		if (rigidBody == null)
			return;

		Vector3 forceVector = (hitSource.position - hitPosition).normalized;

		// Debug.LogWarning("AddForce: " + gameObject.name + " hit by: " + hitSource.name 
		// 	+ " with force: " + forceVector * laserHitModifier);

		rigidBody.AddForceAtPosition(forceVector.normalized * laserHitModifier, hitPosition, ForceMode.Impulse);
	}


	public void DestroyAsteroid(Asteroid targetAstroid)
	{
		targetAstroid.SelfDestruct();
	}


	public void BlowUp()
	{	
<<<<<<< HEAD
<<<<<<< HEAD
		Instantiate(blowUp, transform.position, Quaternion.identity);

		// Debug.Log("BlowUp: " + gameObject.name);
=======
		// Instantiate(blowUp, transform.position, Quaternion.identity);
		if (blowUpPrefab == null)
			return;

		// GameObject tmpPrefab = Instantiate(blowUpPrefab, transform.position, transform.rotation) as GameObject;

		// if (tmpPrefab != null)
		// {
		// 	Destroy(tmpPrefab, animationTimeOut);
		// 	// tmpPrefab.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
		// }

		
		Debug.Log("Blowup Prefab should explode for: " + gameObject.name + " with tag: " + gameObject.tag);

>>>>>>> Initial commit
=======
		if (blowUpPrefab == null)
			return;

		// Debug.Log("Blowup Prefab should explode for: " + gameObject.name + " with tag: " + gameObject.tag);
>>>>>>> Initial commit - version 2

		// If attached to Asteroid
		Asteroid tmpAst = GetComponent<Asteroid>();
		if (tmpAst != null)
		{
<<<<<<< HEAD
			// EventManager.ScorePoints(5000);
			if (gameObject != null)
<<<<<<< HEAD
				Destroy(gameObject);
			return;
		}

		// If attached to Player 
		if (gameObject != null && gameObject.transform != null && gameObject.transform.parent != null)
		{
			GameObject parent = gameObject.transform.parent.gameObject;

			if (parent != null && parent.gameObject.transform.parent != null)
			{
				GameObject myShip = parent.transform.parent.gameObject;
				if (myShip != null)
				{
					Destroy(myShip);
					return;
				}
			}
=======
			{
				Debug.Log("Asteroid goes BOOM!");
				gameObject.GetComponent<Asteroid>().SelfDestruct();
				return;
			}
=======
			GameObject blowupExplosion = Instantiate(blowUpPrefab, transform.position, transform.rotation) as GameObject;
			blowupExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
			if (blowupExplosion != null)
				Destroy(blowupExplosion, 3.5f);

			Debug.Log("Explosion: Asteroid selfdestructing next!");
			targetToDestroy = tmpAst;
			// Invoke("DestroyAsteroid", 1.5f);
			targetToDestroy.SelfDestruct();
			
			return;
>>>>>>> Initial commit - version 2
		}

		// If attached to Player 
		if (gameObject.CompareTag("Player"))
		{
			// DESTROY PLAYER SHIP
			Destroy(gameObject);
>>>>>>> Initial commit
		}
		
		// If attached to MotherShip
		if (gameObject.tag.Equals("MotherShip"))
		{
			GameObject mShip = gameObject;
			GameObject parent = gameObject.transform.parent.gameObject;
<<<<<<< HEAD
			Destroy(parent);
=======
			if (parent != null)
				Destroy(parent);
>>>>>>> Initial commit
		}

		// If attached to Enemy, blow him up
		if (gameObject.tag.Equals("Enemy"))
		{
			GameObject enemy = gameObject;
			if (enemy != null) 
			{
				EventManager.ScorePoints(100);

				GameObject enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
				enemySpawner.GetComponent<EnemySpawner>().RemoveEnemy(enemy);
<<<<<<< HEAD
				// Debug.Log("Removed Enemy Entry: " + gameObject.name);
=======
>>>>>>> Initial commit

				// Destroy GameObject
				Destroy(enemy);
			}
		}

	}
	

}
