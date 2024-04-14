using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FixedSummon : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    Vector3 last_position;

    public void Init(Transform transform, float angle, float radius)
    {
        target = transform;
        float cos_angle = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin_angle = Mathf.Sin(angle * Mathf.Deg2Rad);
        offset = radius * (Vector3.right * cos_angle + Vector3.forward * sin_angle);
    }

    void Update()
    {
        transform.position = target.position + offset;
        GetComponent<Animator>().SetBool("isWalking", transform.position != last_position);
        GetComponent<Animator>().SetBool("isIdle", transform.position == last_position);
        last_position = transform.position;
    }
}
