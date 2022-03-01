using UnityEngine;

public interface IShootController
{
    void AssignParent();

    void ActiveParticle();

    Vector3 ShootDirection(float sideValue);
}
