using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;

    private int catchedSmallNum = 0;
    private bool isTake = false;
    private bool isButtonStage = false;
    private bool isChapter3 = false;
    private bool isPushed = false;
    public bool IsTake{ get => isTake; set=> isTake = value; }
    public bool IsButtonStage { get=>isButtonStage;}
    public bool IsChapter3 { get=>isChapter3;}
    public bool IsPushed{ get => isPushed; set => isPushed = value; }




    [SerializeField]
    TextMeshProUGUI move, target, level;
    [SerializeField]
    int stageListNum;

    [SerializeField]
    List<int> targetAnimalList = new List<int>();
    [SerializeField]
    GameObject characterController;

    [SerializeField]
    GameObject resultSucceed;
    [SerializeField]
    GameObject resultFail;


    private int targetNum;
    private int starNum = 0;
    private GameObject animal, alien, alienCh;
    private List<GameObject> smallAnimals = new List<GameObject>();
    private List<Vector3> smallAnimalsPos = new List<Vector3>();
    private Vector3 animalStartPos, alienStartPos, alienchStartPos;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }


    GameObject nowStage;
    [SerializeField]
    List<int> StagebuttonsNum =new List<int>();
    List<ButtonController> Stagebuttons = new List<ButtonController>();
    private void Start()
    {
        for (int i = 0; i < stageListNum; i++)
        {
            if (GameManager.instance.stageNum == i + 1)
            {
                nowStage = transform.GetChild(i).gameObject;
                nowStage.SetActive(true);
                Debug.Log(nowStage.name + "활성화됨!");
                alien = nowStage.transform.GetChild(0).gameObject;
                animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                alienCh = nowStage.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
                Debug.Log(animal.name + "선택!");
                Debug.Log(alien.name + "선택!");
                targetNum = targetAnimalList[i];
                level.text = "STAGE\n" + (i + 1).ToString();
                target.text = "Target <b>" + targetNum.ToString() + "</b>";
                
                for (int j = 0; j < targetAnimalList[i]; j++)
                {
                    smallAnimals.Add(nowStage.transform.GetChild(1).gameObject.transform.GetChild(j + 1).gameObject);
                    smallAnimalsPos.Add(smallAnimals[j].transform.position);
                    smallAnimals[j].tag = "small";
                }
                if (isButtonStage)
                {
                    for(int j=0;j< StagebuttonsNum[i];j++)
                    {
                        Debug.Log("StagebuttonsNum[i] = " + StagebuttonsNum[i]);
                        Stagebuttons.Add( nowStage.transform.GetChild(3).transform.GetChild(j).transform.GetChild(1).gameObject.GetComponent<ButtonController>());
                        Stagebuttons[j].TurnRed();
                    }                   
                    isPushed = false;
                }
             
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);

        }
        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<UFOMove>();
        characterController.GetComponent<CharecterController>().alien_charecter = alienCh.GetComponent<AnimalMove>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMove>();


           animalStartPos = animal.transform.position;
            alienStartPos = alien.transform.position;
        alienchStartPos = alienCh.transform.position;
    }


    public void ResetMove()
    {
        catchedSmallNum = 0;
        animal.transform.position = animalStartPos;
        alien.transform.position = alienStartPos;
        alienCh.transform.position = alienchStartPos;
        if (isButtonStage)
        {
            for (int j = 0; j < StagebuttonsNum[GameManager.instance.stageNum - 1]; j++)
            {
                Stagebuttons[j] = nowStage.transform.GetChild(3).transform.GetChild(j).transform.GetChild(1).gameObject.GetComponent<ButtonController>();
                Stagebuttons[j].TurnRed();
            }
            isTake = false;
        }

        animal.transform.localScale = new Vector3(1, 1, 1);
        alienCh.transform.localScale = new Vector3(1, 1, 1);
        alienCh.SetActive(true);
        animal.SetActive(true);
        isTake = false;
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
    }

    public void StageFinish()
    {
       if(!isTake)
        resultSucceed.SetActive(true);


        if (targetNum == catchedSmallNum)
            starNum = 3;
        else if (targetNum / 2 < catchedSmallNum)
            starNum = 2;
        else
            starNum = 1;
        Debug.Log("찾은 애기동물 수 : " + catchedSmallNum);
        Debug.Log("별점 : " + starNum);


    }

    public void StageFail()
    {
        resultFail.SetActive(true);
    }

    public void RetryStage()
    {
        resultSucceed.SetActive(false);
        resultFail.SetActive(false);
        ResetMove();
    }
    public void MainStage()
    {
        resultSucceed.SetActive(false);
        resultFail.SetActive(false);
        GameManager.instance.SceneChange(0);
    }
    public void NextLevel()
    {

        catchedSmallNum = 0;
            nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
            resultSucceed.SetActive(false);
            nowStage.SetActive(false);
            GameManager.instance.stageNum++;
            nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
            nowStage.SetActive(true);

            alien = nowStage.transform.GetChild(0).gameObject;
        Stagebuttons.Clear();
        smallAnimals.Clear();
        smallAnimalsPos.Clear();
        if (isButtonStage)
        {
            for (int j = 0; j < StagebuttonsNum[GameManager.instance.stageNum - 1]; j++)
            {
                Stagebuttons.Add( nowStage.transform.GetChild(3).transform.GetChild(j).transform.GetChild(1).gameObject.GetComponent<ButtonController>());
                Stagebuttons[j].TurnRed();
            }
            isPushed = false;
        }
        CharecterController.instance.smalls.Clear();


        animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        alienCh = nowStage.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
            targetNum = targetAnimalList[GameManager.instance.stageNum - 1];

        level.text = "STAGE\n " + GameManager.instance.stageNum.ToString();
            target.text = "Target <b>" + targetNum.ToString() + "</b>";

            characterController.GetComponent<CharecterController>().alien = alien.GetComponent<UFOMove>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMove>();
        characterController.GetComponent<CharecterController>().alien_charecter = alienCh.GetComponent<AnimalMove>();
        animalStartPos = animal.transform.position;
        alienStartPos = alien.transform.position;
        for (int j = 0; j < targetAnimalList[GameManager.instance.stageNum - 1]; j++)
        {
            smallAnimals.Add(nowStage.transform.GetChild(1).gameObject.transform.GetChild(j + 1).gameObject);
            smallAnimalsPos.Add(smallAnimals[j].transform.position);
            smallAnimals[j].GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            smallAnimals[j].tag = "small";
        }
        isTake = false;
        CharecterController.instance.newlogics = 0;
    }

    public void IncreaseCatchedAnimals()
    {
        if(catchedSmallNum < targetNum)
            catchedSmallNum++;
    }
    public void DecreaseCatchedAnimals()
    {
        if (catchedSmallNum > 0 )
            catchedSmallNum--;
    }
    public void ButtonTurnRed(bool isRed)
    {
        if(!isRed)
        {
            for (int i = 0; i < Stagebuttons.Count; i++)
                Stagebuttons[i].TurnGreen();
        }
        else
        {
            for (int i = 0; i < Stagebuttons.Count; i++)
                Stagebuttons[i].TurnRed();
        }

    }

}
