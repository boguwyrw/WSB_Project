using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject toxicSubstancePrefab;
    
    SkinnedMeshRenderer enemySkinnedMesh;
    SkinnedMeshRenderer toxicSubstanceSkinnedMesh;
    float increaser = 0.0f;
    float increaserValue = 2.0f;
    float maxValue = 100.0f;
    float viewRadius = 28.0f; //12.0f

    int numberOfSubstances = 5;
    int actionStage = 0;
    float timeToNextShoot = 1.5f;
    float timeCounter = 0.0f;
    bool currentSubstanceSet = false;
    Transform currentToxicSubstance = null;
    Transform raycastPoint = null;
    ToxicSubstance toxicSubstanceScript = null;

    void Awake()
    {
        for (int i = 0; i < numberOfSubstances; i++)
        {
            GameObject toxicSubstanceClone = Instantiate(toxicSubstancePrefab, transform);
            toxicSubstanceClone.SetActive(false);
        }
    }

    void Start()
    {
        enemySkinnedMesh = GetComponent<SkinnedMeshRenderer>();
        timeCounter = timeToNextShoot;
        raycastPoint = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        PlayerDetectionFunctionality();
    }

    void PlayerDetectionFunctionality()
    {
        RaycastHit raycastHit;
        Vector3 directionVector = Vector3.zero;

        if (transform.localScale.x == 1.0f)
        {
            directionVector = raycastPoint.right;
        }
        else if (transform.localScale.x == -1.0f)
        {
            directionVector = -1 * raycastPoint.right;
        }

        if (Physics.Raycast(raycastPoint.position, directionVector, out raycastHit, viewRadius))
        {
            PlayerInRangeFunctionality(raycastHit);
            
        }
    }

    void PlayerInRangeFunctionality(RaycastHit hit)
    {
        if (hit.collider.gameObject.layer == 9)
        {
            //Debug.Log(raycastHit.collider.name);
            currentSubstanceSet = false;
            EnemyShootAction();
        }
        else if (increaser > 0.0f)
        {
            increaser = increaser - increaserValue;

            if (increaser <= 0.0f)
            {
                increaser = 0.0f;
            }

            if (!currentSubstanceSet)
            {
                currentSubstanceSet = true;
                SetCurrentToxicSubstance();
            }

            enemySkinnedMesh.SetBlendShapeWeight(0, increaser);
            toxicSubstanceSkinnedMesh.SetBlendShapeWeight(0, increaser);
        }
    }

    void EnemyShootAction()
    {
        switch(actionStage)
        {
            case 0:
                SetToxicSubstance();
                break;
            case 1:
                IncreaserToxicSubstance();
                break;
            case 2:
                ShootToxicSubstance();
                break;
            case 3:
                ShootToxicSubstancePause();
                break;
            case 4:
                ResetTimeCounter();
                break;
        }
    }

    void SetToxicSubstance() // case 0
    {
        SetCurrentToxicSubstance();
        toxicSubstanceScript = currentToxicSubstance.GetComponent<ToxicSubstance>();
        currentToxicSubstance.gameObject.SetActive(true);
        toxicSubstanceScript.AssignParent();

        actionStage++;
    }

    void SetCurrentToxicSubstance()
    {
        currentToxicSubstance = transform.GetChild(1);
        toxicSubstanceSkinnedMesh = currentToxicSubstance.GetComponent<SkinnedMeshRenderer>();
    }

    void IncreaserToxicSubstance() // case 1
    {
        if (increaser < maxValue)
        {
            increaser = increaser + increaserValue;
        }
        else if (increaser >= maxValue)
        {
            increaser = 100.0f;
            actionStage++;
        }

        enemySkinnedMesh.SetBlendShapeWeight(0, increaser);
        toxicSubstanceSkinnedMesh.SetBlendShapeWeight(0, increaser);
    }

    void ShootToxicSubstance() // case 2
    {
        currentToxicSubstance.parent = null;
        toxicSubstanceScript.isShoot = true;
        toxicSubstanceScript.ActiveParticle();

        actionStage++;
    }

    void ShootToxicSubstancePause() // case 3
    {
        
        if (increaser > 0.0f)
        {
            increaser = increaser - increaserValue;
        }
        else if (increaser <= 0.0f)
        {
            increaser = 0.0f;
        }

        enemySkinnedMesh.SetBlendShapeWeight(0, increaser);
        
        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0.0f)
        {
            actionStage++;
        }
    }

    void ResetTimeCounter() // case 4
    {
        timeCounter = timeToNextShoot;
        actionStage = 0;
    }
}
