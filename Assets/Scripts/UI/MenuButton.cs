using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public event Action OnMenu;

    public void ShowMenu()
    {
        OnMenu?.Invoke();
    }
}
