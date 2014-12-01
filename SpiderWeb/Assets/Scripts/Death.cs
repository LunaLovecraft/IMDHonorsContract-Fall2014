using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public Transform empty;
    public void Die()
    {
        transform.position = empty.position;
    }
}
