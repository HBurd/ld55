using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSummon : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    Vector3 last_position;

    private SpriteRenderer sprite_renderer;
    private Vector2 move_input;
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
            sprite_renderer.flipX = false;
        }
        else if (move_input.x > 0)
        {
            sprite_renderer.flipX = true;
        }
    }
}
