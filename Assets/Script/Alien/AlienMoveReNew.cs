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

    bool taking = true;

    public void LeftMove()
    {
        if (taking) 
        {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.2f, groundCheck.position.y, transform.position.z);
            if (GroundCheck())
                transform.DOMoveX(transform.position.x - 1.2f, 0.1f);
        }       
    }
    public void RightMove()
    {
        if (taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
            if (GroundCheck())
                transform.DOMoveX(transform.position.x + 1.2f, 0.1f);
        }
    }
    public void UpMove()
    {
        if (taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
            if (GroundCheck())
                transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);
        }
    }
    public void DownMove()
    {
        if (taking)
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
            if (GroundCheck())
                transform.DOMoveZ(transform.position.z - 1.2f, 0.1f);
        }
    }

    void Update()
    {
        if (taking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) )
            {

                //     StartCoroutine(MoveToPosition(dir.left));
                groundCheck.transform.position = new Vector3(transform.position.x - 1.2f, groundCheck.position.y , transform.position.z);
                if(GroundCheck())
                //if (min_x < transform.position.x - 1.2f)
                    transform.DOMoveX(transform.position.x - 1.2f, 0.1f);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
                if (GroundCheck())
                    //if (max_x > transform.position.x + 1.2f)
                    //  StartCoroutine(MoveToPosition(dir.right));
                    transform.DOMoveX(transform.position.x + 1.2f, 0.1f);

            }

            else if (Input.GetKeyDown(KeyCode.UpArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x , groundCheck.position.y, transform.position.z + 1.2f);
                if (GroundCheck())
                    // if (max_z > transform.position.z + 1.2f)
                    transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) )
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
                if (GroundCheck())
                    //if (min_z < transform.position.z - 1.2f)
                    transform.DOMoveZ(transform.position.z - 1.2f, 0.1f);


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            taking = false;
            GameManager.instance.IsTake = true;
            other.GetComponent<AnimalMoveReNew>().StopAllCoroutines();
            StartCoroutine(Take(other.transform));
        }
    }

    IEnumerator Take(Transform animal)
    {
      
        animal.DOMoveY(3f, 1f);
        yield return new WaitForSeconds(1f);
        animal.gameObject.SetActive(false);
        //yield return new WaitForSeconds(0.3f);
       // animal.gameObject.SetActive(true);
       // animal.gameObject.GetComponent<NewAnimalMove>().ReVIVER();
        taking = true;
    }

    RaycastHit hit;
    public bool GroundCheck()
    {
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {

            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("highGround"))
            {
                return true;
            }
      
        }
       return false;
    }


}
