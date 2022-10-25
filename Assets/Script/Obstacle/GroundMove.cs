using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GroundMove : MonoBehaviour
{
    [SerializeField]
    float target_height;
    [SerializeField]
    float origin_height = 0f;
    [SerializeField]
    float move_time = 3f;

    Sequence loopMove;

    private void Start()
    {
        loopMove = DOTween.Sequence()
            .Append(transform.DOMoveY(0, 2f))
            .Append(transform.DOMoveY(target_height, move_time))

            .SetLoops(100, LoopType.Yoyo);
    }
}
