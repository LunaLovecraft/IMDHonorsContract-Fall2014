using UnityEngine;
using System.Collections;

public class ExampleTriggerZoneScript : AbstractTriggerZone {

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.ToString() + " has entered trigger zone " + this.name);
    }

    public override void OnTriggerStay(Collider other)
    {
        Debug.Log(other.ToString() + " is staying in trigger zone " + this.name);
    }

    public override void OnTriggerExit(Collider other)
    {
        Debug.Log(other.ToString() + " has left trigger zone " + this.name);
    }
}
