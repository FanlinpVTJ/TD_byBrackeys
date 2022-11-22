using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] GameObject nodeUI;

    private NodeSelectionBuild selectedNode;

    public void SetTarget(NodeSelectionBuild _node)
    {
        selectedNode = _node;

        transform.position = selectedNode.transform.position;
    }

    public void Hide()
    {
        nodeUI.SetActive(false);
    }
}
