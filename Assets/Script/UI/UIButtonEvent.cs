using UnityEngine;
using DG.Tweening;
public class UIButtonEvent : MonoBehaviour
{
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    GameObject _selectedStage;
    [SerializeField]
    GameObject _myStage;
    [SerializeField]
    UIAnimation UIAnimation;
    [SerializeField]
    GameObject _selectedSmallStage;


    private Material _myStageMaterial, _selectedStageMaterial;
    private void Start()
    {
       
    }
    public void PointerDown()
    {
        transform.DOScale(0.5f, 0.5f);
    }
    public void PointerUp()
    {
        transform.DOScale(1f, 1f);
    }


    Sequence showSelectStageSequence, hideSeletStageSequence;
    public void ShowSlectStage()
    {
      //  _myStage.SetActive(false);
      //  _selectedStage.SetActive(true);
        _selectedSmallStage.SetActive(true);
    }

    public void ShowMainStage()
    {
    //    _myStage.SetActive(true);
        Debug.Log("´­¸²");
    }

    public void HideButton()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowButton()
    {
        this.gameObject.SetActive(true);
    }
}
