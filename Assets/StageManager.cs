using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    //[SerializeField] GameObject CompleteHuman;


    public int MoveNum=0, TargetNum;
    [SerializeField]
    TextMeshProUGUI move, target;

    public GameObject animal, alien;
    Vector3 animalStartPos, alienStartPos;

    [SerializeField]
    List<GameObject> StageList = new List<GameObject>();

    [SerializeField]
     GameObject characterController;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
     //   else if (instance != this)
          //  Destroy(gameObject);

       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for(int i=0;i< StageList.Count;i++)
        {
            if (StageList[i].activeSelf)
            {
                Debug.Log(StageList[i].name + "활성화됨!");
                alien = StageList[i].transform.GetChild(0).gameObject;
                animal = StageList[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                Debug.Log(animal.name + "선택!");
                Debug.Log(alien.name + "선택!");
            }
         

        }
        characterController.GetComponent<CharecterController>().alien = alien.GetComponent<AlienMoveReNew>();
        characterController.GetComponent<CharecterController>().animal = animal.GetComponent<AnimalMoveReNew>();
        animalStartPos = animal.transform.position;
        alienStartPos = alien.transform.position;
    }
    public void SetTarget(int num)
    {
        TargetNum = num;
        target.text = TargetNum.ToString();
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
    }

}
