using UnityEngine;
using System.Collections;

public class WindScript : AbstractTriggerZone {

    public Vector2 windForce;

    /// <summary>
    /// Apply a wind force to the object
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerStay2D(Collider2D other)
    {
        other.rigidbody2D.AddForce(windForce);
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
    }
}
