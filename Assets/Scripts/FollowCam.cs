using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	Transform myT;
	[SerializeField] Transform target;
	[SerializeField] Vector3 defaultDist = new Vector3(0f, 0f, -10f);

	[SerializeField] Vector3 velocity = Vector3.one; 
	public float distanceDamp = 10f;
	public float rotationDamp = 10f;


	void Awake() {
		myT = transform;
	}


	void Start() {
		
	}
	

	void Update() {
		
	}

	void LateUpdate() {
		SmoothFollow();
	}

	bool HasCamTarget()
	{
		if (target != null)
			return true;

		GameObject findTarget = GameObject.FindWithTag("Player");
		if (findTarget == null)
			return false;

		target = findTarget.transform;
		return true;
	}


	void SmoothFollow() {
		if (!HasCamTarget())
			return;

		Vector3 toPos = target.position + (target.rotation * defaultDist);
		Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp * Time.deltaTime);
		myT.position = curPos;

		myT.LookAt(target, target.up);
	}
}
