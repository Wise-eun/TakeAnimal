using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;

    int catchedSmallNum = 0;
    bool isTake = false;

    private bool isPushed = false;
    public bool IsTake { get => isTake; set => isTake = value; }
    public bool IsChapter2 { get => isChapter2; }
    public bool IsChapter3 { get => isChapter3; }
    public bool IsPushed { get => isPushed; set => isPushed = value; }
    public enum layer
    {
        animal = 6,
        smallAnimal,
        alien,
        prision,
        barricade
    }


    [SerializeField]
    bool isChapter2 = false;
    [SerializeField]
    bool isChapter3 = false;
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

    [SerializeField]
    List<int> stageButtonNum = new List<int>();
   

    GameObject nowStage;
    int stageNum;
    int targetNum;
    int starNum = 0;
    GameObject animal, alien, alienCh;
    List<GameObject> smallAnimals = new List<GameObject>();
    List<Vector3> smallAnimalsPos = new List<Vector3>();
    Vector3 animalStartPos, alienStartPos, alienchStartPos;
    List<ButtonController> stageButtons = new List<ButtonController>();

    void Awake()
    {
        Init();
    }
    private void Start()
    {
        SettingStage();
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
    void SettingStage()
    {
        stageNum = GameManager.instance.StageNum - 1;
        targetNum = targetAnimalList[stageNum];
        nowStage = transform.GetChild(stageNum).gameObject;
        nowStage.SetActive(true);

        for (int i = 0; i < stageListNum; i++)
        {
            if (stageNum != i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (isChapter2)
        {
            SettingChapter2();
        }
        SettingCharacter();
        SettingSmallAnimals();
        SettingStageUI();
    }
    void SettingCharacter()
    {
        alien = nowStage.transform.GetChild(0).gameObject;
        animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        alienCh = nowStage.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;

        characterController.GetComponent<CharecterController>().UFO = alien.GetComponent<UFOMove>();
        characterController.GetComponent<CharecterController>().Alien = alienCh.GetComponent<AlienMove>();
        characterController.GetComponent<CharecterController>().Animal = animal.GetComponent<AnimalMove>();

        animalStartPos = animal.transform.position;
        alienStartPos = alien.transform.position;
        alienchStartPos = alienCh.transform.position;
    }
    void SettingSmallAnimals()
    {
        smallAnimals.Clear();
        smallAnimalsPos.Clear();
        CharecterController.instance.ClearSmallAnimals();
        for (int j = 0; j < targetAnimalList[stageNum]; j++)
        {
            smallAnimals.Add(nowStage.transform.GetChild(1).gameObject.transform.GetChild(j + 1).gameObject);
            smallAnimalsPos.Add(smallAnimals[j].transform.position);
            smallAnimals[j].tag = "small";
        }
    }
    void SettingStageUI()
    {
        level.text = "STAGE\n " + GameManager.instance.StageNum.ToString();
        target.text = "Target <b>" + targetNum.ToString() + "</b>";

    }
    void SettingChapter2()
    {
        stageButtons.Clear();
        for (int j = 0; j < stageButtonNum[stageNum]; j++)
        {
            Debug.Log("StagebuttonsNum[i] = " + stageButtonNum[stageNum]);
            stageButtons.Add(nowStage.transform.GetChild(3).transform.GetChild(j).transform.GetChild(1).gameObject.GetComponent<ButtonController>());
            stageButtons[j].TurnLight(ButtonController.lightColor.red);
        }
        isPushed = false;
    }


    void ResetCharacter()
    {
        animal.transform.position = animalStartPos;
        alien.transform.position = alienStartPos;
        alienCh.transform.position = alienchStartPos;

        animal.transform.localScale = new Vector3(1, 1, 1);
        alienCh.transform.localScale = new Vector3(1, 1, 1);
        alienCh.SetActive(true);
        animal.SetActive(true);
    }
    void ResetSmallAnimals()
    {
        catchedSmallNum = 0;

        for (int i = 0; i < smallAnimals.Count; i++)
        {
            smallAnimals[i].gameObject.layer = (int)layer.prision;
            smallAnimals[i].transform.position = smallAnimalsPos[i];
            smallAnimals[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            smallAnimals[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            smallAnimals[i].SetActive(true);
        }
        CharecterController.instance.ClearSmallAnimals();
    }
    void ResetButtons()
    {
        for (int j = 0; j < stageButtonNum[stageNum]; j++)
        {
            stageButtons[j] = nowStage.transform.GetChild(3).transform.GetChild(j).transform.GetChild(1).gameObject.GetComponent<ButtonController>();
            stageButtons[j].TurnLight(ButtonController.lightColor.red);
        }
        isTake = false;
    }
    private void Reset()
    {
        isTake = false;
        ResetCharacter();
        ResetSmallAnimals();
        if (isChapter2)
        {
            ResetButtons();
        }
    }


    public void StageSucceed()
    {
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
        SetActiveUI(false);
        Reset();
    }
    public void MainStage()
    {
        SetActiveUI(false);
        GameManager.instance.SceneChange(0);
    }
    public void NextStage()
    {
        Reset();
        SetActiveUI(false);
        nowStage.SetActive(false);
        GameManager.instance.StageNum++;
        SettingStage();

    }


    public void IncreaseCatchedAnimals()
    {
        if (catchedSmallNum < targetNum)
            catchedSmallNum++;
    }
    public void DecreaseCatchedAnimals()
    {
        if (catchedSmallNum > 0)
            catchedSmallNum--;
    }
    public void ButtonTurnRed(bool isRed)
    {
       
        if (isRed)
        {
            stageButtons[0].TurnBarricade(ButtonController.lightColor.red);
            for (int i = 0; i < stageButtons.Count; i++)
                stageButtons[i].TurnLight(ButtonController.lightColor.red);           
        }
        else
        {
            stageButtons[0].TurnBarricade(ButtonController.lightColor.green);
            for (int i = 0; i < stageButtons.Count; i++)
                stageButtons[i].TurnLight(ButtonController.lightColor.green);
        }

    }
    void SetActiveUI(bool active)
    {
        resultSucceed.SetActive(active);
        resultFail.SetActive(active);
    }

}
