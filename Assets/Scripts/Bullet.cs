using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IShootController
{
    [SerializeField] ParticleSystem bulletFire;

    GameObject cannonParent = null;
    float bulletSpeed = 0.12f;

    [HideInInspector] public bool isFired = false;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (isFired && transform.parent == null)
        {
            transform.Translate(ShootController.Instance.SetShootDirection(transform.root.localScale.x) * bulletSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
        {
            ShootController.Instance.HitEffectSystem(isFired, bulletFire, gameObject, cannonParent);
        }
    }

    public void AssignParent()
    {
        cannonParent = transform.parent.gameObject;
    }

    public void ActiveParticle()
    {
        bulletFire.Play();
    }
}
