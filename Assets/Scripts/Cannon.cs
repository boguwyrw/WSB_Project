using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    int numberOfBullets = 20;
    float timeToNextFire = 1.2f;
    float timeToFire = 0.0f;
    bool nextFireAvailable = false;
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

    void Update()
    {
        if (nextFireAvailable)
        {
            timeToFire -= Time.deltaTime;
        }

        if (timeToFire < 0.0f)
        {
            nextFireAvailable = false;
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && timeToFire <= 0.0f)
        {
            currentBullet = transform.GetChild(0);
            currentBulletScript = currentBullet.GetComponent<Bullet>();
            currentBullet.gameObject.SetActive(true);
            currentBulletScript.AssignParent();
            currentBullet.parent = null;
            currentBulletScript.isFired = true;
            currentBulletScript.ActiveParticle();
            timeToFire = timeToNextFire;
            nextFireAvailable = true;
        }
    }
}
