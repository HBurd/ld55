using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    static GameObject instance;
    static GameObject player;

    public static T Get<T>()
    {
        if (!instance)
        {
            instance = GameObject.Find("/Singletons");
        }
        return instance.GetComponent<T>();
    }

    public static GameObject GetPlayer()
    {
        if (!player)
        {
            player = GameObject.Find("/Player");
        }

        return player;
    }
}
