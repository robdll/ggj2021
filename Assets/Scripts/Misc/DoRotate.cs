using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoRotate : MonoBehaviour
{
    public float accelx, accely, accelz;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(accelx * Time.unscaledDeltaTime, accely * Time.unscaledDeltaTime, accelz * Time.unscaledDeltaTime);
    }
}
