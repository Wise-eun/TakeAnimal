using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLightController : MonoBehaviour
{
    // Start is called before the first frame update


  [SerializeField]  GameObject Ground_Soul;
    [SerializeField] bool alienMode= false;

    [SerializeField] GameObject dog;
    [SerializeField] GameObject dog_default;
    [SerializeField] GameObject alien;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Human_Light"))
        {

            if(alienMode)
            {
                dog = other.gameObject.transform.parent.gameObject;
                StartCoroutine(animalUp());
               
            }
            else
            {
                Ground_Soul.SetActive(false);
                this.gameObject.SetActive(false);
               
            }
            other.gameObject.SetActive(false);
        }
    }

    float timeToMove = 20f;
    IEnumerator animalUp()
    {
        var t = 0f;

        dog.GetComponent<AnimalMove>().StopAllCoroutines();

        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
         
            dog.transform.position = Vector3.Lerp(dog.transform.position, alien.transform.position, t);

            if((alien.transform.position -   dog.transform.position).sqrMagnitude <= 4)
            {
                break;
            }    
            yield return null;
        }
        //  dog_default.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dog.SetActive(false);
       // this.gameObject.SetActive(false);

        // dog.transform.position = alien.transform.position;
    }
    
}
