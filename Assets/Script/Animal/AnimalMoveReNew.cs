using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimalMoveReNew : MonoBehaviour
{
    bool Isfloor = true;


    [SerializeField]
    float max_x, max_z;
    [SerializeField]
    float min_x, min_z;

    float height;

    [SerializeField]
    Transform MovingGround_height;
    [SerializeField]
    Transform groundCheck;
    public enum dir
    {
        left,
        right,
        forward,
        back
    }

    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !(GameManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.2f, groundCheck.position.y, transform.position.z);
            StartCoroutine(MoveToPosition(dir.right));

        }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !(GameManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.2f, groundCheck.position.y, transform.position.z);
            StartCoroutine(MoveToPosition(dir.left));
        }

            else if (Input.GetKeyDown(KeyCode.UpArrow) && !(GameManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.2f);
            StartCoroutine(MoveToPosition(dir.back));
        }

            if (Input.GetKeyDown(KeyCode.DownArrow) && !(GameManager.instance.IsTake))
            {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.2f);
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
        height = transform.position.y;
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

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(transform.position, jump_position, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
            yield return null;
        }
        t = 0f;
        if(GroundCheck())
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
            if (hit.collider.CompareTag("ground"))
            {
                return true;
            }

        }
        return false;
    }




}
