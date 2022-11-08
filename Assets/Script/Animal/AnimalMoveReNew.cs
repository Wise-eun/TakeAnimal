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
                   // groundCheck.transform.position = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
                   // GroundCheck();

                    StartCoroutine(Sliding(dir.right));
                }


                return;
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);
            
                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
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
                    //groundCheck.transform.position = new Vector3(transform.position.x - 1.2f, groundCheck.position.y, transform.position.z);
                    //GroundCheck();
                    StartCoroutine(Sliding(dir.left));
                }

                return;    
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);
 
                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x - 1.2f, groundCheck.position.y, transform.position.z);
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
                move = transform.position.x + 1.2f;
                groundDirect = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
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
                move = transform.position.x - 1.2f;
                groundDirect = new Vector3(transform.position.x - 1.2f, groundCheck.position.y, transform.position.z);
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
           
                move = transform.position.z + 1.2f;
                groundDirect = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
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
                move = transform.position.z - 1.2f;
                groundDirect = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
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
               // groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
                //GroundCheck();

                StartCoroutine(Sliding(dir.forward));
                return;
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);

                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
            StartCoroutine(MoveToPosition(dir.forward));
        }
    }

    public void DownMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            if (IsChapter4)
            {
                //groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
                //GroundCheck();

                StartCoroutine(Sliding(dir.back));
                return;
            }
            if (InWater)
            {
                groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
                GroundCheck();
                if (IsWater) //다음 지역도 물이라면
                    transform.DOMoveZ(transform.position.z + 1.2f, 0.1f);

                return;
            }
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
            StartCoroutine(MoveToPosition(dir.back));
        }
    }/*
    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !(StageManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
            StartCoroutine(MoveToPosition(dir.right));

        }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !(StageManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.2f, groundCheck.position.y, transform.position.z);
            StartCoroutine(MoveToPosition(dir.left));
        }

            else if (Input.GetKeyDown(KeyCode.UpArrow) && !(StageManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
            StartCoroutine(MoveToPosition(dir.back));
        }

            if (Input.GetKeyDown(KeyCode.DownArrow) && !(StageManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
            StartCoroutine(MoveToPosition(dir.forward));


        }
        
    }
    */
    Vector3 not_position;
    Vector3 jump_position;
    Vector3 move_position;
    Vector3 next_position;
    Quaternion rotation;
    float timeToMove = 0.07f;

    public IEnumerator ReMoveToPosition(Vector3 repos)
    {
      
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
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z + 1.2f);
                next_position = new Vector3(transform.position.x, height, transform.position.z + 1.2f);
            }
            else if (direction.Equals(dir.back))
            {
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z - 1.2f);
                next_position = new Vector3(transform.position.x, height, transform.position.z - 1.2f);
            }
            else if (direction.Equals(dir.right))
            {
                rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x + 1.2f, height + 1.7f, transform.position.z);
                next_position = new Vector3(transform.position.x + 1.2f, height, transform.position.z);
            }
            else
            {
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
                move_position = new Vector3(transform.position.x - 1.2f, height + 1.7f, transform.position.z);
                next_position = new Vector3(transform.position.x - 1.2f, height, transform.position.z);
            }


            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(transform.position, jump_position, t);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
                yield return null;
            }


            t = 0f;
            if (GroundCheck())
            {
                if (!IsWater)
                {
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
                    transform.DOKill();
                }
                else
            {
                transform.GetChild(1).gameObject.SetActive(true);

                Vector3 water_position = new Vector3(next_position.x, height - 1, next_position.z);
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
                        transform.position = Vector3.Lerp(move_position, water_position, t);
                        yield return null;
                    }
                if (direction.Equals(dir.right))
                    transform.GetChild(0).transform.localPosition = new Vector3(0.5f, 1, 0);
                else if (direction.Equals(dir.left))
                    transform.GetChild(0).transform.localPosition = new Vector3(0.5f, 1, 0);
                else if (direction.Equals(dir.back))
                    transform.GetChild(0).transform.localPosition = new Vector3( 0.5f, 1, 0f);
                else if (direction.Equals(dir.forward))
                    transform.GetChild(0).transform.localPosition = new Vector3( 0.5f, 1, 0f);
                transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, 90f);

                    transform.DOMoveY(-0.7f, 1).SetLoops(-1, LoopType.Yoyo);
                    groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
                    InWater = true;


                }



            }
            else
            {
                timeToMove = 0.1f;
                while (t < 1)
                {
                    t += Time.deltaTime / timeToMove;
                    transform.position = Vector3.Lerp(transform.position, not_position, t);
                    yield return null;
                }
            }

      



    }

    RaycastHit hit;
    public bool GroundCheck()
    {
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {         
            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("lightGround"))
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

        }
        IsSliding = false;
        IsWater = false;
        return false;
    }




}
