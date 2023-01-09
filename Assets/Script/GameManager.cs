using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //[SerializeField] GameObject CompleteHuman;

 
    public int stageNum = 1;


    void Awake()
    {
        MakeInstance();
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetStageNum(int num)
    {
       stageNum = num;
    }

    public void SceneChange(int chapter) 
    {
        //else 가 길어질거같으면 case로 퉁침
        //Main, Chapter 두씬만 관리하는걸 목표로.
        if (chapter.Equals(0))
        {
            SceneManager.LoadScene("Main");
        }
        else if (chapter == 1)
        {
            SceneManager.LoadScene("TakeAnimal_1");
        }
        else if (chapter == 2)
        {
            SceneManager.LoadScene("TakeAnimal_2");
        }
        else if (chapter == 3)
        {
            SceneManager.LoadScene("TakeAnimal_3");
        }
        else if (chapter == 4)
        {
            SceneManager.LoadScene("TakeAnimal_4");
        }

    }
    
}
