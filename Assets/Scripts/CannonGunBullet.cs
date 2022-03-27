using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonGunBullet : MonoBehaviour, IShootController
{
    GameObject gunBulletParent = null;
    float gunBulletSpeed = 0.2f;

    [HideInInspector] public bool isLaunch = false;
    [HideInInspector] public int damageValue = 25;

    void LateUpdate()
    {
        if (isLaunch && transform.parent == null)
        {
            transform.Translate(Vector3.down * gunBulletSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            GameController.Instance.UpdatePlayerHealth(damageValue);
            Debug.Log("YOU HAVE BEEN HIT CANNON GUN BULLET");
            ShootController.Instance.HitEffectSystem(isLaunch, null, gameObject, gunBulletParent);
        }

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
