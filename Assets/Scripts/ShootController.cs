﻿using UnityEngine;

public class ShootController
{
    #region Singleton
    private static ShootController _instance;

    public static ShootController Instance
    {
        get
        {
            if (_instance == null) _instance = new ShootController();
            return _instance;
        }
    }
    #endregion

    public void HitEffectSystem(bool isHit, ParticleSystem particleEffect, GameObject currentObj, GameObject parentObj)
    {
        isHit = false;
        particleEffect.Stop();
        currentObj.transform.parent = parentObj.transform;
        currentObj.transform.localPosition = Vector3.zero;
        currentObj.SetActive(false);
    }

    public Vector3 SetShootDirection(float sideValue)
    {
        Vector3 currentSide = Vector3.right;

        if (sideValue == -1.0f)
        {
            currentSide = Vector3.left;
        }

        return currentSide;
    }
}
