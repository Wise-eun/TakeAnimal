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

    void ResetMove()
    {
        MoveNum = 0;
        move.text = "Move <b>" + MoveNum.ToString() + "</b>";
        animal.transform.position = animalStartPos;
        alien.transform.position = alienStartPos;
    }

}
