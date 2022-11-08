using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChapterButtons = new List<GameObject>();

    [SerializeField]
    GameObject BackButton;


    

    public void HideButton()
    {
        BackButton.SetActive(true);
        for (int i = 0; i < ChapterButtons.Count; i++)
            ChapterButtons[i].SetActive(false);
    }
    public void ShowButton()
    {
        BackButton.SetActive(false);
        for (int i = 0; i < ChapterButtons.Count; i++)
            ChapterButtons[i].SetActive(true);
    }
}
