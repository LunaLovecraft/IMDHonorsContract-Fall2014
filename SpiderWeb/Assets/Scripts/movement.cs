using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    //Variables
    Vector2 maxSpeed;
    float maxStrength;
    float maxJumpHeight;

	// Use this for initialization
	void Start () {
        maxSpeed = new Vector2(0.1f,0);
        this.GetComponent<SpringJoint2D>().enabled = false;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = (rigidbody2D.position - maxSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = (rigidbody2D.position + maxSpeed);
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position += Vector3.up * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position += Vector3.down * Time.deltaTime;
        //}

        if (this.GetComponent<SpringJoint2D>().connectedBody != null && this.GetComponent<SpringJoint2D>().distance * 1 >= (this.rigidbody2D.position - this.GetComponent<SpringJoint2D>().connectedBody.position).magnitude)
        {
            this.GetComponent<SpringJoint2D>().enabled = false;
        }

	}
}
