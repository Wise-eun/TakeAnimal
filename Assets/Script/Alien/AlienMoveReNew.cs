using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AlienMoveReNew : MonoBehaviour
{

    [SerializeField]
    float max_x, max_z;
    [SerializeField]
    float min_x, min_z;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform animalCheck;
    public bool taking = false;
    bool light = true;
    GameObject alienLight;
    [SerializeField]
    AudioSource takeSound;
    private void Start()
    {
        alienLight = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
    }

    public void ReMove(Vector3 repos)
    {
        
        transform.DOMove(repos, 0.1f);
        groundCheck.transform.position = new Vector3(repos.x, groundCheck.position.y, repos.z);
        animalCheck.transform.position = new Vector3(repos.x, animalCheck.position.y, repos.z);
        GroundCheck();

    }

    public void LeftMove()
    {
        StartCoroutine(ColliderHideAndShow());

        if (!taking) 
        {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
            animalCheck.transform.position = new Vector3(transform.position.x - 1.1f, animalCheck.position.y, transform.position.z);
    
                if (GroundCheck())
                {
                    transform.DOMoveX(transform.position.x - 1.1f, 0.1f);
                }
    
 
        }
    }
    public void RightMove()
    {
        StartCoroutine(ColliderHideAndShow());
        
        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
            animalCheck.transform.position = new Vector3(transform.position.x + 1.1f, animalCheck.position.y, transform.position.z);

                if (GroundCheck())
                {
                    transform.DOMoveX(transform.position.x + 1.1f, 0.1f);
                }

         
        }
    }
    public void UpMove()
    {
        StartCoroutine(ColliderHideAndShow());
        
        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
            animalCheck.transform.position = new Vector3(transform.position.x, animalCheck.position.y, transform.position.z + 1.1f);
   
                if (GroundCheck())
            { 
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);
  
            }
        
  
        }
    }
    public void DownMove()
    {
        StartCoroutine(ColliderHideAndShow());
       
        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
            animalCheck.transform.position = new Vector3(transform.position.x, animalCheck.position.y, transform.position.z - 1.1f);

                if (GroundCheck())
                {
                    transform.DOMoveZ(transform.position.z - 1.1f, 0.1f);
                }        
        }
      
    }

    IEnumerator ColliderHideAndShow()
    {
        transform.GetComponent<BoxCollider>().center = new Vector3(-0.2f, 6f, -0.15f);
        yield  return new WaitForSeconds(0.1f);
        transform.GetComponent<BoxCollider>().center = new Vector3(-0.2f, 1.1f, -0.15f);
    }
    /*
    IEnumerator CheckLight(float time)
    {
        yield return new WaitForSeconds(time);
        if(light)
        {
            transform.GetComponent<BoxCollider>().center = new Vector3(-0.2f, 1.1f, -0.15f);
            alienLight.transform.localScale = new Vector3(1f, 1f, 1f);
        }          
        else
        {
            transform.GetComponent<BoxCollider>().center = new Vector3(-0.2f, 6f, -0.15f);
            alienLight.transform.localScale = new Vector3(1f, 1f, 0.08f);
        }
             
    }
    */
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Animal"))
        {
            StageManager.instance.IsTake = true;
            // taking = false;
            taking = true;
           // other.GetComponent<AnimalMoveReNew>().StopAllCoroutines();
            StartCoroutine(Take(other.transform, true));
        }
        else if (other.CompareTag("Mysmall")) //새끼동물이 잡혀간 경우
        {
            taking = true;
            StageManager.instance.IsTake = true;
            StartCoroutine(Take(other.transform, false));
            CharecterController.instance.smalls.Remove(other.gameObject.GetComponent<SmallAnimalMove>());
            StageManager.instance.catchedSmallNum--;
        }
    }

    IEnumerator Take(Transform animal, bool IsAnimal)
    {

        takeSound.Play();
        animal.transform.position = new Vector3(transform.position.x, 0, transform.position.z);


        // yield return new WaitForSeconds(0.2f);
        animal.DOMoveY(4f, 1f).SetEase(Ease.InCirc);
        StartCoroutine(Rotate(animal));
        yield return new WaitForSeconds(0.2f);
        // StartCoroutine(Rotate( animal));

        // animal.transform.DOJump(animal.transform.position, 2f, 1, 1f);
        //yield return new WaitForSeconds(0.5f); 


        animal.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1f);
        yield return new WaitForSeconds(0.8f);
        

        animal.gameObject.SetActive(false);

        taking = false;
     
       
        // StageManager.instance.StageFinish();
        if (IsAnimal)
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
            // animal.transform.DORotate(new Vector3(0, 0, 0), 0.3f, RotateMode.FastBeyond360)
            //.SetEase(Ease.Linear); yield return new WaitForSeconds(0.3f);
        }
    }
    RaycastHit hit;
    RaycastHit animalHit;
    public bool GroundCheck()
    {    
        Debug.DrawRay(animalCheck.position, animalCheck.transform.up, Color.red, 1f);
        if (Physics.Raycast(animalCheck.position, animalCheck.transform.up, out animalHit))
        {

            Debug.Log(animalHit.collider.name);
            if (animalHit.collider.CompareTag("Animal"))
            {
                Debug.Log("동물이 바로앞에 있어요!");
                return true;
            }
            if(animalHit.collider.CompareTag("Mysmall"))
            {
                Debug.Log("쪼꼬미가 바로앞에 있어요!");
                return true;
            }
        }
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {

            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("highGround") || hit.collider.CompareTag("waterGround") || hit.collider.CompareTag("targetGround") || hit.collider.CompareTag("buttonGround"))
            {
                light = true;
                return true;
            }
            if (hit.collider.CompareTag("lightGround") )
            {

                light = false;
                return true;
            }

        }
       return false;
    }


}
