using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    //[SerializeField] GameObject CompleteHuman;

    public bool IsTake = false;
    public int MoveNum=0, TargetNum;
    [SerializeField]
    TextMeshProUGUI move, target, level;

    public GameObject animal, alien;
    Vector3 animalStartPos, alienStartPos;

    [SerializeField]
    List<GameObject> StageList = new List<GameObject>();
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


    private void Start()
    {
        for(int i=0;i< StageList.Count;i++)
        {
            if (GameManager.instance.stageNum == i+1)
            {
                StageList[i].SetActive(true);
                Debug.Log(StageList[i].name + "활성화됨!");
                alien = StageList[i].transform.GetChild(0).gameObject;
                animal = StageList[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                Debug.Log(animal.name + "선택!");
                Debug.Log(alien.name + "선택!");
                level.text = "Stage " + (i + 1).ToString();
                target.text = "Target <b>" + TargetMoveList[i].ToString() + "</b>";
            }
            else
            StageList[i].SetActive(false);

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
        ResultUI.SetActive(false);
        StageList[GameManager.instance.stageNum-1].SetActive(false);
        GameManager.instance.stageNum++;
        StageList[GameManager.instance.stageNum -1].SetActive(true);
        alien = StageList[GameManager.instance.stageNum - 1].transform.GetChild(0).gameObject;
        animal = StageList[GameManager.instance.stageNum - 1].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

        MoveNum = 0;
        TargetNum = TargetMoveList[GameManager.instance.stageNum - 1];

        level.text = "Stage " + GameManager.instance.stageNum.ToString();
        target.text = "Target <b>" + TargetMoveList[GameManager.instance.stageNum-1].ToString() + "</b>";
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";

        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
        animalStartPos = animal.transform.position;
        alienStartPos = alien.transform.position;

        IsTake = false;
        alien.GetComponent<AlienMoveReNew>().taking = false;
        animal.GetComponent<AnimalMoveReNew>().IsSliding = false;
    }



}
