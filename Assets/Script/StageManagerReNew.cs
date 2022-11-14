using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManagerReNew : MonoBehaviour
{
    public static StageManagerReNew instance = null;
    //[SerializeField] GameObject CompleteHuman;

    public bool IsTake = false;
    public int MoveNum = 0, TargetNum;
    [SerializeField]
    TextMeshProUGUI move, target, level;

    public GameObject animal, alien;
    Vector3 animalStartPos, alienStartPos;

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
                Debug.Log(nowStage.name + "Ȱ��ȭ��!");
                alien = nowStage.transform.GetChild(0).gameObject;
                animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                Debug.Log(animal.name + "����!");
                Debug.Log(alien.name + "����!");
                level.text = "Stage " + (i + 1).ToString();
                target.text = "Target <b>" + TargetMoveList[i].ToString() + "</b>";
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);

        }
        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
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

        MoveNum = 0;
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";
        animal.transform.position = animalStartPos;
        alien.transform.position = alienStartPos;
        animal.SetActive(true);
        IsTake = false;
        alien.GetComponent<AlienMoveReNew>().taking = false;
        animal.GetComponent<AnimalMoveReNew>().IsSliding = false;
        CharecterController.instance.newlogics = 0;
        //   animal.GetComponent<AnimalMoveReNew>().StartCoroutine_Auto;
    }

    public void StageFinish()
    {
        ResultUI.SetActive(true);
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
        nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
        ResultUI.SetActive(false);
        nowStage.SetActive(false);
        GameManager.instance.stageNum++;
        nowStage = transform.GetChild(GameManager.instance.stageNum - 1).gameObject;
        nowStage.SetActive(true);
        alien = nowStage.transform.GetChild(0).gameObject;
        animal = nowStage.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

        MoveNum = 0;
        TargetNum = TargetMoveList[GameManager.instance.stageNum - 1];

        level.text = "Stage " + GameManager.instance.stageNum.ToString();
        target.text = "Target <b>" + TargetMoveList[GameManager.instance.stageNum - 1].ToString() + "</b>";
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";

        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
        animalStartPos = animal.transform.position;
        alienStartPos = alien.transform.position;

        IsTake = false;
        alien.GetComponent<AlienMoveReNew>().taking = false;
        animal.GetComponent<AnimalMoveReNew>().IsSliding = false;
        CharecterController.instance.newlogics = 0;
    }



}
