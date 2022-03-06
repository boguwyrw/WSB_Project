using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedGateController : MonoBehaviour
{
    GateController gateController;

    void Start()
    {
        gateController = transform.parent.GetComponent<GateController>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            gateController.canOpenGate = false;
            gateController.canClosedGate = true;
        }
    }
}
