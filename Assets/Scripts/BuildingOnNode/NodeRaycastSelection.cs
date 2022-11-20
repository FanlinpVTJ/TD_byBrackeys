using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: шото не совсем понятно, что оно делает по названию
// NodeSelectionInput?
// по коду я если честно тоже не понял, что оно делает
// как будто если мышкой навелись ждем кадры, а если не навелись - ждем секунды
// и вообще ничего не делаем
// а нафига оно вообще надо?
public class NodeRaycastSelection : MonoBehaviour
{
    // TODO: публичное PascalCase - Hovered (с большой буквы)
    // bool обычно называют вопросом, начинающимся с is, иногда has / was
    // на вопрос должно быть можно ответить только да/нет
    // То же самое относится к методам, если у тебя будет метод, который возвращает bool
    // IsHovered
    public bool hovered {get; private set;} // во, тут же есть private set

    // TODO: private забыл
    void Start()
    {
        StartCoroutine(RayCastSelection());
    }

    private IEnumerator RayCastSelection()
    {
        // TODO: Camera.main лучше закешировать в переменную
        // потому что каждый раз, когда ты это вызываешь, юнити делает
        // GameObject.FindWithTag("MainCamera");
        
        // var camera = Camera.main;
        
        while (true)
        {
            // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // TODO: не очень понял, что тут происходит))
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                // TODO: локальные переменные без _ - selectedNode
                var _selectedNode = hit.collider.gameObject.GetComponent<NodeSelectionBuild>();
                if (hit.collider.tag == "Node") // TODO: лучше заменить на hit.collider.CompareTag("Node"), работает быстрее
                {
                    
                    yield return null;
                }
                else
                {
                    // TODO: это шо?))
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
