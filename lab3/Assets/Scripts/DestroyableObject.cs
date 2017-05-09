using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public float forceRequired = 8.0f;

    private void OnCollisionEnter(Collision col)
    {
        if(col.impulse.magnitude > forceRequired)
        {
            Destroy(gameObject);
        }
    }
}
