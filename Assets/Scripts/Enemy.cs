using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject toxicSubstancePrefab;
    [SerializeField] Vector2 walkingLimits;
    [SerializeField] bool isWalkingRight;

    SkinnedMeshRenderer enemySkinnedMesh;
    SkinnedMeshRenderer toxicSubstanceSkinnedMesh;
    float increaser = 0.0f;
    float increaserValue = 2.5f;
    float maxValue = 100.0f;
    float viewRadius = 46.0f; //12.0f
    float directionValue = 1.0f;

    int numberOfSubstances = 6;
    int actionStage = 0;
    float timeToNextShoot = 1.25f;
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

    void Update()
    {
        PlayerDetectionFunctionality();
    }

    void LateUpdate()
    {
        if (isWalkingRight && transform.position.x >= walkingLimits.y)
        {
            transform.localScale = SetNewEnemyDirection(-directionValue);
            isWalkingRight = !isWalkingRight;
        }
        else if (!isWalkingRight && transform.position.x <= walkingLimits.x)
        {
            transform.localScale = SetNewEnemyDirection(directionValue);
            isWalkingRight = !isWalkingRight;
        }

        transform.Translate(EnemyCurrentDirection() * Time.deltaTime);
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
            currentSubstanceSet = false;
            EnemyShootAction();
        }
        else if (increaser > 0.0f && hit.collider.gameObject.layer != 9)
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

    Vector3 EnemyCurrentDirection()
    {
        Vector3 enemyDirection = Vector3.right;
        if (transform.localScale.x == -1.0f)
            enemyDirection = Vector3.left;
 
        return enemyDirection;
    }

    Vector3 SetNewEnemyDirection(float dirValue)
    {
        return new Vector3(dirValue, 1.0f, 1.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            gameObject.SetActive(false);
            if (transform.parent.name.Equals("Stage_1"))
            {
                GameController.Instance.objectsToDestroyInStage_1_Number--;
            }
            else if (transform.parent.name.Equals("Stage_2"))
            {
                GameController.Instance.objectsToDestroyInStage_2_Number--;
            }
        }
    }
}
