using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float movement_speed;

    [SerializeField]
    float min_player_distance;

    [SerializeField]
    float range;

    [SerializeField]
    float projectile_speed;

    [SerializeField]
    double attack_cooldown;

    [SerializeField]
    double attack_windup_time;

    [SerializeField]
    double attack_stop_time;

    // These are absolute
    double next_attack_time;
    double move_stop_time;
    double actual_attack_time;  // i.e. after windup
    private SpriteRenderer sprite_renderer;
    Transform player_tf;

    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        player_tf = Singleton.GetPlayer().GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 player_distance = player_tf.position - transform.position;
        float magnitude = player_distance.magnitude;

        if (Time.timeAsDouble > move_stop_time && magnitude > min_player_distance)
        {
            Vector3 velocity = player_distance.normalized * movement_speed;
            transform.position += velocity * Time.deltaTime;

            if (velocity.x > 0)
            {
                sprite_renderer.flipX = false;
            }
            else if (velocity.x < 0)
            {
                sprite_renderer.flipX = true;
            }
        }

        if (magnitude < range && Time.timeAsDouble > next_attack_time)
        {
            next_attack_time = Time.timeAsDouble + attack_cooldown;
            move_stop_time = Time.timeAsDouble + attack_stop_time;
            actual_attack_time = Time.timeAsDouble + attack_windup_time;
        }

        if (Time.timeAsDouble > actual_attack_time)
        {
            actual_attack_time = double.MaxValue;
            Projectile proj = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>();
            proj.SetVelocity(projectile_speed * player_distance.normalized);
            proj.SetLifetime(range / projectile_speed);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Muscle"))
    //    {

    //    }
    //}
}
