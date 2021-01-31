using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotEye : Eye
{
    [SerializeField] Camera cam;
    public override void Use()
    {
        Debug.Log("Using eyes" + eyeInfo.abilityName );
        Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("we hit" + hit.collider.gameObject.name);
        }
    }
}

