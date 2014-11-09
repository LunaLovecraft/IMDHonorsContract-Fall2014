using UnityEngine;
using System.Collections;

public class FluffScript : MonoBehaviour {

    public float desiredY;
    public float lowestY;
    public float floatForce;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (transform.position.y < lowestY)
        {
            rigidbody2D.AddForce(new Vector3(0, floatForce, 0));
            //transform.position.Set(transform.position.x, transform.position.y + floatForce * Time.deltaTime, transform.position.z);
        }
	
	}
}
