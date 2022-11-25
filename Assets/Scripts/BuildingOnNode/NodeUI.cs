using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] GameObject nodeUI;

    private TurretBuildInput selectedNode;

    public void SetTarget(TurretBuildInput _node)
    {
        selectedNode = _node;

        transform.position = selectedNode.transform.position;
    }

    public void Hide()
    {
        nodeUI.SetActive(false);
    }
}
