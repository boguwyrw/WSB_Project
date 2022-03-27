using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateController : MonoBehaviour
{
    GateController gateController;

    void Start()
    {
        gateController = transform.parent.GetComponent<GateController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 && GameController.Instance.objectsToDestroyNumber == 0)
        {
            Debug.Log("Open GATE");
            gateController.canOpenGate = true;
        }
    }
}
