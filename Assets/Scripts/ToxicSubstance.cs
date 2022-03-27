using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSubstance : MonoBehaviour, IShootController
{
    [SerializeField] ParticleSystem toxicSmoke;

    SkinnedMeshRenderer substanceskinnedMesh;
    GameObject enemyParent = null;
    float toxicSubstanceSpeed = 0.25f;

    [HideInInspector] public bool isShoot = false;
    [HideInInspector] public int damageValue = 20;

    void Start()
    {
        substanceskinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    void LateUpdate()
    {
        if (isShoot && transform.parent == null)
        {
            Vector3 shootDirection = ShootController.Instance.SetShootDirection(transform.root.localScale.x);
            transform.Translate(shootDirection * toxicSubstanceSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            if (other.gameObject.layer == 9)
            {
                GameController.Instance.UpdatePlayerHealth(damageValue);
                Debug.Log("YOU HAVE BEEN HIT BY TOXIC SUBSTANCE");
            }

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
}
