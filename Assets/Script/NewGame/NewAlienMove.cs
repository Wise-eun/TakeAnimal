using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NewAlienMove : MonoBehaviour
{

    [SerializeField]
    float max_x, max_z;
    [SerializeField]
    float min_x, min_z;

    bool taking = true;
    void Update()
    {
        if (taking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                //     StartCoroutine(MoveToPosition(dir.left));
                if (min_x < transform.position.x - 1.2f)
                    transform.DOMoveX(transform.position.x - 1.2f, 0.1f);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (max_x > transform.position.x + 1.2f)
                    //  StartCoroutine(MoveToPosition(dir.right));
                    transform.DOMoveX(transform.position.x + 1.2f, 0.1f);

            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (max_z > transform.position.z + 1.2f)
                    transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (min_z < transform.position.z - 1.2f)
                    transform.DOMoveZ(transform.position.z - 1.2f, 0.1f);


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Animal"))
        {
            taking = false;
            other.GetComponent<NewAnimalMove>().StopAllCoroutines();
            StartCoroutine(Take(other.transform));
        }
    }

    IEnumerator Take(Transform animal)
    {
        animal.DOMoveY(2f, 1f);
        yield return new WaitForSeconds(1f);
        animal.gameObject.SetActive(false);
       yield return new WaitForSeconds(0.3f);
        animal.gameObject.SetActive(true);
        animal.gameObject.GetComponent<NewAnimalMove>().ReVIVER();
        taking = true;
    }


}
