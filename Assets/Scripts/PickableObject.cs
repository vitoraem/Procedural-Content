using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private Rigidbody objectRB;
    private Transform pickPoint;
    public float lerpSpeed = 10f;

    // Update is called once per frame
    void Awake()
    {
        objectRB = GetComponent<Rigidbody>();
    }

    public void Pick(Transform pickPoint)
    {
        this.pickPoint = pickPoint;
        objectRB.useGravity = false;
        
    }

    public void Drop()
    {
        this.pickPoint = null;
        objectRB.useGravity = true;
        
    }

    private void FixedUpdate()
    {
        if(pickPoint != null)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, pickPoint.position, Time.deltaTime * lerpSpeed);
            objectRB.MovePosition(newPos);
        }
    }


}
