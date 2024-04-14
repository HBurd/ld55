using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.rotation = //Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
            Camera.main.transform.rotation;// * Quaternion.AngleAxis(90.0f, Vector3.right);
    }
}
