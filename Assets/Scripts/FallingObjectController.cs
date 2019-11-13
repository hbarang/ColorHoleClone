using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectController : MonoBehaviour
{
    public GameObject plane;
    public float titlSpeed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            //Physics.IgnoreCollision(plane.GetComponent<Collider>(), GetComponent<Collider>());
            RotateThroughHole(other.transform);
        }
    }

    private void RotateThroughHole(Transform holeTransform)
    {
        Vector2 differenceVector = new Vector2(holeTransform.position.x-transform.position.x, holeTransform.position.z-transform.position.z);
        if(Mathf.Abs(differenceVector.x) > Mathf.Abs(differenceVector.y)){
            RotateZ(differenceVector.x);
        } 
        else{
            RotateY(differenceVector.y);
        }
    }

    private void RotateZ(float x){
    }
    private void RotateY(float y){

    }
}
