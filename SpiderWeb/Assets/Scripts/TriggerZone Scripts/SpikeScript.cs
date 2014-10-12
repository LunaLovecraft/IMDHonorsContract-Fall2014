using UnityEngine;
using System.Collections;

public class SpikeScript : AbstractTriggerZone {

    /// <summary>
    /// When the player enters the spike zone "kill" them.
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameObject.Find("Player"))
        {
            //Kill code here.
            //other.GetComponent<PlayerScript>.Reset();
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
    }
}
