using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour {
	void Start () {
		transform.position = transform.GetComponent<FixedJoint2D> ().connectedBody.transform.position;
	}
}