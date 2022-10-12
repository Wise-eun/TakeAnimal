using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject target;

    RaycastHit hit;
    float maxDistance = 5f;
    bool Isfloor = false;
    
    [SerializeField]
    float height;
   public  enum dir
    {
        left,
        right,
        forward,
        back
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
                StartCoroutine(MoveToPosition(dir.right));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
   
                StartCoroutine(MoveToPosition(dir.left));

        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

                StartCoroutine(MoveToPosition(dir.back));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
  
                StartCoroutine(MoveToPosition(dir.forward));
        }
    }

    Vector3 not_position;
    Vector3 jump_position;
    Vector3 move_position;
    Vector3 next_position;
    Quaternion rotation;
    float timeToMove = 0.07f;
    public IEnumerator MoveToPosition(dir direction)
    {
        not_position = new Vector3(transform.position.x, height, transform.position.z);
        if (direction.Equals(dir.forward))
        {
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
            move_position = new Vector3(transform.position.x + 1, height+1.7f, transform.position.z);
            next_position = new Vector3(transform.position.x + 1, height, transform.position.z);
        }
        else if (direction.Equals(dir.back))
        {
            rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
            move_position = new Vector3(transform.position.x - 1, height + 1.7f, transform.position.z);
            next_position = new Vector3(transform.position.x - 1, height, transform.position.z);
        }
        else if (direction.Equals(dir.right))
        {
            rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
            move_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z - 1);
            next_position = new Vector3(transform.position.x, height, transform.position.z - 1);
        }
        else
        {
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            jump_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z);
            move_position = new Vector3(transform.position.x, height + 1.7f, transform.position.z + 1);
            next_position = new Vector3(transform.position.x, height, transform.position.z + 1);
        }

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(transform.position, jump_position, t);
            transform.rotation =  Quaternion.Lerp(transform.rotation, rotation, t);
            yield return null;
        }
        t = 0f;
        Isfloor = check();
        if (!Isfloor)
        {
            timeToMove = 0.1f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(transform.position, not_position, t);
                yield return null;
            }
        }
        else
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
        }

    }



    private bool check()
    {
        if (Physics.Raycast(target.transform.position, target.transform.up, out hit, maxDistance))
        {
           // Debug.DrawRay(target.transform.position, target.transform.up, Color.red,1f);
            //Debug.Log(this.gameObject.name+"가 충돌감지!");
            //hit.transform.GetComponent<MeshRenderer>().material.color = Color.black;

            if (hit.collider.CompareTag("Human_Light"))
            {
               // if (!IsAnimal)
               // {
                   
                    Debug.Log("사라졌어야함");


            }
                

                return true;
            //else
            //   return false;
        }
        return false;
    }
}



