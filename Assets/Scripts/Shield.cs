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


	public void TakeDamage(Vector3 hitPoint, int dmg = 1)
	{
		curHealth -= dmg;
		float curHealthPercent = (float)curHealth / (float)maxHealth;

		// Debug.Log("TakeDamage: " + gameObject.name + " tag: " + gameObject.tag + " Cur HP: " + curHealthPercent);

		if (gameObject.CompareTag("MotherShip"))
		{
			// Debug.Log("TakeDamage: " + gameObject.name + " tag: " + gameObject.tag + " " + curHealthPercent + "% HP");
			EventManager.onMotherTakeDamage(curHealthPercent);
			GetComponent<Explosion>().ShowTakeDamage(hitPoint);

			if (curHealth == 1)
			{
				EventManager.PlayerDeath();
				GetComponent<Explosion>().BlowUp();
				Debug.Log("GAME OVER; MotherShip died === Player died == Blowup!");
			}
		} 
		else if (gameObject.CompareTag("Player"))
		{
			// Debug.Log("TakeDamage: " + gameObject.name + " curH: " + curHealth + " max: " + maxHealth + " percent: " + curHealthPercent);
			EventManager.TakeDamage(curHealthPercent);

			if (curHealth < 1)
			{	
				GameObject motherShip = GameObject.FindGameObjectWithTag("MotherShip");
				if (motherShip != null)
				{
					GetComponent<Explosion>().BlowUp();
					EventManager.PlayerDeath();
					Debug.Log("Player died: BOOM!");
				}
				else
				{
					GetComponent<Explosion>().BlowUp();
				}
			}
		}
		else if (gameObject.CompareTag("Asteroid"))
		{
			// Debug.Log("Asteroid taking damage, current health: " + curHealth);
			GetComponent<Explosion>().ShowTakeDamage(hitPoint);

			if (curHealth < 1)
			{
				GetComponent<Explosion>().BlowUp();
				return;
			}
		}
		else if (gameObject.CompareTag("Enemy"))
		{
			GetComponent<Explosion>().ShowTakeDamage(hitPoint);

			if (curHealth < 1)
			{
				GetComponent<Explosion>().BlowUp();
				return;
			}
		}

		if (curHealth < 0)
			curHealth = 0;

	}


	public void TakeDamageFromPlayer(Vector3 hitPoint, int dmg = 1)
	{
		curHealth -= dmg;
		// Debug.Log(gameObject.name + " Taking Dmg From Player: " + dmg + " cur HP: " + curHealth);

		if (curHealth < 1) 
		{
			GetComponent<Explosion>().ShowTakeDamage(hitPoint);
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
