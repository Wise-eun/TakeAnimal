using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    bool MainStage = false;
    [SerializeField]
    GameObject _selectedSmallStage;
    public void SpinStage()
    {
        transform.DORotate(new Vector3(-48.9f, 180, 0), 2).From().SetEase(curve);

    }

    Material material;
    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = new Color(1, 1, 1, 0);
     
        if (MainStage)
        {
       

          //  transform.DORotate(new Vector3(-34.8f, 35.76f, 0), 2).SetEase(curve);
            material.DOFade(1, "_BaseColor", 1);
        }
     


    }

    public void ShowStage()
    {

        //transform.DORotate(new Vector3(-34.8f, 35.76f, 0), 2).SetEase(curve);
        material.DOFade(1, "_BaseColor", 1);
        if (!MainStage)
            _selectedSmallStage.SetActive(true);
    }

    public void HideStage()
    {
    
       // transform.DORotate(new Vector3(-34.8f, -144.24f, 0), 2).SetEase(curve);
        material.DOFade(0, "_BaseColor", 1);
        if (!MainStage)
            _selectedSmallStage.SetActive(false);


    }
}
