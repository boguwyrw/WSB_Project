using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IShootController
{
    [SerializeField] ParticleSystem bulletFire;

    GameObject cannonParent = null;
    float bulletSpeed = 0.050f;

    [HideInInspector] public bool isFired = false;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (isFired)
        {
            transform.Translate(ShootDirection(transform.root.localScale.x) * bulletSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
        {
            isFired = false;
            bulletFire.Stop();
            transform.parent = cannonParent.transform;
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
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

    public Vector3 ShootDirection(float sideValue)
    {
        Vector3 currentSide;
        if (sideValue == 1.0f)
        {
            currentSide = Vector3.right;
        }
        else
        {
            currentSide = Vector3.left;
        }
        return currentSide;
    }
}
