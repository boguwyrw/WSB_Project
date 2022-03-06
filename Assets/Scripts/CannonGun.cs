﻿using System;
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
    float barrelRotationSpeed = 0.2f;
    float barrelCurrentRotationSpeed = 0.0f;
    float viewLength = 46.0f;
    float timeToNextLaunch = 0.4f;
    float timeCounter = 0.0f;
    bool isRotateForward = true;
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

    void FixedUpdate()
    {
        RaycastHit raycastHit;

        //if (Physics.Raycast(cannonGunRaycastPoint.position, Vector3.down, out raycastHit, viewLength))
        if (Physics.Raycast(cannonGunRaycastPoint.position, -cannonGunRaycastPoint.up, out raycastHit, viewLength))
        {
            Debug.DrawRay(cannonGunRaycastPoint.position, -cannonGunRaycastPoint.up * viewLength/2, Color.green);
            //Debug.Log(transform.GetSiblingIndex() + " - " + raycastHit.collider.name);
            if (raycastHit.collider.gameObject.layer == 9)
            {
                CannonGunShootAction();
            }
        }
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
        currentGunBullet = transform.GetChild(1);
        currentGunBullet.position = cannonGunRaycastPoint.position;
        currentGunBullet.rotation = cannonGunRaycastPoint.rotation;
        cannonGunBulletScript = currentGunBullet.GetComponent<CannonGunBullet>();
        currentGunBullet.gameObject.SetActive(true);
        cannonGunBulletScript.AssignParent();

        actionStage++;
    }

    void LaunchCannonGunBullet()
    {
        currentGunBullet.parent = null;
        cannonGunBulletScript.isLaunch = true;

        actionStage++;
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

    void LateUpdate()
    {
        //Debug.Log(barrel.rotation.z);
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
}