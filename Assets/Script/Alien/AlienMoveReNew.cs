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
        StartCoroutine(CheckLight(0.01f));
    }

    public void LeftMove()
    {
     
        if (!taking) 
        {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
            animalCheck.transform.position = new Vector3(transform.position.x - 1.1f, animalCheck.position.y, transform.position.z);
            if (light)
            {
                if (GroundCheck())
                {
                    StartCoroutine( CheckLight(0.01f));
                    transform.DOMoveX(transform.position.x - 1.1f, 0.1f);
                }
            }        
            else
            {
                if (GroundCheck())
                {
                   
                    transform.DOMoveX(transform.position.x - 1.1f, 0.1f);
                    StartCoroutine(CheckLight(0.1f));
                }
            }
        }
 
      
    }
    public void RightMove()
    {
   
        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
            animalCheck.transform.position = new Vector3(transform.position.x + 1.1f, animalCheck.position.y, transform.position.z);
            if (light)
            {
                if (GroundCheck())
                {
                    StartCoroutine(CheckLight(0.01f));
                    transform.DOMoveX(transform.position.x + 1.1f, 0.1f);
                }
            }
            else
            {
                if (GroundCheck())
                {
              
                    transform.DOMoveX(transform.position.x + 1.1f, 0.1f);
                    StartCoroutine(CheckLight(0.1f));
                }
            }
        }


    }
    public void UpMove()
    {
   

        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
            animalCheck.transform.position = new Vector3(transform.position.x, animalCheck.position.y, transform.position.z + 1.1f);
            if (light)
            {
                if (GroundCheck())
                {
                    StartCoroutine(CheckLight(0.01f));
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);
                }
            }
            else
            {
                if (GroundCheck())
                {
                    
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);
                    StartCoroutine(CheckLight(0.1f));
                }
            }
        
  
        }

    }
    public void DownMove()
    {
    
    
        if (!taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
            animalCheck.transform.position = new Vector3(transform.position.x, animalCheck.position.y, transform.position.z - 1.1f);
            if (light)
            {
                if (GroundCheck())
                {
                    StartCoroutine(CheckLight(0.01f));
                    transform.DOMoveZ(transform.position.z - 1.1f, 0.1f);
                }
            }
            else
            {
                if (GroundCheck())
                {
                
                    transform.DOMoveZ(transform.position.z - 1.1f, 0.1f);
                    StartCoroutine(CheckLight(0.1f));
                }
            }
        }
     
    }
/*
    void Update()
    {
        if (taking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y , transform.position.z);
                if(GroundCheck())

                    transform.DOMoveX(transform.position.x - 1.1f, 0.1f);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
                if (GroundCheck())

                    transform.DOMoveX(transform.position.x + 1.1f, 0.1f);

            }

            else if (Input.GetKeyDown(KeyCode.UpArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x , groundCheck.position.y, transform.position.z + 1.1f);
                if (GroundCheck())
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
                if (GroundCheck())
                    transform.DOMoveZ(transform.position.z - 1.1f, 0.1f);


            }
        }
    }
*/
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            // taking = false;
            taking = true;
            StageManager.instance.IsTake = true;
            other.GetComponent<AnimalMoveReNew>().StopAllCoroutines();
            StartCoroutine(Take(other.transform));
        }
    }

    IEnumerator Take(Transform animal)
    {
        animal.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        yield return new WaitForSeconds(0.2f);
    
        animal.transform.DOJump(animal.transform.position, 2f, 1, 1f);
        yield return new WaitForSeconds(0.25f);
        animal.DOMoveY(3.5f, 0.5f).SetEase(Ease.InCirc);
 
        yield return new WaitForSeconds(0.5f);
        

        animal.gameObject.SetActive(false);

        taking = false;
        StageManager.instance.IsTake = false;
        StageManager.instance.StageFinish();
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
                return false;
            }

        }
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {
     
          
            
            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("highGround") || hit.collider.CompareTag("waterGround") || hit.collider.CompareTag("slideGround"))
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
