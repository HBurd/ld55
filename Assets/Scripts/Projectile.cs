using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 velocity;
    double life_end;

    public void SetVelocity(Vector3 v)
    {
        velocity = v;
    }

    public void SetLifetime(double life)
    {
        life_end = Time.timeAsDouble + life;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * velocity;
        if (Time.timeAsDouble > life_end)
        {
            Destroy(gameObject);
        }
    }
}
