using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    //[SerializeField] GameObject CompleteHuman;

    public bool IsTake = false;
    public int MoveNum = 0, TargetNum;
    [SerializeField]
    TextMeshProUGUI move, target, level;

    public GameObject animal, alien;
    Vector3 animalStartPos, alienStartPos;
    List<GameObject> animals = new List<GameObject>();
    List<Vector3> animalStartPoss= new List<Vector3>();
    int catchedAnimals = 0;
    [SerializeField]
    int StageListNum;
    //List<GameObject> StageList = new List<GameObject>();
    [SerializeField]
    List<int> TargetMoveList = new List<int>();
    [SerializeField]
    GameObject characterController;

    [SerializeField]
    GameObject ResultUI;
   
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);


    }
    GameObject nowStage;

    private void Start()
    {
        for (int i = 0; i < StageListNum; i++)
        {
            if (GameManager.instance.stageNum == i + 1)
            {
                nowStage = transform.GetChild(i).gameObject;
                nowStage.SetActive(true);
                Debug.Log(nowStage.name + "활성화됨!");
                alien = nowStage.transform.GetChild(0).gameObject;
                animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                Debug.Log(animal.name + "선택!");
                Debug.Log(alien.name + "선택!");
                level.text = "Stage " + (i + 1).ToString();
                target.text = "Target <b>" + TargetMoveList[i].ToString() + "</b>";


                for(int j=0;j< TargetMoveList[i];j++)
                {
                    animals.Add(nowStage.transform.GetChild(1 + j).gameObject.transform.GetChild(0).gameObject);
                    animalStartPoss.Add(animals[j].transform.position);
                }
                    
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);

        }
        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
        for (int j = 0; j < TargetMoveList[GameManager.instance.stageNum-1]; j++)
        {
            characterController.GetComponent<CharecterController>().animals.Add(animals[j].GetComponent<AnimalMoveReNew>());
        }

            // characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
            //animalStartPos = animal.transform.position;
            alienStartPos = alien.transform.position;
    }


    public void IncreaseMove()
    {
        MoveNum++;
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";
        // move.text = MoveNum.ToString();
    }
    public void DecreaseMove()
    {
        MoveNum--;
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";
    }

    public void ResetMove()
    {
        catchedAnimals = 0;
        MoveNum = 0;
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";
       // animal.transform.position = animalStartPos;
        alien.transform.position = alienStartPos;
       // animal.SetActive(true);
        IsTake = false;
        alien.GetComponent<AlienMoveReNew>().taking = false;
       
        for (int i=0;i< TargetMoveList[GameManager.instance.stageNum - 1];i++)
        {
            animals[i].transform.localScale = new Vector3(1, 1, 1);
            animals[i].transform.position = animalStartPoss[i];
            animals[i].SetActive(true);
            animals[i].GetComponent<AnimalMoveReNew>().IsSliding = false;
        }
        CharecterController.instance.newlogics = 0;
        //   animal.GetComponent<AnimalMoveReNew>().StartCoroutine_Auto;
    }

    public void StageFinish()
    {
        catchedAnimals++;
        if (catchedAnimals == TargetMoveList[GameManager.instance.stageNum - 1])
        {
            ResultUI.SetActive(true);
        }
   
    }
    public void RetryStage()
    {
        ResultUI.SetActive(false);
        ResetMove();
    }
    public void MainStage()
    {
        ResultUI.SetActive(false);
        GameManager.instance.SceneChange(0);
    }
    public void NextLevel()
    {
       

            catchedAnimals = 0;
            nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
            ResultUI.SetActive(false);
            nowStage.SetActive(false);
            GameManager.instance.stageNum++;
            nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
            nowStage.SetActive(true);

            alien = nowStage.transform.GetChild(0).gameObject;
            animals.Clear();
            animalStartPoss.Clear();
            for (int i = 0; i < TargetMoveList[GameManager.instance.stageNum - 1]; i++)
            {
                animals.Add(nowStage.transform.GetChild(1 + i).gameObject.transform.GetChild(0).gameObject);
                animalStartPoss.Add(animals[i].transform.position);
                animals[i].GetComponent<AnimalMoveReNew>().IsSliding = false;
            }



            // animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

            MoveNum = 0;
            TargetNum = TargetMoveList[GameManager.instance.stageNum - 1];

            level.text = "Stage " + GameManager.instance.stageNum.ToString();
            target.text = "Target <b>" + TargetMoveList[GameManager.instance.stageNum - 1].ToString() + "</b>";
            move.text = "Move <b>" + MoveNum.ToString() + "</b>";

            characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
            characterController.GetComponent<CharecterController>().animals.Clear();
            for (int j = 0; j < TargetMoveList[GameManager.instance.stageNum - 1]; j++)
            {
                characterController.GetComponent<CharecterController>().animals.Add(animals[j].GetComponent<AnimalMoveReNew>());
            }
            //  characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
            // animalStartPos = animal.transform.position;
            alienStartPos = alien.transform.position;

            IsTake = false;
            alien.GetComponent<AlienMoveReNew>().taking = false;
        // animal.GetComponent<AnimalMoveReNew>().IsSliding = false;
        CharecterController.instance.newlogics = 0;
    }



}
