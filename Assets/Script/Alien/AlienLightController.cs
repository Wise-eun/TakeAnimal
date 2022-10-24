using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AlienLightController : MonoBehaviour
{
    private RaycastHit hit;
    GameObject animal;
    [SerializeField]
    GameObject UFO;
    [SerializeField]
    Transform UFOLight;
    public void CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            if (hit.collider.CompareTag("Animal"))
            {
                UFOLight.localScale = new Vector3(1f, 1f, 0.8f);
                GameManager.instance.IsTake = true;

                animal = hit.collider.gameObject;
                StartCoroutine(TakeAnimal());
            }
            else if (hit.collider.CompareTag("ground"))
            {
                UFOLight.localScale = new Vector3(1f, 1f, 0.8f);
            }
            else
            {
                UFOLight.localScale = new Vector3(1f, 1f, 0.3f);
            
            }

        }    
    }



    IEnumerator TakeAnimal()
    {
        animal.GetComponent<AnimalMove>().StopAllCoroutines();
        animal.transform.DOMoveY(transform.position.y - 1f,1.5f);
        yield return new WaitForSeconds(1.5f);
        animal.SetActive(false);
        GameManager.instance.IsTake = false;
    }


}
