using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    //[SerializeField] GameObject CompleteHuman;

    public bool IsTake = false;
    public int MoveNum = 0, TargetNum;
    [SerializeField]
    TextMeshProUGUI move, target, level;

    public GameObject animal, alien;
    public List<GameObject> smallAnimals = new List<GameObject>();
    public List<Vector3> smallAnimalsPos = new List<Vector3>();
    Vector3 animalStartPos, alienStartPos;
    //List<GameObject> animals = new List<GameObject>();
   // List<Vector3> animalStartPoss= new List<Vector3>();
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
    [SerializeField]
    GameObject ResultFail;

    public int catchedSmallNum = 0;
    public int targetSmallNum = 0;
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
        catchedSmallNum = 0;
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
                TargetNum = TargetMoveList[i];
                for (int j = 0; j < TargetMoveList[i]; j++)
                {
                    smallAnimals.Add(nowStage.transform.GetChild(1).gameObject.transform.GetChild(j + 1).gameObject);
                    smallAnimalsPos.Add(smallAnimals[j].transform.position);
                    smallAnimals[j].tag = "small";
                }
                    
               
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);

        }
        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();

            characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();


            // characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
           animalStartPos = animal.transform.position;
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
        catchedSmallNum = 0;
        catchedAnimals = 0;
        MoveNum = 0;
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";
        animal.transform.position = animalStartPos;
        alien.transform.position = alienStartPos;

        animal.transform.localScale = new Vector3(1, 1, 1);

        animal.SetActive(true);
        IsTake = false;
        alien.GetComponent<AlienMoveReNew>().taking = false;
        for (int i = 0; i < smallAnimals.Count; i++)
        {
            smallAnimals[i].transform.position = smallAnimalsPos[i];
            smallAnimals[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            smallAnimals[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            smallAnimals[i].SetActive(true);
            smallAnimals[i].tag = "small";
            smallAnimals[i].GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);

        }
        CharecterController.instance.smalls.Clear();



        CharecterController.instance.newlogics = 0;
        //   animal.GetComponent<AnimalMoveReNew>().StartCoroutine_Auto;
    }

    public void StageFinish()
    {
        //catchedAnimals++;
       if(!IsTake)
        ResultUI.SetActive(true);

   
    }

    public void StageFail()
    {
        ResultFail.SetActive(true);
    }

    public void RetryStage()
    {
        ResultUI.SetActive(false);
        ResultFail.SetActive(false);
        ResetMove();
    }
    public void MainStage()
    {
        ResultUI.SetActive(false);
        ResultFail.SetActive(false);
        GameManager.instance.SceneChange(0);
    }
    public void NextLevel()
    {

        catchedSmallNum = 0;
        catchedAnimals = 0;
            nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
            ResultUI.SetActive(false);
            nowStage.SetActive(false);
            GameManager.instance.stageNum++;
            nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
            nowStage.SetActive(true);

            alien = nowStage.transform.GetChild(0).gameObject;
        smallAnimals.Clear();
        smallAnimalsPos.Clear();

        CharecterController.instance.smalls.Clear();


        animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

            MoveNum = 0;
            TargetNum = TargetMoveList[GameManager.instance.stageNum - 1];

        level.text = "Stage " + GameManager.instance.stageNum.ToString();
            target.text = "Target <b>" + TargetMoveList[GameManager.instance.stageNum - 1].ToString() + "</b>";
            move.text = "Move <b>" + MoveNum.ToString() + "</b>";

            characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
        //  characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
         animalStartPos = animal.transform.position;
        alienStartPos = alien.transform.position;
        for (int j = 0; j < TargetMoveList[GameManager.instance.stageNum - 1]; j++)
        {
            smallAnimals.Add(nowStage.transform.GetChild(1).gameObject.transform.GetChild(j + 1).gameObject);
            smallAnimalsPos.Add(smallAnimals[j].transform.position);
            smallAnimals[j].GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            smallAnimals[j].tag = "small";
        }
        IsTake = false;
            alien.GetComponent<AlienMoveReNew>().taking = false;
        // animal.GetComponent<AnimalMoveReNew>().IsSliding = false;
        CharecterController.instance.newlogics = 0;
    }



}
