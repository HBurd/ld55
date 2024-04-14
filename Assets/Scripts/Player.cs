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

    int num_rotating_summons;

    void Update()
    {
        Vector3 velocity = Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right;
        velocity = Vector3.ClampMagnitude(velocity, 1.0f) * max_speed;
        velocity *= max_speed;
        transform.position += Time.deltaTime * velocity;

        if (Input.GetButtonDown("Summon1"))
        {
            RotatingSummon summon = Instantiate(rotating_summon).GetComponent<RotatingSummon>();
            summon.SetTarget(transform);
            summon.SetIndex(num_rotating_summons);
            num_rotating_summons += 1;
        }

        Collider[] collectibles = Physics.OverlapSphere(transform.position, collect_radius, LayerMask.GetMask("Collectible"));
        foreach (Collider collectible in collectibles)
        {
            int item_type = collectible.GetComponent<Item>().GetItemType();
            Singleton.Get<Inventory>().CollectItem(item_type);
            Destroy(collectible.gameObject);
        }
    }
}
