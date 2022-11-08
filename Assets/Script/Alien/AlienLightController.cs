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

    Vector3 cloud_height;
    Vector3 ground_height;
    private void Start()
    {
        cloud_height = new Vector3(1f, 1f, 0.3f);
        ground_height = new Vector3(1f, 1f, 0.8f);
    }
    public void CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
          //  Debug.Log("¥Í¿Œ π∞√º¥¬ ∏ª¿Ã¡“ : " + hit.transform.gameObject.name);


            if (hit.collider.CompareTag("cloud"))
            {
                Debug.Log("cloud ¥Í¿”!");
                UFOLight.localScale = cloud_height;
               // UFOLight.position.Scale(new Vector3(1f, 1f, 0.3f));
            }
            else 
            {
                UFOLight.localScale = ground_height;
               // UFOLight.position.Scale(new Vector3(1f, 1f, 0.8f));
                if (hit.collider.CompareTag("Animal"))
                {
                
                    StageManager.instance.IsTake = true;

                    animal = hit.collider.gameObject;
                    StartCoroutine(TakeAnimal());
                }
            }
        
      

        }    
    }



    IEnumerator TakeAnimal()
    {
        animal.GetComponentInParent<AnimalMove>().StopAllCoroutines();
        animal.transform.DOMoveY(transform.position.y - 1f,1.5f);
        yield return new WaitForSeconds(1.5f);
        animal.SetActive(false);
        StageManager.instance.IsTake = false;
    }


}
