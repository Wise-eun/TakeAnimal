
using UnityEngine;
using DG.Tweening;
public class CarMove : MonoBehaviour
{

    [SerializeField]
    float StartPoint;
    [SerializeField]
    float FinalPoint;
    [SerializeField]
    float MovingTime;

    Sequence loopMove;
    // Start is called before the first frame update
    void Start()
    {
        loopMove = DOTween.Sequence()
         .Append(transform.DOMoveZ(FinalPoint, MovingTime))
       // .Append(transform.DORotate(new Vector3(0,180,0),1))
         // .Append(transform.position.z(FinalPoint))
        // .Append(transform.DOMoveZ(StartPoint, 2f))
          //.Append(transform.DORotate(new Vector3(0, 0, 0), 1))
         .SetLoops(100, LoopType.Restart);
    }


}
