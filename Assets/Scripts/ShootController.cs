using UnityEngine;

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
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
        currentObj.transform.parent = parentObj.transform;
        currentObj.transform.localPosition = Vector3.zero;
        currentObj.transform.localRotation = Quaternion.identity;
        currentObj.transform.localScale = Vector3.one;
        currentObj.SetActive(false);
    }

    public Vector3 SetShootDirection(float sideValue)
    {
        Vector3 currentSide = Vector3.right;

        if (sideValue < 0.0f)
        {
            currentSide = Vector3.left;
        }

        return currentSide;
    }
}
