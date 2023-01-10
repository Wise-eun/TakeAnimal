using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Material[] buttonColor = new Material[2]; // 0:red , 1:green 
    [SerializeField]
    Material[] barricadeColor = new Material[2]; // 0:red , 1:green 
    [SerializeField]
    List<MeshRenderer> barricade = new List<MeshRenderer>();


    bool pushed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Animal") || other.CompareTag("Mysmall"))
        {
            Debug.Log("버튼에 동물 닿였어용");
            if(!StageManager.instance.IsPushed)
            {
                StageManager.instance.ButtonTurnRed(false);
                pushed = true;
             
            }             
            else
            {
                StageManager.instance.ButtonTurnRed(true);
                pushed = false;
            }
            StageManager.instance.IsPushed = pushed;
        }
    }

    public void TurnRed()
    {
        for (int i = 0; i < barricade.Count; i++)
            barricade[i].material = barricadeColor[0];

        GetComponent<MeshRenderer>().material = buttonColor[0];
        pushed = false;
    }
    public void TurnGreen()
    {
        for (int i = 0; i < barricade.Count; i++)
            barricade[i].material = barricadeColor[1];

        GetComponent<MeshRenderer>().material = buttonColor[1];
        pushed = false;
    }
}
