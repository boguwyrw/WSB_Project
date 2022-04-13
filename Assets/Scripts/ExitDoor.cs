using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject exit;

    BoxCollider doorCollider;
    Vector3 doorColliderCenter = new Vector3(1.75f, 2.0f, 0.0f);
    Vector3 doorColliderSize = new Vector3(1.25f, 4.0f, 1.0f);

    void Start()
    {
        doorCollider = transform.GetChild(0).GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (GameController.Instance.objectsToDestroyInStage_2_Number == 0)
        {
            exit.SetActive(true);
            doorCollider.center = doorColliderCenter;
            doorCollider.size = doorColliderSize;
            doorCollider.isTrigger = true;
        }
    }
}
