using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotEye : Eye
{
   // Camera cam;
    public GameObject bulletPrefab;

    private void OnEnable()
    {
      //  cam = GetComponentInParent<Camera>();
    }
    public override void Use()
    {
        Debug.Log("Using eyes" + eyeInfo.abilityName );
        Shoot();
    }

    void Shoot()
    {
        // Ray ray = new Ray(transform.position, transform.forward);
        ////ray.origin = cam.transform.position;
        // if(Physics.Raycast(ray, out RaycastHit hit))
        // {

        //hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage( ((EyesInfo)eyeInfo).damage );
        //   Debug.Log("we hit" + hit.collider.gameObject.name);
        // }
        GameObject go = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
        PlayerController pc = GetComponentInParent<PlayerController>();
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.player = pc;
      //  go.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * bulletSpeed);
    }
}

