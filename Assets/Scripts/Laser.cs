using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {

[SerializeField] float laserOffTime = .15f;
[SerializeField] float maxDist = 3000f;
[SerializeField] float fireDelay = .5f;
[SerializeField] float laserSpeed = 3.0f;
[SerializeField] GameObject laserSound;
LineRenderer lr;
[SerializeField] GameObject laserBoltPrefab;
[SerializeField] Transform laserBoltSpawn;
[SerializeField] float laserOffset = 10.0f;
=======

[RequireComponent(typeof(LaserBolt))]
public class Laser : MonoBehaviour {

[SerializeField] float laserOffTime = .15f;
[SerializeField] float maxDist = 500;
[SerializeField] float fireDelay = 0.2f;
[SerializeField] float laserSpeed = 700.0f;
[SerializeField] GameObject laserSound;
[SerializeField] GameObject laserBoltPrefab;
[SerializeField] Transform laserBoltSpawn;
[SerializeField] float laserOffset = 10.0f;

>>>>>>> Initial commit
public bool canFire;


private void Start() 
{
<<<<<<< HEAD
	lr.enabled = false;
=======
>>>>>>> Initial commit
	canFire = true;
}

private void Awake() 
{
<<<<<<< HEAD
	lr = GetComponent<LineRenderer>();
=======

>>>>>>> Initial commit
}

private void Update() {
	// Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDist, Color.green, 0.1f);
<<<<<<< HEAD
	// lr.transform.position += Vector3.forward * Time.deltaTime * laserSpeed;
=======
>>>>>>> Initial commit
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
<<<<<<< HEAD
		else if (hit.transform.CompareTag("Asteroid"))
		{
			EventManager.ScorePoints(100);

			GameObject asteroidObj = hit.transform.gameObject;

			if (asteroidObj != null)
			{
				Shield targetShield = hit.transform.GetComponent<Shield>();
				targetShield.TakeDamageFromPlayer();
				float curHealth = targetShield.GetCurHealth();
			}
		}
=======
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
>>>>>>> Initial commit

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

<<<<<<< HEAD
		Debug.Log("Shoot Laser from:" + srcPos + " - Spawn: " + laserBoltSpawn.name);
=======
		// Debug.Log("Shoot Laser from:" + srcPos + " - Spawn: " + laserBoltSpawn.name);
>>>>>>> Initial commit

		if (target != null)
		{
			Debug.Log("Firing at NO TARGET!");
<<<<<<< HEAD
			// SpawnExplosion(targetPos, target);
			// Debug.Log("Laser pos: " + transform.position + " Parent: " + transform.parent.ToString());
=======
>>>>>>> Initial commit
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
<<<<<<< HEAD

	// Debug.Log("FireWithLaserBolt at: target" + targetPosition + " src: " + srcPosition + " srcRot: " + srcRot);

	// Vector3 srcPos = laserBoltSpawnPos.position + laserBoltSpawnPos.forward * laserOffset;
	// Vector3 srcPos = transform.parent.position ;
	// Vector3 srcPos = transform.position + (transform.forward * laserOffset);
	// Vector3 srcPos = laserBoltSpawnPos.position + (laserBoltSpawnPos.forward * laserOffset);
		
	// Vector3 srcOffset = transform.parent.transform.parent.position + (transform.parent.forward * laserOffset);
	Vector3 srcOffset = laserBoltSpawn.position + (laserBoltSpawn.forward * laserOffset);

	// Rigidbody rb = transform.parent.transform.parent.GetComponent<Rigidbody>();
	// Transform originT = rb.transform;
	// Vector3 srcPos = rb.position + (rb.transform.forward * laserOffset);
	
	// Vector3 srcPos = transform.parent.transform.parent.GetComponent<Rigidbody>().position;
	// Vector3 srcPos = laserBoltSpawnPos.position + (laserBoltSpawnPos.forward * laserOffset) ;
	// Vector3 srcPos = transform.parent.position + transform.parent.forward * laserOffset;

	// Quaternion srcRotation = transform.parent.transform.parent.GetComponent<Rigidbody>().rotation;
	Quaternion srcRotation = transform.parent.transform.parent.rotation;

	// Debug.Log("Shoot Laser from:" + srcPosition + " - Spawn: " + laserBoltSpawn.name);
	// Debug.Log("=== TO: " + dstPos + " - Target: " + target);
	

	// GameObject laserBolt = Instantiate(laserBoltPrefab, srcPos, laserBoltSpawn.rotation, transform.parent) as GameObject;
	GameObject laserBolt = Instantiate(laserBoltPrefab, laserBoltSpawn.position, srcRot) as GameObject;
=======
	Vector3 srcOffset = laserBoltSpawn.position + (laserBoltSpawn.forward * laserOffset);
	Quaternion srcRotation = transform.parent.rotation;

	// GameObject laserBolt = Instantiate(laserBoltPrefab, srcPos, laserBoltSpawn.rotation, transform.parent) as GameObject;
	GameObject laserBolt = Instantiate(laserBoltPrefab, srcOffset, srcRot) as GameObject;
>>>>>>> Initial commit
	
	// Play sound effect
	if (laserSound != null)
	{
		GameObject soundObj = Instantiate(laserSound) as GameObject;
		Destroy(soundObj, 1.5f);
	}

<<<<<<< HEAD
	Destroy(laserBolt, 2.0f); // TODO: Tweak Laserbolt disapperance
=======
	Destroy(laserBolt, laserOffTime); // TODO: Tweak Laserbolt disapperance
>>>>>>> Initial commit
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
