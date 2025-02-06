using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShotting : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 10f;
    [SerializeField] GameObject Bullet;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnFire();
        }
    }
    void OnFire()
    {
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent < Rigidbody2D >();
        rb.AddForce(transform.up * BulletSpeed, ForceMode2D.Impulse);

    }


}
