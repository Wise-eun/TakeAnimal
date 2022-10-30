using System.Collections;

using UnityEngine;
using DG.Tweening;

public class NewAnimalMove : MonoBehaviour
{
    bool Isfloor = true;


    [SerializeField]
    float max_x, max_z;
    [SerializeField]
    float min_x, min_z;

    float height;

    [SerializeField]
    Transform MovingGround_height;

    public enum dir
    {
        left,
        right,
        forward,
        back
    }

    Sequence loopMove;

    private void Start()
    {

        StartCoroutine(Move());

    }

    dir MoveDirect;
    IEnumerator Move()
    {
        for(int i=0;i<999;i++)
        {
            MoveDirect = (dir)Random.RandomRange(0, 4);
            if (MoveDirect == dir.back && transform.position.z - 1.2f < min_z)
                continue;
            if (MoveDirect == dir.forward && transform.position.z + 1.2f > max_z)
                continue;
            if (MoveDirect == dir.left && transform.position.x - 1.2f < min_x)
                continue;
            if (MoveDirect == dir.right && transform.position.x + 1.2f >max_x)
                continue;

            StartCoroutine(MoveToPosition(MoveDirect));
            yield return new WaitForSeconds(1f);

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
    public void ReVIVER()
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(1f, 0.3f);
        transform.position = new Vector3(Random.RandomRange(min_x, max_x), 0f, Random.RandomRange(min_z, max_z));
        StartCoroutine(Move());
    }



    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
              
            if (!GameManager.instance.IsTake)
                StartCoroutine(MoveToPosition((dir)Random.RandomRange(0, 4)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!GameManager.instance.IsTake)
                StartCoroutine(MoveToPosition((dir)Random.RandomRange(0, 4)));

        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!GameManager.instance.IsTake)
                StartCoroutine(MoveToPosition((dir)Random.RandomRange(0, 4)));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!GameManager.instance.IsTake)
                StartCoroutine(MoveToPosition((dir)Random.RandomRange(0, 4)));
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


                if (hit.collider.CompareTag("ground"))
                {
                    if (transform.position.y > 1.8)
                        return false;
                    return true;
                }
                if (hit.collider.CompareTag("movingGround"))
                {
                    if (transform.position.y > 1.8)
                        return false;
                    if (MovingGround_height.position.y <= 1.5)
                        return true;
                }

                return false;
                //else
                //   return false;
            }
            return false;
        }
    */

}



