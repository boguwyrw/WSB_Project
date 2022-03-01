﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSubstance : MonoBehaviour, IShootController
{
    [SerializeField] ParticleSystem toxicSmoke;

    SkinnedMeshRenderer substanceskinnedMesh;
    GameObject enemyParent = null;
    float toxicSubstanceSpeed = 0.15f;

    [HideInInspector] public bool isShoot = false;

    void Start()
    {
        substanceskinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    void LateUpdate()
    {
        if (isShoot && transform.parent == null)
        {
            transform.Translate(ShootDirection(transform.root.localScale.x) * toxicSubstanceSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            substanceskinnedMesh.SetBlendShapeWeight(0, 0.0f);
            ShootController.Instance.HitEffectSystem(isShoot, toxicSmoke, gameObject, enemyParent);
        }
    }

    public void AssignParent()
    {
        enemyParent = transform.parent.gameObject;
    }

    public void ActiveParticle()
    {
        toxicSmoke.Play();
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
