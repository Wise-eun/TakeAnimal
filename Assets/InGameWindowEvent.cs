using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameWindowEvent : MonoBehaviour
{
    [SerializeField] GameObject PauseWindow;
    public void ShowPuaseWindow()
    {
        PauseWindow.SetActive(true);
    }
    public void HidePuaseWindow()
    {
        PauseWindow.SetActive(false);
    }
}
