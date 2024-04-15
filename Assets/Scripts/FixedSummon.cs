using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FixedSummon : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    Vector3 last_position;
    private SpriteRenderer sprite_renderer;
    private Vector2 move_input;

    public void Init(Transform transform, float angle, float radius)
    {
        target = transform;
        float cos_angle = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin_angle = Mathf.Sin(angle * Mathf.Deg2Rad);
        offset = radius * (Vector3.right * cos_angle + Vector3.forward * sin_angle);
    }

    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        move_input.x = Input.GetAxisRaw("Horizontal");
        move_input.y = Input.GetAxisRaw("Vertical");
        if (move_input.x < 0)
        {
            sprite_renderer.flipX = true;
        }
        else if (move_input.x > 0)
        {
            sprite_renderer.flipX = false;
        }

        transform.position = target.position + offset;
        GetComponent<Animator>().SetBool("isWalking", transform.position != last_position);
        GetComponent<Animator>().SetBool("isIdle", transform.position == last_position);
        last_position = transform.position;
    }
}
