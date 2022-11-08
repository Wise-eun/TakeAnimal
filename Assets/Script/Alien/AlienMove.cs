using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject target;

    RaycastHit hit;
    float maxDistance = 5f;
    bool Isfloor = false;
    [SerializeField]
    AlienLightController alienLightController;

    [SerializeField]
    float height;
    public enum dir
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
            if (!StageManager.instance.IsTake)
               // Debug.DrawRay(target.transform.position, new Vector3(transform.position.x +10, transform.position.y-10, transform.position.z), Color.black, 0.3f);
                StartCoroutine(MoveToPosition(dir.left));
            //Debug.DrawRay(target.transform.position, target.transform.up, Color.red, 1f);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!StageManager.instance.IsTake)
                StartCoroutine(MoveToPosition(dir.right));

            //Debug.DrawRay(target.transform.position, target.transform.up, Color.red, 1f);

        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!StageManager.instance.IsTake)
                StartCoroutine(MoveToPosition(dir.forward));

            // Debug.DrawRay(target.transform.position, target.transform.up, Color.red, 1f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!StageManager.instance.IsTake)
                StartCoroutine(MoveToPosition(dir.back));
 
            //  Debug.DrawRay(target.transform.position, target.transform.up, Color.red, 1f);
        }
    }

  
    Vector3 next_position;
    Quaternion rotation;
    float timeToMove = 0.07f;
    public IEnumerator MoveToPosition(dir direction)
    {
   
        if (direction.Equals(dir.forward))
        {
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            next_position = new Vector3(transform.position.x +1f, height, transform.position.z);
        }
        else if (direction.Equals(dir.back))
        {
            rotation = Quaternion.Euler(new Vector3(0, 270, 0));

            next_position = new Vector3(transform.position.x - 1f, height, transform.position.z);
        }
        else if (direction.Equals(dir.right))
        {
            rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            next_position = new Vector3(transform.position.x, height, transform.position.z - 1f);
        }
        else
        {
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            next_position = new Vector3(transform.position.x, height, transform.position.z + 1f);
        }

        var t = 0f;
        // timeToMove = 0.1f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
            yield return null;
        }
        t = 0f;
        Isfloor = check();
        if (Isfloor)
        {
        
            // Debug.Log("¹Ù´ÚÀÓ");
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(transform.position, next_position, t);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
                alienLightController.gameObject.transform.position = transform.position;
                yield return null;
            }     
        }

        alienLightController.CheckObject();
    }



    private bool check()
    {
        //Debug.Log("checkµé¾î¿À±äÇÔ?");

        if (Physics.Raycast(target.transform.position, target.transform.up, out hit, maxDistance))
        {
            // Debug.DrawRay(target.transform.position, target.transform.up, Color.red,1f);
            //Debug.Log(this.gameObject.name+"°¡ Ãæµ¹°¨Áö!");
            //  hit.transform.GetComponent<MeshRenderer>().material.color = Color.black;
            // Debug.Log("¹¹ ´êÀÓ");
      
            if (hit.collider.CompareTag("Human_Light"))
            {
                // if (!IsAnimal)
                // {
         
                Debug.Log("¹Ø¿¡ Ä£±¸¶û ´ê¾Ò½À´Ï´Ù!");
                // }


            }
            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("movingGround"))
            {
                // if (!IsAnimal)
                // {

               
                // }

                return true;
            }

            return false;

        }
       // Debug.Log("¹¹ ¾È´êÀÓ");
        return false;
    }
}
