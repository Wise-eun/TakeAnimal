using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //[SerializeField] GameObject CompleteHuman;

 
    public int stageNum = 1;


    void Awake()
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
        if (chapter == 0)
            SceneManager.LoadScene("Main");
        else if (chapter == 1)
            SceneManager.LoadScene("TakeAnimal_1");
        else if (chapter == 2)
            SceneManager.LoadScene("TakeAnimal_2");
        else if (chapter == 3)
            SceneManager.LoadScene("TakeAnimal_3");
        else if (chapter == 4)
            SceneManager.LoadScene("TakeAnimal_4");

    }
    
}
