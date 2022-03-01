using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem bulletFire;

    GameObject cannonParent;
    float bulletSpeed = 0.050f;

    [HideInInspector] public bool isFired = false;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (isFired)
        {
            transform.Translate(Vector3.right * bulletSpeed);
        }
    }

    public void AssignCannonParent()
    {
        cannonParent = transform.parent.gameObject;
    }

    public void ActiveBulletFire()
    {
        bulletFire.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 12)
        {
            isFired = false;
            bulletFire.Stop();
            transform.parent = cannonParent.transform;
            gameObject.SetActive(false);
        }
    }
}
