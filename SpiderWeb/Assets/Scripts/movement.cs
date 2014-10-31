using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    //Variables
    float maxSpeed;
    float maxStrength;
    float maxJumpHeight;

	// Use this for initialization
	void Start () {
        maxSpeed = 2;
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * maxSpeed * Time.deltaTime;
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position += Vector3.up * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position += Vector3.down * Time.deltaTime;
        //}

	}
}
