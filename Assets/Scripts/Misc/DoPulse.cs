using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPulse : MonoBehaviour
{
    public float accelx, accely, accelz;
    public float resizingValue;
    public float maxSizeX, maxSizeY, maxSizeZ;
    bool upscaling = true;

    // Update is called once per frame
    void Update()
    {
        if(upscaling)
        {
            accelx += resizingValue * Time.unscaledDeltaTime;
            accely += resizingValue * Time.unscaledDeltaTime;
            accelz += resizingValue * Time.unscaledDeltaTime;

            transform.localScale = new Vector3(Mathf.Lerp(1, maxSizeX,accelx), Mathf.Lerp(1, maxSizeY,accely), Mathf.Lerp(1, maxSizeZ,accelz));

            if (transform.localScale.x >= maxSizeX || transform.localScale.y >= maxSizeY || transform.localScale.z >= maxSizeZ)
            {
                transform.localScale = new Vector3(maxSizeX, maxSizeY, maxSizeZ);
                upscaling = false;

                accelx = 0;
                accely = 0;
                accelz = 0;
            }
        }
        else
        {
            accelx += resizingValue * Time.unscaledDeltaTime;
            accely += resizingValue * Time.unscaledDeltaTime;
            accelz += resizingValue * Time.unscaledDeltaTime;

            transform.localScale = new Vector3(Mathf.Lerp(1, maxSizeX, maxSizeX - accelx), Mathf.Lerp(1, maxSizeY,maxSizeY - accely), Mathf.Lerp(1, maxSizeZ,maxSizeZ - accelz));

            if (transform.localScale.x <= 1 || transform.localScale.y <= 1 || transform.localScale.z <= 1)
            {
                
                transform.localScale = Vector3.one;
                upscaling = true;

                accelx = 0;
                accely = 0;
                accelz = 0;
            }
        }

        
    }
}
