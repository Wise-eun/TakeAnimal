using UnityEngine;
using DG.Tweening;
using System.Collections;



public class SmallAnimalMove : MonoBehaviour
{
    Vector3 originSize;
    Vector3 zeroSize;
    private void Start()
    {
        originSize = GetComponent<BoxCollider>().center;
        zeroSize = new Vector3(0, -4, 0);
    }
    // Start is called before the first frame update
    public void Move(Vector3 next_position, float rotate)
    {
        transform.DORotate(new Vector3(0, rotate, 0), 0.1f);
        transform.DOJump(next_position, 1f, 1, 0.2f);
    }

    public IEnumerator Move(Vector3 next_position, float rotate, float wait)
    {
       // GetComponent<BoxCollider>().center = zeroSize;
        yield return new WaitForSeconds(wait);
        transform.DORotate(new Vector3(0, rotate, 0), 0.1f);
        transform.DOJump(next_position, 1f, 1, 0.2f);
       // GetComponent<BoxCollider>().center = originSize;
    }
}
