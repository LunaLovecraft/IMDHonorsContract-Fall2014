using UnityEngine;
using System.Collections;

public class wallCollide : MonoBehaviour {

    public float stability = 10;
    private float force;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            force = col.gameObject.GetComponent<CharacterVariable>().Force;
            if (force > stability)
            {
                Destroy(gameObject);
            }
        }
    }
}
