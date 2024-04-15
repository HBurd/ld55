using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    float max_speed;

    [SerializeField]
    float collect_radius;

    [SerializeField]
    GameObject rotating_summon;

    [SerializeField]
    GameObject fixed_summon;

    [SerializeField]
    GameObject floating_summon;

    [SerializeField]
    float fixed_summon_angle_rate;

    [SerializeField]
    float fixed_summon_radius;

    [SerializeField]
    Transform fixed_summon_indicator;

    public Vector3 floating_summon_offset = new Vector3(0, 10, 0);

    private GameObject flying_monke;
    private Vector2 move_input;
    private SpriteRenderer sprite_renderer;
    int num_rotating_summons;

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
        Vector3 velocity = Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right;
        velocity = Vector3.ClampMagnitude(velocity, 1.0f) * max_speed;
        velocity *= max_speed;
        transform.position += Time.deltaTime * velocity;

        bool walking = velocity.magnitude > 0.01f;
        GetComponent<Animator>().SetBool("isWalking", walking);
        GetComponent<Animator>().SetBool("isIdle", !walking);

        if (Input.GetButtonDown("Summon1") && Singleton.Get<Inventory>().GetCount(0) > 0)
        {
            RotatingSummon summon = Instantiate(rotating_summon).GetComponent<RotatingSummon>();
            summon.SetTarget(transform);
            summon.SetIndex(num_rotating_summons);
            num_rotating_summons += 1;
            Singleton.Get<Inventory>().AdjustItem(0, -1);
        }

        if (Singleton.Get<Inventory>().GetCount(1) > 0)
        {

            float fixed_summon_angle_period = 360.0f / fixed_summon_angle_rate;
            float fixed_summon_angle = 360.0f - (float)(Time.timeAsDouble % (double)fixed_summon_angle_period) / fixed_summon_angle_period * 360.0f;

            float cos_angle = Mathf.Cos(fixed_summon_angle * Mathf.Deg2Rad);
            float sin_angle = Mathf.Sin(fixed_summon_angle * Mathf.Deg2Rad);
            fixed_summon_indicator.gameObject.SetActive(true);
            fixed_summon_indicator.position = fixed_summon_radius * (Vector3.right * cos_angle + Vector3.forward * sin_angle) + transform.position;

            if (Input.GetButtonDown("Summon2"))
            {
                FixedSummon summon = Instantiate(fixed_summon).GetComponent<FixedSummon>();
                summon.Init(transform, fixed_summon_angle, fixed_summon_radius);
                Singleton.Get<Inventory>().AdjustItem(1, -1);
            }
        }
        else
        {
            fixed_summon_indicator.gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Summon3") && Singleton.Get<Inventory>().GetCount(2) > 0)
        {
            if (flying_monke == null)
            {
                flying_monke = Instantiate(floating_summon, transform.position + floating_summon_offset, Quaternion.identity);
                Singleton.Get<Inventory>().AdjustItem(2, -1);
            }
            else
            {
                Singleton.Get<Inventory>().AdjustItem(2, -1);
            }
        }

        if (flying_monke != null)
        {
            flying_monke.transform.position = transform.position + floating_summon_offset;
        }

        Collider[] collectibles = Physics.OverlapSphere(transform.position, collect_radius, LayerMask.GetMask("Collectible"));
        foreach (Collider collectible in collectibles)
        {
            int item_type = collectible.GetComponent<Item>().GetItemType();
            Singleton.Get<Inventory>().AdjustItem(item_type, 1);
            Destroy(collectible.gameObject);
        }
    }

    // void LateUpdate()
    // {
    //     floating_summon.transform.position = transform.position + floating_summon_offset;
    // }
}
