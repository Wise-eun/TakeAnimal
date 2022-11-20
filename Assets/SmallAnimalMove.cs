using UnityEngine;
using DG.Tweening;


public class SmallAnimalMove : MonoBehaviour
{
    // Start is called before the first frame update
    public void Move(Vector3 next_position, float rotate)
    {
        transform.DORotate(new Vector3(0, rotate, 0), 0.1f);
        transform.DOJump(next_position, 1f, 1, 0.2f);
    }

}
