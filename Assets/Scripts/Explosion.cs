﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Explosion : MonoBehaviour {

	[SerializeField] float explosionScale = 15.0f;
	[SerializeField] float laserHitModifier = 300f;

	[SerializeField] GameObject blowUp;
	[SerializeField] Shield shield;
	[SerializeField] Rigidbody rigidBody;

	[SerializeField] GameObject blowUpPrefab;
	[SerializeField] GameObject blowUpSound;
	[SerializeField] GameObject explosion;
	[SerializeField] GameObject explosionSound;
	[SerializeField] GameObject explosionPrefab;
	[SerializeField] GameObject explosionSoundPrefab;
	[SerializeField] GameObject takeDamagePrefab;

	private Asteroid targetToDestroy;

	// TODO: Tweaking necessary for better shooting/explosion experience
	[SerializeField] float soundTimeOut = 1.05f;
	[SerializeField] float animationTimeOut = 1.05f;
	GameObject animInstance;
	GameObject soundInstance;


	public void HitTaken(Vector3 hitPoint)
	{
		if (shield != null)
		{
			// shield.GetComponent<Shield>().TakeDamage();
			shield.TakeDamage(hitPoint);
			// Debug.Log("Hit Taken to: " + gameObject.name);
		}
		else
		{
			Debug.Log("No shield to apply HitTaken to.");
		}
	}


	public void ShowTakeDamage(Vector3 hitPoint)
	{	
		// Debug.Log("TakeDamagePrefeb is to explode. takeDamagePrefab: " + takeDamagePrefab.name + " trPos: " + transform.position + " trRot: " + transform.rotation);
		if (takeDamagePrefab != null && transform != null)
		{
			GameObject takeDamageExplosion = Instantiate(takeDamagePrefab, hitPoint, transform.rotation, transform) as GameObject;
			takeDamageExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

			if (takeDamageExplosion != null)
				Destroy(takeDamageExplosion, animationTimeOut);
		}
	}


	public void HitTakenFromPlayer(Vector3 hitPoint)
	{
			// Debug.Log("Hit Taken from Player to: " + gameObject.transform.parent.gameObject.name);
			shield.TakeDamageFromPlayer(hitPoint);
	}


	void OnCollisionEnter(Collision collision) 
	{
		// Debug.Log("Contact: between " + gameObject.name + " and " + collision.transform.name);

		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.gameObject.GetComponentInChildren<Shield>().TakeDamage(collision.contacts[0].point, 1);
			HitTaken(collision.contacts[0].point);
			// collision.transform.gameObject.GetComponentInChildren<Explosion>().ShowTakeDamage(collision.contacts[0].point);
			return;
		}

		// foreach(ContactPoint contact in collision.contacts)
		HitTaken(collision.contacts[0].point);

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
		if (blowUpPrefab == null)
			return;

		// Debug.Log("Blowup Prefab should explode for: " + gameObject.name + " with tag: " + gameObject.tag);

		// If attached to Asteroid
		Asteroid tmpAst = GetComponent<Asteroid>();
		if (tmpAst != null)
		{
			// EventManager.ScorePoints(5000);
			if (gameObject != null)
			{
				GameObject blowupExplosion = Instantiate(blowUpPrefab, transform.position, transform.rotation, transform) as GameObject;
				blowupExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

				if (blowupExplosion != null)
					Destroy(blowupExplosion, 3.5f);
				
				GameObject tmpSound = Instantiate(blowUpSound, transform.position, transform.rotation) as GameObject;
				if (tmpSound != null)
					Destroy(tmpSound, soundTimeOut);

				gameObject.GetComponent<Asteroid>().SelfDestruct();
				// Destroy(gameObject);
				return;
			}
			return;
		}


		// If attached to Player 
		if (gameObject.CompareTag("Enemy"))
		{
			GameObject blowupExplosion = Instantiate(blowUpPrefab, transform.position, transform.rotation, transform) as GameObject;
			blowupExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

			if (blowupExplosion != null)
				Destroy(blowupExplosion, 3.5f);

			Destroy(gameObject);
			return;
		}


		// If attached to Player 
		if (gameObject.CompareTag("Player"))
		{
			GameObject blowupExplosion = Instantiate(blowUpPrefab, transform.position, transform.rotation, transform) as GameObject;
			blowupExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

			if (blowupExplosion != null)
				Destroy(blowupExplosion, 3.5f);

			Debug.Log("Blowup Player!");
			Destroy(gameObject);
			return;
		}

		// If attached to MotherShip
		if (gameObject.CompareTag("MotherShip"))
		{
			GameObject blowupExplosion = Instantiate(blowUpPrefab, transform.position, transform.rotation) as GameObject;
			blowupExplosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

			if (blowupExplosion != null)
				Destroy(blowupExplosion, 3.5f);

			Destroy(gameObject);
		}

		// If attached to Enemy, blow him up
		if (gameObject.CompareTag("Enemy"))
		{
			GameObject enemy = gameObject;
			if (enemy != null) 
			{
				EventManager.ScorePoints(100);

				GameObject enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
				enemySpawner.GetComponent<EnemySpawner>().RemoveEnemy(enemy);

				// Destroy Enemy GameObject
				Destroy(enemy);
			}
		}


	} // End BlowUp()
	

}
