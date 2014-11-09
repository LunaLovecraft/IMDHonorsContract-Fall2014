using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform toFollow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( toFollow.position.x, toFollow.position.y, transform.position.z);
	}
}
