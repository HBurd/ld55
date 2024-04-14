using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSummon : MonoBehaviour
{
    [SerializeField]
    float rotation_speed;

    [SerializeField]
    float radius;

    [SerializeField]
    Transform target;

    [SerializeField]
    float phase_increment;

    int index;

    void Update()
    {
        float rotation_period = 360.0f / rotation_speed;
        float angle = (float)(Time.timeAsDouble % (double)rotation_period) / rotation_period * 360.0f + index * phase_increment;
        float sin_angle = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos_angle = Mathf.Cos(angle * Mathf.Deg2Rad);
        transform.position = (Vector3.right * cos_angle + Vector3.forward * sin_angle) * radius + target.position;
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
    }

    public void SetIndex(int new_index)
    {
        index = new_index;
    }
}
