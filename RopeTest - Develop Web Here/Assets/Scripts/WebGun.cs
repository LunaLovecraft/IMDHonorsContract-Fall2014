using UnityEngine;
using System.Collections;

public class WebGun : MonoBehaviour
{
    // Node Handlers
    /// <summary>
    /// Previous node used by the web slinger.
    /// </summary>
    public Rigidbody2D prevNode;

    /// <summary>
    /// Current Node being held by the web slinger during firing
    /// </summary>
    public Rigidbody2D currentNode;

    // State handler and timer
    /// <summary>
    /// Which of the three states the web slinger is currently in.
    /// 0 - Inactive
    /// 1 - Firing Web
    /// 2 - Holding Web
    /// 3 - Reset
    /// </summary>
    private byte fireState;

    /// <summary>
    /// Counts the number of frames to wait between firing a new node.
    /// </summary>
    private byte pointCounter;

    // Reference
    /// <summary>
    /// The webpoint object being fired.
    /// </summary>
    public Rigidbody2D webPoint;

    // Use this for initialization
    void Start()
    {
        // Start in a default state of 0
        fireState = 0;
    }

    // Update is used to ensure accurate keypresses.
    void Update()
    {
        // Make sure that any web point we're holding onto is following us.
        if(prevNode != null)
        {
            prevNode.rigidbody2D.position = this.transform.position;
        }

        // Check to see which state we're in for input reasons.
        #region StateHandler
        switch (fireState)
        {
            // Inactive case
            case 0:
                // On left click begin firing
                if (Input.GetMouseButtonDown(0))
                {
                    fireState = 1;
                    // Create a point since there isn't one yet.  This will be the first one fired.
                    prevNode = Instantiate(webPoint, this.transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
                    goto case 1;
                }
                break;
            
            // Currently firing web
            case 1:
                // On releasing left click stop firing
                if (Input.GetMouseButtonUp(0))
                {
                    fireState = 2;
                    goto case 2;
                }
                // On right click reset
                if (Input.GetMouseButtonUp(1))
                {
                    // This is dropping the web
                    // Activate the previousnode
                    prevNode.GetComponent<WebNode>().Activate();
                    // Disable the spring permanently: it'll always be null so it will always try to head towards a static position.  Can't have that.
                    prevNode.GetComponent<SpringJoint2D>().enabled = false;
                    fireState = 3;
                }
                break;

            case 2:
                // Same as case one right click
                if (Input.GetMouseButtonUp(1))
                {
                    prevNode.GetComponent<WebNode>().Activate();
                    prevNode.GetComponent<SpringJoint2D>().enabled = false;
                    fireState = 3;
                }

                // On space pull in the web
                if (prevNode.GetComponent<WebNode>().nextNode != null && Input.GetKey(KeyCode.Space))
                {
                    // Get the direction towards the webgun from the next point.
                    Vector2 tensionForceDirection = (prevNode.rigidbody2D.position - prevNode.GetComponent<WebNode>().nextNode.rigidbody2D.position).normalized;

                    // Scale the force arbitrarily and add it.
                    tensionForceDirection.Scale(new Vector2(50, 50));
                    prevNode.GetComponent<WebNode>().nextNode.rigidbody2D.AddForce(tensionForceDirection);
                }
                break;
        }
        #endregion
    }

    // FixedUpdate is used for physics related tasks to maintain accuracy.
    void FixedUpdate()
    {

        // Debug movement controls.
        #region DebugMovement
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0, 0.1f,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += new Vector3(0, -0.1f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-0.1f, 0, 0);
        }
        #endregion

        // Code handling the firing of web.
        #region FiringWeb
        switch (fireState)
        {
            // Firing Case
            case 1:
                // Once every 20 frames...
                if (pointCounter == 0)
                {


                    // Create a new node to interact with
                    currentNode = Instantiate(webPoint, this.transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;


                    // Tell it the next node in the chain.
                    currentNode.GetComponent<WebNode>().nextNode = prevNode.GetComponent<WebNode>();

                    // If there is a next node in the chain, inform that next node about the new one.
                    if (prevNode != null)
                    {
                        prevNode.GetComponent<WebNode>().prevNode = currentNode.GetComponent<WebNode>();
                    }

                    // Tell the next node in the chain to have a spring between it and the new node.
                    prevNode.GetComponent<SpringJoint2D>().connectedBody = currentNode.rigidbody2D;

                    // Fire the previous node
                    prevNode.GetComponent<WebNode>().Activate();

                    // Get the worldspace position of the mouse
                    Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 fireDirection = new Vector2(mousePoint.x - this.transform.position.x, mousePoint.y - this.transform.position.y);

                    // Fire it with arbitrary force.
                    fireDirection.Normalize();
                    fireDirection.Scale(new Vector2(700,700));
                    prevNode.rigidbody2D.AddForce(fireDirection);

                    // Move the nodes along the chain
                    prevNode = currentNode;
                    currentNode = null;
                }
                // Increase the timer until the next fire.
                pointCounter++;

                // Arbitrary reset point
                if (pointCounter >= 10)
                {
                    pointCounter = 0;
                }
                break;
            // Holding Case
            case 2:
                // If the next node is too close to the previous node while being held, then suck it in.
                if (prevNode.GetComponent<WebNode>().nextNode != null && (prevNode.GetComponent<WebNode>().nextNode.rigidbody2D.position - prevNode.position).magnitude < .4f)
                {
                    // Create a temporary node to allow access to the next rigidbody.
                    Rigidbody2D temp = prevNode.GetComponent<WebNode>().nextNode.rigidbody2D;
                    // Remove references to the prevNode
                    temp.GetComponent<SpringJoint2D>().connectedBody = null;
                    temp.GetComponent<SpringJoint2D>().enabled = false;
                    // Destroy it, and set PrevNode = to the temporary node.
                    Destroy(prevNode.gameObject);
                    prevNode = temp;
                    prevNode.rigidbody2D.isKinematic = true;
                    prevNode.rigidbody2D.GetComponent<WebNode>().prevNode = null;
                }
                break;

            // Reset Case
            case 3:
                // Reset all parts into their base sections.
                pointCounter = 0;
                currentNode = null;
                prevNode = null;
                fireState = 0;
                break;
        }
        #endregion
    }
}
