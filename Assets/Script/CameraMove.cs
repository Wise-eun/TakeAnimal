
using DG.Tweening;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
  public void CameraMoveTo(int num)
    {
        if( num ==1)
            transform.DOMoveX(1f, 1f);
        else if(num ==2)
            transform.DOMoveX(151.5f, 1f);
        else if (num == 3)
            transform.DOMoveX(280.7f, 1f);
        else if (num == 4)
            transform.DOMoveX(410.88f, 1f);
    }

    [ContextMenu("CameraMoveTo_tutorial")]
    public void CameraMoveTo_1Stage()
    {
            transform.position = new Vector3(-5.760601f, 6.390142f, -10.08309f);
       // transform.rotation = new Quaternion(-5.760601f, 6.390142f, -10.08309f);

    }

    [ContextMenu("CameraMoveTo_stage")]
    public void CameraMoveTo_stage()
    {
        transform.position = new Vector3(-8.827455f, 12.88622f, -11.35392f);
    }
}
