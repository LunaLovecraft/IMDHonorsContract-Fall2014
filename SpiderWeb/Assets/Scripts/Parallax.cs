using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    private Vector3 prevCharacterPos;
    public GameObject actor;
    public float speed = 10;

	void Start () 
    {
        prevCharacterPos = actor.transform.position;	
	}
	
	void Update () 
    {
        Vector3 deltaCharPos = actor.transform.position - prevCharacterPos;
        transform.position = new Vector3(transform.position.x - deltaCharPos.x /speed, transform.position.y - deltaCharPos.y / speed, transform.position.z);
        prevCharacterPos = actor.transform.position;
	}
}
