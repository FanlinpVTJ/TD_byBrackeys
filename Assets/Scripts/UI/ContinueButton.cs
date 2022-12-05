using System;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public event Action OnContinue;

    public void ContinueGame()
    {
        OnContinue?.Invoke();
    }
}
