using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserBolt))]
public class Laser : MonoBehaviour {

[SerializeField] float laserOffTime = .15f;
[SerializeField] float maxDist = 500f;
[SerializeField] float fireDelay = 0.2f;
// [SerializeField] float laserSpeed = 700.0f;
[SerializeField] GameObject laserSound;
[SerializeField] GameObject laserBoltPrefab;
[SerializeField] Transform laserBoltSpawn;
[SerializeField] float laserOffset = 10.0f;

public bool canFire;


private void Start() 
{
	canFire = true;
}


private void Awake() 
{

}

private void Update() {
	// Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDist, Color.green, 0.1f);
}


Vector3 CastRay()
{
	RaycastHit hit;
	Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDist;

	if (Physics.Raycast(transform.position, fwd, out hit))
	{
		if (hit.transform.tag.Equals("Enemy"))
		{
			GameObject enemy = hit.transform.gameObject;

			enemy.GetComponent<Explosion>().BlowUp();
			EventManager.ScorePoints(1000);
		}
		else if (hit.transform.tag.Equals("MotherShip"))
		{
			Debug.Log("DONT SHOOT MOTHERSHIP DUMMY!");

			GameObject mShip = hit.transform.gameObject;
			float motherCurHealth = mShip.GetComponent<Shield>().GetCurHealth();
			float motherMaxHealth = mShip.GetComponent<Shield>().GetMaxHealth();
			float motherCurHealthPercent = motherCurHealth / motherMaxHealth;
			EventManager.onMotherTakeDamage(motherCurHealthPercent);

			if (motherCurHealth == 0)
			{
				mShip.GetComponent<Explosion>().BlowUp();
				EventManager.PlayerDeath();
				// return Vector3.zero;
			}
		}

		// else if (hit.transform.CompareTag("Asteroid"))
		// {
		// 	EventManager.ScorePoints(100);

		// 	GameObject asteroidObj = hit.transform.gameObject;

		// 	if (asteroidObj != null)
		// 	{
		// 		Shield targetShield = hit.transform.GetComponent<Shield>();
		// 		if (targetShield != null)
		// 		{
		// 			targetShield.TakeDamageFromPlayer();
		// 			float curHealth = targetShield.GetCurHealth();
		// 		}
		// 	}
		// }

		// SpawnExplosion(hit.point, hit.transform);
		
		return hit.point;
	} // END if we hit anything

	// Debug.Log("We missed...");
	return transform.position + (transform.forward * maxDist);
}


void SpawnExplosion(Vector3 hitPos, Transform target)
{
	Debug.Log("Spawning Explosion, target: " + target.name);

	Explosion tmp = target.transform.GetComponentInChildren<Explosion>();

	if (tmp != null)
	{
		tmp.HitTaken(hitPos);
		tmp.AddForce(hitPos, target.transform);
	}
}

// When player shoots
public void FireLaser()
{
	Vector3 pos = CastRay();
	FireLaser(pos);
	// OR just FireLaser(CastRay());
}

// When enemy shoots
public void FireLaser(Vector3 targetPos, Transform target = null) 
{
	if (canFire)
	{
		Vector3 srcPos = laserBoltSpawn.position;

		// Debug.Log("Shoot Laser from:" + srcPos + " - Spawn: " + laserBoltSpawn.name);

		if (target != null)
		{
			Debug.Log("Firing at NO TARGET!");
		}

		// FireWithLR(srcPos, targetPos, srcRot);
		FireWithLaserBolt(srcPos, targetPos);

		canFire = false;
	
		// Invoke("TurnOffLaser", laserOffTime);
		Invoke("CanFire", fireDelay);
	}
}


void FireWithLaserBolt(Vector3 srcPosition, Vector3 targetPosition)
{
	Quaternion srcRot = transform.rotation;
	Vector3 srcOffset = laserBoltSpawn.position + (laserBoltSpawn.forward * laserOffset);
	Quaternion srcRotation = transform.parent.rotation;

	// GameObject laserBolt = Instantiate(laserBoltPrefab, srcPos, laserBoltSpawn.rotation, transform.parent) as GameObject;
	GameObject laserBolt = Instantiate(laserBoltPrefab, srcOffset, srcRot) as GameObject;
	
	// Play sound effect
	if (laserSound != null)
	{
		GameObject soundObj = Instantiate(laserSound) as GameObject;
		Destroy(soundObj, 1.5f);
	}

	Destroy(laserBolt, laserOffTime); // TODO: Tweak Laserbolt disapperance
}


// void FireWithLR(Vector3 srcPos, Vector3 dstPos, Quaternion srcRot)
// {
// 	lr.SetPosition(0, srcPos);
// 	lr.SetPosition(1, dstPos);
// 	lr.startWidth = 3.0f;
// 	lr.endWidth = 3.0f;
// 	lr.enabled = true;
// }

// void TurnOffLaser()
// {
// 	lr.enabled = false;
// }


void CanFire() 
{
	canFire = true;
}


public float Distance
{
	get { return maxDist; }
}

	
}
