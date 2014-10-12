using UnityEngine;
using System.Collections;

public abstract class AbstractTriggerZone : MonoBehaviour {

    /// <summary>
    /// When something enters the plane the following code triggers.
    /// </summary>
    /// <param name="other">The object in the trigger zone.</param>
    public abstract void OnTriggerEnter2D(Collider2D other);

    /// <summary>
    /// While the object in the plane is in the plane the following code runs.
    /// </summary>
    /// <param name="other">The object in the trigger zone.</param>
    public abstract void OnTriggerStay2D(Collider2D other);

    /// <summary>
    /// When the object leaves the plane the following code runs.
    /// </summary>
    /// <param name="other">The object in the trigger zone.</param>
    public abstract void OnTriggerExit2D(Collider2D other);

}
