using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimalMove : MonoBehaviour
{
    private float moveSpeed= 0.2f;
    private float height;


    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    ParticleSystem prisionBroken;
    [SerializeField]
    ParticleSystem AnimalBroken;


    Vector3 forward, back, left, right;
    Vector3 animalDirect;
    Vector3 not_position;
    Vector3 next_position;
    RaycastHit hit;



    public enum dir
    {
        left = 180 ,
        right = 0 ,
        forward = -90 ,
        back  = 90
    }

   
    private void Start()
    {
        forward = new Vector3(0,0, 0.3f);
        back = new Vector3(0, 0, -0.3f);
        right = new Vector3(0.3f, 0, 0);
        left = new Vector3(-0.3f, 0, 0);
    }
    public void RightMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
            MoveToPosition(dir.right);
        }          
    }
    public void LeftMove()
    {
        if (!(StageManager.instance.IsTake))
        {      
            groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
            MoveToPosition(dir.left);
        }
    }
    
    public void UpMove()
    {
       
        if (!(StageManager.instance.IsTake))
        {        
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
            MoveToPosition(dir.forward);
        }
    }

    public void DownMove()
    {
        if (!(StageManager.instance.IsTake))
        {          
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
            MoveToPosition(dir.back);
        }
    }


    public void MoveToPosition(dir direction)
    {
        height = 0;
        not_position = new Vector3(transform.position.x, height, transform.position.z);
        transform.DORotate(new Vector3(0, (float)direction, 0), 0.1f);
        if (GroundCheck())
        {
            switch (direction)
            {
                case dir.forward:
                    animalDirect = forward;
                    next_position = new Vector3(transform.position.x, height, transform.position.z + 1.1f);          
                    break;
                case dir.back:
                    animalDirect = back;
                    next_position = new Vector3(transform.position.x, height, transform.position.z - 1.1f);
                    break;
                case dir.right:
                    animalDirect = right;
                    next_position = new Vector3(transform.position.x + 1.1f, height, transform.position.z);
                    break;
                case dir.left:
                    animalDirect = left;
                    next_position = new Vector3(transform.position.x - 1.1f, height, transform.position.z);
                    break;
            }
            if (StageManager.instance.IsButtonStage)
            {
                if (BarricadeCheck(animalDirect) && !StageManager.instance.IsPushed)
                {
                    NotMove();
                    return;
                }
            }
            AnimalCheck(animalDirect);
            transform.DOJump(next_position, 1f, 1, moveSpeed);
            CharecterController.instance.SaveAnimalPos((float)direction);
        }
    }

     private void NotMove()
    {
        transform.DOJump(not_position, 1f, 1, moveSpeed);
    }

    public bool GroundCheck()
    {

        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {

            if (hit.collider.CompareTag("targetGround"))
            {
                StageManager.instance.StageFinish();
                return true;
            }
            if (hit.collider.CompareTag("ground")  || hit.collider.CompareTag("buttonGround"))
            {

                return true;

            }
        }
        NotMove();
        return false;
    }
     void AnimalCheck(Vector3 rayDirection)
    {

        Debug.DrawRay(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, Color.red,1f);

        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, out hit,1f))
        {
            if (hit.collider.CompareTag("small"))
            {
                Debug.Log("앞에 동물!");

                CharecterController.instance.smalls.Add(hit.collider.gameObject.GetComponent<SmallAnimalMove>());
                hit.collider.gameObject.tag = "Mysmall";
                hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(false);

                StageManager.instance.IncreaseCatchedAnimals();
                prisionBroken.gameObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                prisionBroken.gameObject.SetActive(true);
                CharecterController.instance.SoundList[1].Play();
                prisionBroken.Play();
            }
        }
    }

    bool BarricadeCheck(Vector3 rayDirection)
    {
        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, out hit, 1f))
        {
            if (hit.collider.CompareTag("Barricade"))
            {
                Debug.Log("앞에 바리게이트당!");
                return true;
            }
        }
            return false;
    }



}
