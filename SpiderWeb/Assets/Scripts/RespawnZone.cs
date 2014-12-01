using UnityEngine;
using System.Collections;

public class RespawnZone : MonoBehaviour 
{
    public GameObject empty;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.Find("Player"))
        {
            other.GetComponent<Death>().empty = empty.transform;
        }

        Destroy(gameObject);
    }
    
}
