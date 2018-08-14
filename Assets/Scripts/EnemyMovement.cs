using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] Transform target;
	[SerializeField] float moveSpeed = 25f;
	public float minDist = 100f;

	[SerializeField] float rayCastOffset = 2.5f;
	[SerializeField] float detectionDist = 50f;
	[SerializeField] float rotationSpeed = 50f;

	public static bool isPaused = false;


	void OnEnable() 
	{
		EventManager.onPlayerDeath += FindCamera;
		EventManager.onStartGame += SelfDestruct;
		EventManager.onPauseGame += TogglePaused;
	}


	void OnDisable() 
	{
		EventManager.onPlayerDeath -= FindCamera;
		EventManager.onStartGame -= SelfDestruct;
		EventManager.onPauseGame -= TogglePaused;
	}


	void SelfDestruct()
	{
		Destroy(gameObject);
	}


	void Awake() {
		
	}


	void Update() {
		if (isPaused)
			return;

		if (target == null)
		{
			if (!FindTarget())
				FindCamera();
		}

		// PathFinding();
		Move();
		Turn();
	}


	// Can we find a target, i.e Player?
	bool FindTarget()
	{
		GameObject motherShip = GameObject.FindWithTag("MotherShip");
		GameObject playerShip = GameObject.FindWithTag("Player");

		if (motherShip != null)
		{
			target = motherShip.transform;
			return true;
		}
		else if (playerShip != null)
		{
			target = playerShip.transform;
			return true;
		}
		return false;
	}


	// Find MainCamera and set as target to follow
	void FindCamera()
	{
		GameObject cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
		if (cameraObj != null)
			target = cameraObj.transform;

	}


	void Turn()
	{
		Vector3 direction;
		direction = (target.position - transform.position).normalized;

		Quaternion lookRotation = Quaternion.LookRotation(target.position);
		float angle = Quaternion.Angle(transform.rotation, lookRotation);
		float timeToComplete = angle / rotationSpeed;
		float donePercentage = Mathf.Min(1F, Time.deltaTime / timeToComplete);
		// donePercentage = 0.5f;

		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, donePercentage);

		// GetComponent<Rigidbody>().MoveRotation(transform.rotation);
		Debug.DrawLine(transform.position, target.transform.position, Color.white);
		Debug.Log("Trying to rotate: Percentage done: " + donePercentage);

	}


	void Move()
	{
		if (target == null)
			FindTarget();

		if (InFront())
		{
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
			// GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed * Time.deltaTime, ForceMode.Impulse);
			Debug.DrawLine(transform.position, target.transform.position, Color.white);
		}
		else 
		{
			Turn();
			// Debug.DrawLine(transform.position, target.transform.position, Color.magenta);
		}


		GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed * Time.deltaTime, ForceMode.Impulse);
		// transform.position += transform.forward * moveSpeed * Time.deltaTime;

		// if (InFront() && HaveLineOfSight() && WihtinDistance())
		// {
		// 	if (Time.fixedTime < nextMoveTime)
		// 		return;
		
		// 	// Debug.Log("Cur time: " + Time.fixedTime + " Next move time: " + nextMoveTime);
		// 	if (Time.fixedTime > nextMoveTime)
		// 	{
		// 		nextMoveTime = Time.fixedTime + 10.0f;

		// 		GameObject tmpPlayerTarget = GameObject.FindGameObjectWithTag("Player");
		// 		target = tmpPlayerTarget.transform;

		// 		Debug.Log("Now hunting: " + tmpPlayerTarget.name);
		// 		transform.position += -transform.forward * (moveSpeed * 100f) * Time.deltaTime;

				
		// 		Invoke("MoveAwaySoon", 10.0f);
		// 		return;
		// 	}
		// 	return;
		// }

		

	}


	bool WihtinDistance()
	{
		float distToTarget = Vector3.Distance(transform.position, target.position);
		Vector3 leftSrcPos = transform.position - transform.right * 100f;

		if ( (distToTarget < detectionDist) && (distToTarget > minDist) )
		{
			Debug.DrawLine(leftSrcPos, target.position, Color.gray);
			return true;
		}
		Debug.DrawLine(leftSrcPos, target.position, Color.blue);
		return false;
	}

	bool InFront()
	{
		if (target == null)
			return false;

		Vector3 dirToTarget = transform.position - target.position;
		float angle = Vector3.Angle(transform.forward, dirToTarget);

		Vector3 rightSrcPos = transform.position + transform.right * 100f;

		if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
		{
			Debug.DrawLine(rightSrcPos, target.position, Color.gray);
			return true;
		}

		Debug.DrawLine(rightSrcPos, target.position, Color.yellow);
		return false;
	}

	bool HaveLineOfSight()
	{
		if (target == null)
			return false;
			
		RaycastHit hit;
		Vector3 direction = target.position - transform.position;

		if (Physics.Raycast(transform.position, direction, out hit, detectionDist))
		{
			if (hit.transform != null)
			{
				Debug.DrawLine(transform.position, target.position, Color.gray);
				return true;
			}
		}
		
		Debug.DrawLine(transform.position, target.position, Color.red);
		return false;
	}


	void PathFinding()
	{
		RaycastHit hit;
		Vector3 raycastOffset = Vector3.zero;

		Vector3 left = transform.position - transform.right * rayCastOffset;
		Vector3 right = transform.position + transform.right * rayCastOffset;
		Vector3 up = transform.position + transform.up * rayCastOffset;
		Vector3 down = transform.position - transform.up * rayCastOffset;
		Vector3 sideRight = transform.position + transform.right * rayCastOffset;
		Vector3 sideLeft = transform.position - transform.right * rayCastOffset;

		Vector3 noAngle = transform.right;
		Quaternion spreadAngle = Quaternion.AngleAxis(-45, transform.up);
		Vector3 sideRightDst = (spreadAngle * noAngle);
		Vector3 sideLeftDst = Quaternion.AngleAxis(45, transform.up) * -transform.right;

		Debug.DrawRay(left, transform.forward * detectionDist, Color.cyan);
		Debug.DrawRay(right, transform.forward * detectionDist, Color.cyan);
		Debug.DrawRay(up, transform.forward * detectionDist, Color.cyan);
		Debug.DrawRay(down, transform.forward * detectionDist, Color.cyan);
		Debug.DrawRay(sideRight, sideRightDst * (detectionDist * 1.25f), Color.cyan);
		Debug.DrawRay(sideLeft, sideLeftDst * (detectionDist * 1.25f), Color.cyan);

		if (Physics.Raycast(left, transform.forward , out hit, detectionDist))
		{
			raycastOffset += Vector3.right;
		}
		else if (Physics.Raycast(left, transform.forward, out hit, detectionDist))
		{
			raycastOffset -= Vector3.right;
		}

		if (Physics.Raycast(up, transform.forward, out hit, detectionDist))
		{
			raycastOffset -= Vector3.up;
		}
		else if (Physics.Raycast(down, transform.forward, out hit, detectionDist))
		{
			raycastOffset += Vector3.up;
		}

		if (Physics.Raycast(sideRight, sideRightDst, out hit, detectionDist * 1.25f))
		{
			// Debug.Log("Side right obj detected! Rotate a bit!");
			raycastOffset += Vector3.right;
		}

		if (Physics.Raycast(sideLeft, sideLeftDst, out hit, detectionDist * 1.25f))
		{
			// Debug.Log("Side left obj detected! Rotate a bit!");
			raycastOffset -= Vector3.right;
		}

		if (raycastOffset != Vector3.zero)
		{
			transform.Rotate(raycastOffset * rotationSpeed * Time.deltaTime);
		}
		else 
		{
			Debug.DrawLine(transform.position, target.transform.position, Color.black);
			Turn();
		}
	}


	void TogglePaused()
	{
		isPaused = !isPaused;
	}

}
