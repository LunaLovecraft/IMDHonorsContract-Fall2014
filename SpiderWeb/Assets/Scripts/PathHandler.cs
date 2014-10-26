using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathHandler : MonoBehaviour
{

    /// <summary>
    /// All of the nodes the carriage visits.
    /// </summary>
    public List<Vector2> locations;

    /// <summary>
    /// Whether the carriage bounces or loops.  true = bounces
    /// </summary>
    public bool ping;

    /// <summary>
    /// Speed carriage moves at per step
    /// </summary>
    public float speed;

    /// <summary>
    /// The previous node's index
    /// </summary>
    private int prevNode = 0;

    /// <summary>
    /// The direction the cart is heading in.  true = forward
    /// </summary>
    private bool direction = true;

    /// <summary>
    /// The movement added each round.
    /// </summary>
    private Vector2 movementVector;


    // Use this for initialization
    void Start()
    {
        // This is invalid if we have fewer than two locatinos.
        if (locations.Count >= 2)
        {
            this.transform.position = locations[0];
            movementVector = (locations[1] - locations[0]).normalized;
            movementVector.Scale(new Vector2(speed, speed));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Movement is invalid with fewer than two locations.
        if (locations.Count < 2)
        {
            return;
        }

        //  See if we're heading forward or backwards
        if (direction)
        {
            // Have we gone too far?
            if ((((Vector2)this.transform.position - locations[prevNode]).magnitude - (locations[prevNode + 1] - locations[prevNode]).magnitude) >= 0)
            {
                // Go back if we're too far.
                this.transform.position = locations[prevNode + 1];

                // advance the track
                prevNode++;

                // See if we're at an end of the track
                if (prevNode == locations.Count - 1)
                {
                    // If we are then ping us if we ping, or loop otherwise
                    if (ping)
                    {
                        direction = false;
                        movementVector = (locations[prevNode - 1] - locations[prevNode]).normalized;
                        movementVector.Scale(new Vector2(speed, speed));
                    }
                    else
                    {
                        prevNode = 0;
                        this.transform.position = locations[0];
                        movementVector = (locations[prevNode + 1] - locations[prevNode]).normalized;
                        movementVector.Scale(new Vector2(speed, speed));
                    }
                }
                // Not at the end of the track?  Keep moving!
                else
                {
                    movementVector = (locations[prevNode + 1] - locations[prevNode]).normalized;
                    movementVector.Scale(new Vector2(speed, speed));
                }
            }
        }
        // Heading in the opposite direction.
        else
        {
            // Have we gone too far?
            if ((((Vector2)this.transform.position - locations[prevNode]).magnitude - (locations[prevNode - 1] - locations[prevNode]).magnitude) >= 0)
            {
                // Move back if we're too far
                this.transform.position = locations[prevNode - 1];

                // Advance the track
                prevNode--;

                // Are we at the beginning of the track?
                if(prevNode == 0)
                {
                    direction = true;
                    movementVector = (locations[prevNode + 1] - locations[prevNode]).normalized;
                    movementVector.Scale(new Vector2(speed, speed));
                }
            }
            // Keep moving
            else
            {
                movementVector = (locations[prevNode - 1] - locations[prevNode]).normalized;
                movementVector.Scale(new Vector2(speed, speed));
            }
        }
        // Apply the movement
        this.transform.position += (Vector3)movementVector;

    }

}
