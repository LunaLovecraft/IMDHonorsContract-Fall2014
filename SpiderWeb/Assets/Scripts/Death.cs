using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	// Use this for initialization
    public void Deathh(Transform empty)
    {
        transform.position = empty.position;
    }
}
