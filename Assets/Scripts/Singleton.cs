using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    static GameObject instance;

    public static T Get<T>()
    {
        if (!instance)
        {
            instance = GameObject.Find("/Singletons");
        }
        return instance.GetComponent<T>();
    }
}
