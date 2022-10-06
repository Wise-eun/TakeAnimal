using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //[SerializeField] GameObject CompleteHuman;
    [SerializeField] GameObject Exit;
    [SerializeField] GameObject FakeWall;


    void Awake()
    {
        //CompleteHuman.SetActive(false);
        Exit.SetActive(false);
       // FakeWall.SetActive(true);


        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);



        DontDestroyOnLoad(gameObject);
    }
    

    public void Meet(Vector3 pos)
    {
        //CompleteHuman.transform.position = pos;
        //CompleteHuman.SetActive(true);
        Exit.SetActive(true);
        FakeWall.SetActive(false);
    }
    
}
