using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UFOMove : MonoBehaviour
{

    [SerializeField]
    Transform groundCheck;
    public bool taking = false;
    [SerializeField]
    AudioSource takeSound;


    public void ReMove(Vector3 repos)
    {
        
        transform.DOMove(repos, 0.1f);
        groundCheck.transform.position = new Vector3(repos.x, groundCheck.position.y, repos.z);
        GroundCheck();

    }
    IEnumerator Dungsil()
    {
        transform.DOMoveY(1.3f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DOMoveY(1.7f, 0.2f);
        AnimalCheck();
    }

    public void LeftMove()
    {
        if (!taking) 
        {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
            StartCoroutine(Dungsil());
            if (GroundCheck())
            {
               
                transform.DOMoveX(transform.position.x - 1.1f, 0.1f);

                }
    
 
        }
    }
    public void RightMove()
    {

        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
            StartCoroutine(Dungsil());
            if (GroundCheck())
                {
                    transform.DOMoveX(transform.position.x + 1.1f, 0.1f);

            }

         
        }
    }
    public void UpMove()
    {
        // StartCoroutine(ColliderHideAndShow());

        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
            StartCoroutine(Dungsil());
            if (GroundCheck())
            { 
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);

            }
        
  
        }
    }
    public void DownMove()
    { 
        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);          
            StartCoroutine(Dungsil());
            if (GroundCheck())
                {
                    transform.DOMoveZ(transform.position.z - 1.1f, 0.1f);
                
            }        
        }    
    }
    IEnumerator Take(Transform animal, bool isAnimal)
    {

        takeSound.Play();
        animal.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        animal.DOMoveY(4f, 1f).SetEase(Ease.InCirc);
        StartCoroutine(Rotate(animal));
        yield return new WaitForSeconds(0.2f);
        animal.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1f);
        yield return new WaitForSeconds(0.8f);
        

        animal.gameObject.SetActive(false);

        taking = false;
     
       

        if (isAnimal)
        {
            StageManager.instance.StageFail();
        }
        else
        {
            StageManager.instance.DecreaseCatchedAnimals();
        }
        StageManager.instance.IsTake = false;
    }

    IEnumerator Rotate(Transform animal)
    {
      
        for (int i = 0; i < 3; i++)
        {
            animal.transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.FastBeyond360)
             .SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.3f);
        }
    }
    RaycastHit hit;
    RaycastHit animalHit;
    public bool GroundCheck()
    {    
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {
                if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("highGround")|| hit.collider.CompareTag("targetGround") || hit.collider.CompareTag("buttonGround"))
            {
                return true;
            }
        }
       return false;
    }

    private void AnimalCheck()
    {
        //Debug.DrawRay(transform.position, transform.transform.up, Color.black, 1f);
        if (Physics.Raycast(transform.position, transform.up, out animalHit,10f))
        {

            Debug.Log(animalHit.collider.name);
            if(animalHit.collider.CompareTag("Animal") || animalHit.collider.CompareTag("Mysmall") || animalHit.collider.CompareTag("AlienCh"))
            {
                taking = true;
                StageManager.instance.IsTake = true;
                if (animalHit.collider.CompareTag("Animal"))
                {
                    StartCoroutine(Take(animalHit.transform, true));
                }
                else if (animalHit.collider.CompareTag("Mysmall"))
                {
                    StartCoroutine(Take(animalHit.transform, false));
                    CharecterController.instance.smalls.Remove(animalHit.transform.GetComponent<SmallAnimalMove>());
                }
                else if (animalHit.collider.CompareTag("AlienCh"))
                {
                    StartCoroutine(Take(animalHit.transform, false));
                }
            }
            
        }
    }


}
