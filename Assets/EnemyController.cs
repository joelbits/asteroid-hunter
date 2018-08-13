using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour {

	public GameObject target;
	public Rigidbody rb;

	private enum MoveState { FREE, CHASING, ATTACKING, RETREAT, PAUSED };
	private MoveState moveState;


	void Awake() 
	{
		moveState = MoveState.FREE;
		rb = GetComponent<Rigidbody>();
	}
	

	void Update() 
	{
		// string stateValue = moveState.ToString();
		// if (stateValue != null)
		// 	Debug.Log("stateValue: " + stateValue);
		
		// if (stateValue)
		// 	return;

		Investigate();
	}


	void Investigate()
	{
		CheckForTarget();
		LookAtTarget();
		
	}


	void CheckForTarget()
	{
		if (target == null)
		{
			string tag = "MotherShip";
			
			if (tag != null)
			{
				GameObject foundTagObj = GameObject.FindGameObjectWithTag(tag);
				if (foundTagObj != null)
				{
					target = foundTagObj;
					moveState = MoveState.CHASING;
					Debug.Log("Changed moveState => " + moveState + " target: " + target);
				}
			}
		}

		if (target == null)
		{
			moveState = MoveState.FREE;
			Debug.Log("Changed moveState => " + moveState);
			return;
		}
	}


	void LookAtTarget()
	{
		Debug.DrawLine(transform.position, target.transform.position, Color.red);
		
	}

}
