using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimalMoveReNew : MonoBehaviour
{
    bool Isfloor = true;



    float height;
    bool IsWater = false;
    bool InWater = false;

    
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform smallAnimalCheck;
    [SerializeField]
    bool IsChapter4 = false;
    public bool IsSliding = false; //현재 미끄러져서 이동하는중인지 확인
    public enum dir
    {
        left,
        right,
        forward,
        back
    }
   

    public void RightMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            if(IsChapter4)
            {
                if(!IsSliding)
                {
                   // groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
                   // GroundCheck();

                    StartCoroutine(Sliding(dir.right));
                }


                return;
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
               
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);
            
                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
            smallAnimalCheck.position = new Vector3(transform.position.x + 1.1f, smallAnimalCheck.position.y, transform.position.z );
            
            StartCoroutine(MoveToPosition(dir.right));
        }          
    }
    public void LeftMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            if (IsChapter4)
            {
                if (!IsSliding)
                {
                    //groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
                    //GroundCheck();
                    StartCoroutine(Sliding(dir.left));
                }

                return;    
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
                smallAnimalCheck.position = new Vector3(transform.position.x, smallAnimalCheck.position.y, transform.position.z + 1.1f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);
 
                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
            smallAnimalCheck.position = new Vector3(transform.position.x - 1.1f, smallAnimalCheck.position.y, transform.position.z);
            StartCoroutine(MoveToPosition(dir.left));
        }
    }
    IEnumerator Sliding(dir direct)
    {
        Vector3 groundDirect = transform.position;
        float move = 0f;
        if (direct.Equals(dir.right))
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            for (int i = 0; i < 100; i++)
            {
                move = transform.position.x + 1.1f;
                groundDirect = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
                groundCheck.transform.position = groundDirect;
                GroundCheck();
                if (!IsSliding)
                    break;

                if (IsSliding)
                {
                    transform.DOMoveX(move, 0.2f);
                    yield return new WaitForSeconds(0.15f);          
                }
            }                         
        }
        else if (direct.Equals(dir.left))
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.2f);
            for (int i = 0; i < 100; i++)
            {
                move = transform.position.x - 1.1f;
                groundDirect = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
                groundCheck.transform.position = groundDirect;
                GroundCheck();
                if (!IsSliding)
                    break;

                if (IsSliding)
                {
                    transform.DOMoveX(move, 0.2f);
                    yield return new WaitForSeconds(0.15f);                    
                }
            }             
        }
        else if (direct.Equals(dir.forward))
        {
            transform.DORotate(new Vector3(0, -90, 0), 0.2f);
            for (int i = 0; i < 100; i++)
            {
           
                move = transform.position.z + 1.1f;
                groundDirect = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
                groundCheck.transform.position = groundDirect;
                GroundCheck();
                if (!IsSliding)
                    break;

                if (IsSliding)
                {
                    transform.DOMoveZ(move, 0.2f);
                    yield return new WaitForSeconds(0.15f);
                   
                }
            }
        }
        else if (direct.Equals(dir.back))
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.2f);
            for (int i = 0; i < 100; i++)
            {
                move = transform.position.z - 1.1f;
                groundDirect = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
                groundCheck.transform.position = groundDirect;
                GroundCheck();
                if (!IsSliding)
                    break;

                if (IsSliding)
                {
                    transform.DOMoveZ(move, 0.2f);
                    yield return new WaitForSeconds(0.15f);           
                }
            }                
        }
       
     
    }
    public void UpMove()
    {
       
        if (!(StageManager.instance.IsTake))
        {
            if (IsChapter4)
            {
               // groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
                //GroundCheck();

                StartCoroutine(Sliding(dir.forward));
                return;
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
                smallAnimalCheck.position = new Vector3(transform.position.x, smallAnimalCheck.position.y, transform.position.z + 1.1f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);

                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
            smallAnimalCheck.position = new Vector3(transform.position.x, smallAnimalCheck.position.y, transform.position.z + 1.1f);
            StartCoroutine(MoveToPosition(dir.forward));
        }
    }

    public void DownMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            if (IsChapter4)
            {
                //groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
                //GroundCheck();

                StartCoroutine(Sliding(dir.back));
                return;
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.1f, 0.1f);

                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
            smallAnimalCheck.position = new Vector3(transform.position.x, smallAnimalCheck.position.y, transform.position.z - 1.1f);
            StartCoroutine(MoveToPosition(dir.back));
        }
    }
    Vector3 not_position;
    Vector3 jump_position;
    Vector3 move_position;
    Vector3 next_position;
    Quaternion rotation;
    float timeToMove = 0.07f;

    public IEnumerator ReMoveToPosition(Vector3 repos)
    {
      
        not_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
        move_position = new Vector3(repos.x, height + 1.7f, repos.z);
        next_position = new Vector3(repos.x, height, repos.z);


        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(transform.position, jump_position, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
            yield return null;
        }
        t = 0f;   
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(jump_position, move_position, t);
                yield return null;
            }

            t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(move_position, next_position, t);
                yield return null;
            }
        
    }

    public IEnumerator MoveToPosition(dir direction)
    {
        var t = 0f;
       


        height = 0;
            not_position = new Vector3(transform.position.x, height, transform.position.z);
            if (direction.Equals(dir.forward))
            {
                rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                transform.DORotate(new Vector3(0, -90, 0), 0.1f);
                next_position = new Vector3(transform.position.x, height, transform.position.z + 1.1f);

                if(GroundCheck())
                {
                CharecterController.instance.SaveAnimalPos(-90);
                transform.DOJump(next_position, 1f, 1, 0.2f);
                }
                else
                    transform.DOJump(not_position, 1f, 1, 0.2f);

            //yield return new WaitForSeconds(1f);

        }
        else if (direction.Equals(dir.back))
            {
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                transform.DORotate(new Vector3(0, 90, 0), 0.1f);
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z - 1.1f);
                next_position = new Vector3(transform.position.x, height, transform.position.z - 1.1f);

                if (GroundCheck())
                {
                CharecterController.instance.SaveAnimalPos(90);
                transform.DOJump(next_position, 1f, 1, 0.2f);
                }
                else
                    transform.DOJump(not_position, 1f, 1, 0.2f);

           // yield return new WaitForSeconds(1f);
        }
            else if (direction.Equals(dir.right))
            {
                rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.DORotate(new Vector3(0, 0, 0), 0.1f);
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x + 1.1f, height + 1.7f, transform.position.z);
                next_position = new Vector3(transform.position.x + 1.1f, height, transform.position.z);

                if (GroundCheck())
                {
                CharecterController.instance.SaveAnimalPos(0);
                transform.DOJump(next_position, 1f, 1, 0.2f);
                }
                else
                    transform.DOJump(not_position, 1f, 1, 0.2f);
                
            
            }
            else
            {
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.DORotate(new Vector3(0, 180, 0),0.1f);
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x - 1.1f, height + 1.7f, transform.position.z);
                next_position = new Vector3(transform.position.x - 1.1f, height, transform.position.z);

                if (GroundCheck())
                {
                CharecterController.instance.SaveAnimalPos(180);
                transform.DOJump(next_position, 1f, 1, 0.2f);
                }                 
                else
                    transform.DOJump(not_position, 1f, 1, 0.2f);

        }

        yield return new WaitForSeconds(1f);

    }

    RaycastHit hit;
    RaycastHit animalHit;
    public bool GroundCheck()
    {
        Debug.DrawRay(smallAnimalCheck.position, smallAnimalCheck.transform.up, Color.red, 1f);
        if (Physics.Raycast(smallAnimalCheck.position, smallAnimalCheck.transform.up, out animalHit))
        {

            Debug.Log(animalHit.collider.name);
            if (animalHit.collider.CompareTag("small"))
            {
                Debug.Log("동물이 바로앞에 있어요!");
                CharecterController.instance.smalls.Add(animalHit.collider.gameObject.GetComponent< SmallAnimalMove>());
                animalHit.collider.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.44f, 0.58f, 0.31f);
                animalHit.collider.gameObject.tag = "Mysmall";
                //return false;
            }

        }
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {         
            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("lightGround") || hit.collider.CompareTag("targetGround"))
            {
                IsWater = false;
                
                //InWater = false;
                return true;
            }
            if (hit.collider.CompareTag("waterGround") )
            {
                Debug.Log("물이당!");
                IsWater = true;
                return true;
            }
            if (hit.collider.CompareTag("slideGround"))
            {
                Debug.Log("빙판이당!");
                IsSliding = true;
                return true;
            }
            if (hit.collider.CompareTag("Alien"))
            {
                Debug.Log("외계인이당");
                return false;
            }

            }
        IsSliding = false;
        IsWater = false;
        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("targetGround"))
        {
            StageManager.instance.IsTake = false;
            StageManager.instance.StageFinish();
        }

    }

}
