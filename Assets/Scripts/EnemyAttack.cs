using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	// [SerializeField] float fireRate = .75f;
	[SerializeField] Transform target;
	[SerializeField] Laser laser;
	Vector3 hitPos;


	bool FindTarget()
	{	
		if (target != null)
		{
			GameObject targetObj = GameObject.FindWithTag("MotherShip");
			GameObject playerObj = GameObject.FindWithTag("Player");
			float targetDist = Vector3.Distance(targetObj.transform.position, transform.position);
			float playerDist = Vector3.Distance(playerObj.transform.position, transform.position);
			bool playerClosest = false;

			
			playerClosest = playerDist < targetDist;

			if (playerClosest) 
			{
				target = playerObj.transform;
				return true;
			}

			target = targetObj.transform;
			return true;
		}
		

		return true;
	}


	bool InFront()
	{
		if (target == null)
			return true;

		Vector3 dirToTarget = transform.position - target.position;
		float angle = Vector3.Angle(transform.forward, dirToTarget);

		if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
		{
			Debug.DrawLine(transform.position, target.position, Color.green);
			return true;
		}

		Debug.DrawLine(transform.position, target.position, Color.yellow);
		return false;
	}

	bool HaveLineOfSight()
	{
		if (target == null)
			return false;

		RaycastHit hit;
		Vector3 direction = target.position - transform.position;

		if (Physics.Raycast(laser.transform.position, direction, out hit, laser.Distance))
		{
			if (hit.transform.CompareTag("Player"))
			{
				Debug.DrawRay(laser.transform.position, direction, Color.green);
				hitPos = hit.transform.position;
				FireLaser();
				return true;
			}
			if (hit.transform != null)
			{
				// Debug.Log("LOS to: " + hit.transform.name);
				Debug.DrawRay(laser.transform.position, direction, Color.green);
				hitPos = hit.transform.position;
				FireLaser();
				return false;
			}
		}
		return false;
	}

	void Update() {
		if (!FindTarget())
			return;

		InFront();
		HaveLineOfSight();

		if (InFront() && HaveLineOfSight())
		{
			FireLaser();
		}
	}


	void FireLaser() 
	{
		Debug.Log("Fire lasers at: " + hitPos + " target: " + target);
		laser.FireLaser(hitPos, target);
	}
}
