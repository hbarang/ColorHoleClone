using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectController : MonoBehaviour
{
    public GameObject plane;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            Physics.IgnoreCollision(plane.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
