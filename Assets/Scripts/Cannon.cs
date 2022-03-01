using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    int numberOfBullets = 20;
    Transform currentBullet = null;
    Bullet currentBulletScript = null;

    void Awake()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bulletClone = Instantiate(bulletPrefab, transform);
            bulletClone.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentBullet = transform.GetChild(0);
            currentBulletScript = currentBullet.GetComponent<Bullet>();
            currentBullet.gameObject.SetActive(true);
            currentBulletScript.AssignCannonParent();
            currentBullet.parent = null;
            currentBulletScript.isFired = true;
            currentBulletScript.ActiveBulletFire();
        }
    }
}
