using UnityEngine;
using DG.Tweening;
using System.Collections;



public class SmallAnimalMove : MonoBehaviour
{
    /*
    public void Move(Vector3 next_position, float rotate)
    {
        transform.DORotate(new Vector3(0, rotate, 0), 0.1f);
        transform.DOJump(next_position, 1f, 1, 0.2f);
    }*/
    
    public void Move(Vector3 next_position, float rotate)
    {
        transform.DORotate(new Vector3(0, rotate, 0), 0.1f);
        transform.DOJump(next_position, 1f, 1, 0.2f);
    }
}
