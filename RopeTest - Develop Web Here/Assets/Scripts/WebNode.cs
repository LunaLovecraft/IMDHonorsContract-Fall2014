using UnityEngine;
using System.Collections;

public class WebNode : MonoBehaviour {

    /// <summary>
    /// The next node in the linked list
    /// </summary>
    public WebNode nextNode;

    /// <summary>
    /// The previous node in the linked list
    /// </summary>
    public WebNode prevNode;
    
    /// <summary>
    /// The position of this point.
    /// </summary>
    public Transform point;

	// Use this for initialization
	void Start () {
        // Set to kinematic so we don't have it move while we're holding onto it.
        point.rigidbody2D.isKinematic = true;
        // Start with the spring off, since it won't start with a connected part.
        this.GetComponent<SpringJoint2D>().enabled = false;
	}
	
    /// <summary>
    /// Activates all necessary parts of the webbing
    /// </summary>
    public void Activate()
    {
        point.rigidbody2D.isKinematic = false;
    }

    // Using FixedUpdate to ensure physics things are accurate
	void FixedUpdate ()
    {
        // Check if the node and the one it's connected to are within the distance of the spring.  If so we don't need it: things can get closer just not further
        if (prevNode != null && this.GetComponent<SpringJoint2D>().distance < (prevNode.rigidbody2D.position - this.rigidbody2D.position).magnitude)
        {
            this.GetComponent<SpringJoint2D>().enabled = true;
        }
        else if (prevNode != null && this.GetComponent<SpringJoint2D>().distance >= (prevNode.rigidbody2D.position - this.rigidbody2D.position).magnitude)
        {
            this.GetComponent<SpringJoint2D>().enabled = false;
        }
	}
}
