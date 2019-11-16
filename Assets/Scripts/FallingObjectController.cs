﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectController : MonoBehaviour
{
    Animator anim;
    public Color cubeColor;
    public bool isObjective;
    private GameObject stageGameObject;
    private void Start()
    {
        anim = GetComponent<Animator>();
        var cubeRenderer = this.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", cubeColor);

        stageGameObject = transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), GetComponent<Collider>());
            RotateThroughHole(other.transform);
            StartCoroutine(DestoryAfterAnimation());
        }
    }


    private void RotateThroughHole(Transform holeTransform)
    {
        Vector2 differenceVector = new Vector2(holeTransform.position.x - transform.position.x, holeTransform.position.z - transform.position.z);
        if (anim != null)
        {

            if (Mathf.Abs(differenceVector.x) > Mathf.Abs(differenceVector.y))
            {
                RotateZ(differenceVector.x);
            }
            else
            {
                RotateY(differenceVector.y);
            }
        }
    }

    private void RotateZ(float x)
    {
        if (x < 0)
        {
            anim.SetTrigger("RotateZInc");

        }
        else
        {
            anim.SetTrigger("RotateZDec");
        }
    }
    private void RotateY(float y)
    {
        if (y < 0)
        {
            anim.SetTrigger("RotateXDec");
        }
        else
        {
            anim.SetTrigger("RotateXInc");
        }
    }

    IEnumerator DestoryAfterAnimation()
    {
        yield return new WaitForSeconds(1);
        if (cubeColor == new Color(1, 1, 1, 0))
        {
            if (isObjective)
            {
                stageGameObject.GetComponent<StageController>().ObjectiveCount -= 1;
            }
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.instance.GameOver = true;
        }

    }
}
