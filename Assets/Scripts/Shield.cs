using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Explosion))]
public class Shield : MonoBehaviour {

	[SerializeField] int maxHealth = 10;
	[SerializeField] int curHealth;
	[SerializeField] float regenRate = 5f;
	[SerializeField] int regenAmount = 1;

 
	void Start() 
	{
		curHealth = maxHealth;

		InvokeRepeating("Regenerate", regenRate, regenRate);
	}


	void Regenerate()
	{
		// curHealth += regenAmount;

		if (curHealth < maxHealth)
			curHealth += regenAmount;

		if (curHealth > maxHealth)
		{
			curHealth = maxHealth;
			// CancelInvoke(); 
		}

		// EventManager.TakeDamage(curHealth/(float)maxHealth); //FOR DEBUG
	}


	public void TakeDamage(int dmg = 1)
	{
		curHealth -= dmg;

		float healthPercent = (float) curHealth / (float) maxHealth;

		if (gameObject.CompareTag("MotherShip"))
		{
			EventManager.MotherTakeDamage(healthPercent);
			if (curHealth < 1)
			{
				EventManager.PlayerDeath();

				// GetComponentInChildren<Explosion>().BlowUp();
				// explosion.BlowUp();
				GetComponent<Explosion>().BlowUp();
				Debug.Log("GAME OVER; MotherShip died === Player died == Blowup!");
			}
		} 
		else if (gameObject.CompareTag("Player"))
		{
			// Debug.Log("curH: " + curHealth + " max: " + maxHealth + " percent: " + healthPercent);
			EventManager.TakeDamage(healthPercent);

			if (curHealth < 1)
			{	
				EventManager.PlayerDeath();
				GetComponent<Explosion>().BlowUp();
				// GetComponentInChildren<Explosion>().BlowUp();
				Debug.Log("Player died: BOOM!");
			}
		}
		else if (gameObject.CompareTag("Asteroid"))
		{
			// Debug.Log("Asteroid taking damage, current health: " + curHealth);
			GetComponent<Explosion>().ShowTakeDamage();

			if (curHealth < 1)
			{
				GetComponent<Explosion>().BlowUp();
				Debug.Log("Asteroid died: BOOM!");
				return;
			}
		}

		if (curHealth < 0)
			curHealth = 0;

	}


	public void TakeDamageFromPlayer(int dmg = 1)
	{
		curHealth -= dmg;
		// Debug.Log(gameObject.name + " Taking Dmg From Player: " + dmg + " cur HP: " + curHealth);

		if (curHealth < 1) 
		{
			GetComponent<Explosion>().ShowTakeDamage();
			// EventManager.ScorePoints(100);
		}
	}

	public float GetCurHealth()
	{
		return curHealth;
	}


	public float GetMaxHealth()
	{
		return maxHealth;
	}

}
