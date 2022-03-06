using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer gateSkinned;
    [SerializeField] MeshCollider gateMesh;

    float openValue = 0.0f;
    float openStepsValue = 2.50f;

    bool unlockGate = false;

    [HideInInspector] public bool canOpenGate = false;
    [HideInInspector] public bool canClosedGate = false;


    void LateUpdate()
    {
        OpenGate();

        ClosedGate();

        gateSkinned.SetBlendShapeWeight(0, openValue);
    }

    void OpenGate()
    {
        if (canOpenGate && openValue < 100.0f)
        {
            openValue = openValue + openStepsValue;
        }

        if (!unlockGate && openValue > 75.0f)
        {
            gateMesh.isTrigger = true;
            unlockGate = true;
        }
    }

    void ClosedGate()
    {
        if (canClosedGate && openValue > 0.0f)
        {
            openValue = openValue - openStepsValue * 2.0f;
        }

        if (unlockGate && openValue < 75.0f)
        {
            gateMesh.isTrigger = false;
            unlockGate = false;
        }
    }
}
