﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonGunBullet : MonoBehaviour, IShootController
{
    GameObject gunBulletParent = null;
    float gunBulletSpeed = 0.15f;

    [HideInInspector] public bool isLaunch = false;

    void LateUpdate()
    {
        if (isLaunch && transform.parent == null)
        {
            transform.Translate(Vector3.down * gunBulletSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
        {
            ShootController.Instance.HitEffectSystem(isLaunch, null, gameObject, gunBulletParent);
        }
    }

    public void AssignParent()
    {
        gunBulletParent = transform.parent.gameObject;
    }

    public void ActiveParticle()
    {
        
    }
}