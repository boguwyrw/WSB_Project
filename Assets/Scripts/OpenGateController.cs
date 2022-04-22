using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateController : MonoBehaviour
{
    GateController gateController;
    IEnumerator gateCoroutine;

    void Start()
    {
        gateController = transform.parent.GetComponent<GateController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (GameController.Instance.objectsToDestroyInStage_1_Number == 0)
            {
                gateController.canOpenGate = true;
            }
            else
            {
                gateCoroutine = ManagerUI.Instance.DelayHideInfoPanel();
                ManagerUI.Instance.ShowInfoPanel();
                StartCoroutine(gateCoroutine);
            }
        }
    }

    
}
