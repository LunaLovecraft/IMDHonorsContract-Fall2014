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

    public bool firstNode;

    public int numNodes;

    public LayerMask layerMask;

    public Rigidbody2D webPoint;

    Rigidbody2D currentNode;

	// Use this for initialization
	void Start () {

        layerMask = ~layerMask;

        // Set to kinematic so we don't have it move while we're holding onto it.
        point.rigidbody2D.isKinematic = true;
        // Start with the spring off, since it won't start with a connected part.
        this.GetComponent<SpringJoint2D>().enabled = false;

        LineRenderer lineRenderer = (LineRenderer)GetComponent("LineRenderer");

        if (lineRenderer == null)
        {
            gameObject.AddComponent("LineRenderer");
        }
        lineRenderer = (LineRenderer)GetComponent("LineRenderer");
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
	}
	
    /// <summary>
    /// Activates all necessary parts of the webbing
    /// </summary>
    public void Activate()
    {
        point.rigidbody2D.isKinematic = false;
    }

    // Accurate collision detection.
    void Update ()
    {
        // Let's look for collisions with walls along the string
        if (nextNode != null)
        {
            // We'll use linecasting.
            RaycastHit2D myData = Physics2D.Linecast(this.transform.position, nextNode.transform.position, layerMask);

            //if (myData.collider != null && !myData.collider.isTrigger && myData.distance > 0.01f && ((Vector2)nextNode.transform.position - myData.point).magnitude > 0.01f)
            //{
            //    Vector2 location = myData.point;
                
            //    currentNode = Instantiate(webPoint, location, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
            //    FindObjectOfType<WebGun>().numNodes++;
            //    Debug.Log(currentNode);
            //    currentNode.GetComponent<WebNode>().nextNode = this.nextNode;
            //    currentNode.GetComponent<WebNode>().prevNode = this;
            //    nextNode.prevNode = currentNode.GetComponent<WebNode>();

            //    nextNode.GetComponent<SpringJoint2D>().connectedBody = currentNode.rigidbody2D;
            //    nextNode.GetComponent<SpringJoint2D>().distance *= (1 - myData.fraction);
            //    this.nextNode = currentNode.GetComponent<WebNode>();

            //    this.GetComponent<SpringJoint2D>().connectedBody = currentNode.rigidbody2D;
            //    this.GetComponent<SpringJoint2D>().distance *= (myData.fraction);

            //    currentNode.GetComponent<WebNode>().Activate();

            //    if (!myData.collider.CompareTag("Non-Stick"))
            //    {
            //        currentNode.GetComponent<WebNode>().Attach();
            //    }
            //}
        }
    }

    // Using FixedUpdate to ensure physi    cs things are accurate
	void FixedUpdate ()
    {
        if(firstNode)
        {
            LineRenderer lineRenderer = (LineRenderer)GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.SetVertexCount(numNodes);
                lineRenderer.SetWidth(0.1f, 0.1f);
                lineRenderer.SetColors(Color.white, Color.white);
            }

            WebNode temp = this;
            
            for(int i = 0; i < numNodes; i++)
            {
                if(temp != null)
                {
                    lineRenderer.SetPosition(i, temp.point.position);
                    temp = temp.nextNode;
                }
            }

        }

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
    void OnCollisionEnter2D(Collision2D info)
    {
        
        if (info.collider.gameObject.tag == "Node")
        {
            Debug.Log(info.collider.tag);
            Physics2D.IgnoreCollision(this.collider2D, info.collider.collider2D, false);
            Physics2D.IgnoreCollision(info.collider.collider2D, this.collider2D, false);
            return;
        }
        else if (info.collider.gameObject == this)
            return;
        else
        {
            if(!info.collider.CompareTag("Non-Stick")){
            Attach();
            }
        }
   
    }

    public void Attach()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }


}
