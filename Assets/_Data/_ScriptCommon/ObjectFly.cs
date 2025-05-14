using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFly : TGMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected Vector3 direction = Vector3.forward;

    void Update()
    {
        transform.parent.Translate(this.direction * this.moveSpeed * Time.deltaTime);
    }
}