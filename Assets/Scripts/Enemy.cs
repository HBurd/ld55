using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float movement_speed;

    Transform player_tf;

    void Start()
    {
        player_tf = Singleton.GetPlayer().GetComponent<Transform>();
    }

    void Update()
    {

    }
}
