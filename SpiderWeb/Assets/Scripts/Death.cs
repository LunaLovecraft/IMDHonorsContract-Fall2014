using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	// Use this for initialization
    public void Death(Transform empty)
    {
        transform.position = empty.position;
    }
}
