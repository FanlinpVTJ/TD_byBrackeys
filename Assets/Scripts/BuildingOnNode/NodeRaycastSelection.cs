using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRaycastSelection : MonoBehaviour
{
    public bool hovered {get; private set;}

    void Start()
    {
        StartCoroutine(RayCastSelection());
    }

    private IEnumerator RayCastSelection()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                var _selectedNode = hit.collider.gameObject.GetComponent<NodeSelectionBuild>();
                if (hit.collider.tag == "Node")
                {
                    
                    yield return null;
                }
                else
                {
                    
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
