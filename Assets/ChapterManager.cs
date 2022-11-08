using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }
    public void SetStageNum(int num)
    {
        GameManager.instance.stageNum = num;
    }

    public void SceneChange(int chapter)
    {
        GameManager.instance.SceneChange(chapter);
    }


    }
