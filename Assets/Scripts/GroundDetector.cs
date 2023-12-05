using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask targetMask;

    public bool isDetected;

    private void FixedUpdate()
    {
        isDetected = Detect();
    }

    bool Detect()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius,targetMask);

        if(collider != null)

            return true;

        else

            return false;

    }
}
