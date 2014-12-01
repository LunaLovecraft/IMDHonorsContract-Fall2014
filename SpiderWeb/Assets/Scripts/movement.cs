using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    //Variables
    Vector2 maxSpeed;
    float maxStrength;
    float maxJumpHeight;

	private Animator animator;

	// Use this for initialization
	void Start () {
        maxSpeed = new Vector2(0.1f,0);
        this.GetComponent<SpringJoint2D>().enabled = false;
		animator=this.GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
		if (Input.GetKey(KeyCode.A))
		{
			//transform.position = (rigidbody2D.position - maxSpeed);
			transform.eulerAngles = new Vector2(0, 180);
            var t = transform.localScale;
            //transform.localScale = new Vector3(-Mathf.Abs(t.x), t.y, t.z);
			transform.Translate(Vector2.right * maxSpeed.x);

            foreach (var v in gameObject.GetComponentsInChildren<SimpleIK>())
            {
                v.SetFacingRight(false);
            }
            //gameObject.GetComponent<SimpleIK>().SetFacingRight(false);
		}
		if (Input.GetKey(KeyCode.D))
		{
			//transform.position = (rigidbody2D.position + maxSpeed);
            var t = transform.localScale;
            //transform.localScale = new Vector3(Mathf.Abs(t.x), t.y, t.z);
			transform.eulerAngles = new Vector2(0, 0);
			transform.Translate(Vector2.right * maxSpeed.x);
            foreach (var v in gameObject.GetComponentsInChildren<SimpleIK>())
            {
                v.SetFacingRight(true);
            }
            //gameObject.GetComponent<SimpleIK>().SetFacingRight(true);
		}
		//if (Input.GetKey(KeyCode.UpArrow))
		//{
		//    transform.position += Vector3.up * Time.deltaTime;
		//}
		//if (Input.GetKey(KeyCode.DownArrow))
		//{
		//    transform.position += Vector3.down * Time.deltaTime;
		//}
		animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
		
		if (this.GetComponent<SpringJoint2D>().connectedBody != null && this.GetComponent<SpringJoint2D>().distance * 1 >= (this.rigidbody2D.position - this.GetComponent<SpringJoint2D>().connectedBody.position).magnitude)
		{
			this.GetComponent<SpringJoint2D>().enabled = false;
		}

	}
}
