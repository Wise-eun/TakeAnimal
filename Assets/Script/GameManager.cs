using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //[SerializeField] GameObject CompleteHuman;

    public bool IsTake = false;

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
    
    
}
