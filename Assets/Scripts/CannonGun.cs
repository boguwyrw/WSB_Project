using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonGun : MonoBehaviour
{
    [SerializeField] GameObject cannonGunBulletPrefab;
    [SerializeField] Transform barrel;
    [SerializeField] Transform cannonGunRaycastPoint;

    int numberOfGunBullets = 10;
    int actionStage = 0;
    float barrelAngleValue = 0.42f;
    float barrelRotationSpeed = 0.28f;
    float barrelCurrentRotationSpeed = 0.0f;
    float viewLength = 46.0f;
    float timeToNextLaunch = 0.5f;
    float timeCounter = 0.0f;
    bool isRotateForward = true;
    bool canLaunchCannonGunBullet = false;
    Transform currentGunBullet = null;
    CannonGunBullet cannonGunBulletScript = null;

    void Awake()
    {
        for (int i = 0; i < numberOfGunBullets; i++)
        {
            GameObject gunBulletClone = Instantiate(cannonGunBulletPrefab, transform);
            gunBulletClone.SetActive(false);
        }
    }

    void Start()
    {
        barrelCurrentRotationSpeed = barrelRotationSpeed;
        timeCounter = timeToNextLaunch;
    }

    void Update()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(cannonGunRaycastPoint.position, -cannonGunRaycastPoint.up, out raycastHit, viewLength))
        {
            Debug.DrawRay(cannonGunRaycastPoint.position, -cannonGunRaycastPoint.up * viewLength/2, Color.green);
            if (raycastHit.collider.gameObject.layer == 9)
            {
                canLaunchCannonGunBullet = true;
            }
            else
            {
                canLaunchCannonGunBullet = false;
            }
        }

        CannonGunShootAction();
    }
    
    void LateUpdate()
    {
        if (isRotateForward && barrel.rotation.z >= barrelAngleValue)
        {
            barrelCurrentRotationSpeed = -barrelRotationSpeed;
            isRotateForward = !isRotateForward;
        }
        else if (!isRotateForward && barrel.rotation.z <= -barrelAngleValue)
        {
            barrelCurrentRotationSpeed = barrelRotationSpeed;
            isRotateForward = !isRotateForward;
        }

        barrel.Rotate(Vector3.forward * barrelCurrentRotationSpeed);
    }
    
    private void CannonGunShootAction()
    {
        switch(actionStage)
        {
            case 0:
                SetCannonGunBullet();
                break;
            case 1:
                LaunchCannonGunBullet();
                break;
            case 2:
                ReloadCannonGunBullet();
                break;
            case 3:
                ResetTimeCounter();
                break;
        }
    }

    void SetCannonGunBullet()
    {
        if (canLaunchCannonGunBullet)
        {
            currentGunBullet = transform.GetChild(1);
            currentGunBullet.position = cannonGunRaycastPoint.position;
            currentGunBullet.rotation = cannonGunRaycastPoint.rotation;
            cannonGunBulletScript = currentGunBullet.GetComponent<CannonGunBullet>();
            currentGunBullet.gameObject.SetActive(true);
            cannonGunBulletScript.AssignParent();

            actionStage++;
        }  
    }

    void LaunchCannonGunBullet()
    {
        if (canLaunchCannonGunBullet)
        {
            currentGunBullet.parent = null;
            cannonGunBulletScript.isLaunch = true;

            actionStage++;
        }
    }

    void ReloadCannonGunBullet()
    {
        timeCounter -= Time.deltaTime;

        if (timeCounter <= 0.0f)
        {
            actionStage++;
        }
    }

    void ResetTimeCounter()
    {
        timeCounter = timeToNextLaunch;
        actionStage = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            gameObject.SetActive(false);
            GameController.Instance.objectsToDestroyNumber--;
        }
    }
}
