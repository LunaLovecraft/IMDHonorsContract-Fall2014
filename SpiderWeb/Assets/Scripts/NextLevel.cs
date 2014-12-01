using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public Transform playerSpot;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs (this.transform.position.x - playerSpot.transform.position.x) < 2)
			Application.LoadLevel (Application.loadedLevel + 1);
	}
}
