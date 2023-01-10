using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int stageNum = 1;
    public int StageNum { get => stageNum; set=> stageNum = value; }

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

        DontDestroyOnLoad(gameObject);
    }

    public void SceneChange(int chapter)
    {
        //else �� ������Ű����� case�� ��ħ
        //Main, Chapter �ξ��� �����ϴ°� ��ǥ��.
        switch (chapter)
        {
            case 0:
                SceneManager.LoadScene("Main");
                break;
            case 1:
                SceneManager.LoadScene("TakeAnimal_1");
                break;
            case 2:
                SceneManager.LoadScene("TakeAnimal_2");
                break;
            case 3:
                SceneManager.LoadScene("TakeAnimal_3");
                break;
            case 4:
                SceneManager.LoadScene("TakeAnimal_4");
                break;
        }

    }

}
