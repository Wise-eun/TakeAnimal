using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AlienMove : MonoBehaviour
{
    private float moveSpeed = 0.2f;
    private float height;


    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    ParticleSystem AnimalBroken;

    Vector3 forward, back, left, right;
    Vector3 alienDirect;
    Vector3 notPosition;
    Vector3 nextPosition;
    RaycastHit hit;
    bool isBrokenPlayer = false;
    GameObject brokenAnimal;

    int animalLayerMask = 1 << 6 | 1 << 7;

    private void Start()
    {
        SettingRayDirect();
    }

    void SettingRayDirect()
    {
        forward = new Vector3(0, 0, 0.1f);
        back = new Vector3(0, 0, -0.1f);
        right = new Vector3(0.1f, 0, 0);
        left = new Vector3(-0.1f, 0, 0);
    }
    public void RightMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            groundCheck.transform.position = new Vector3(transform.position.x + 1.1f, groundCheck.position.y, transform.position.z);
            MoveToPosition(CharecterController.dir.right);
        }
    }
    public void LeftMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            groundCheck.transform.position = new Vector3(transform.position.x - 1.1f, groundCheck.position.y, transform.position.z);
            MoveToPosition(CharecterController.dir.left);
        }
    }

    public void UpMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z + 1.1f);
            MoveToPosition(CharecterController.dir.forward);
        }
    }

    public void DownMove()
    {
        if (!(StageManager.instance.IsTake))
        {
            groundCheck.transform.position = new Vector3(transform.position.x, groundCheck.position.y, transform.position.z - 1.1f);
            MoveToPosition(CharecterController.dir.back);
        }
    }


    public void MoveToPosition(CharecterController.dir direction)
    {
        isBrokenPlayer = false;
        height = 0;
        notPosition = new Vector3(transform.position.x, height, transform.position.z);
        transform.DORotate(new Vector3(0, (float)direction, 0), 0.1f);
        if (GroundCheck())
        {
            switch (direction)
            {
                case CharecterController.dir.forward:
                    alienDirect = forward;
                    nextPosition = new Vector3(transform.position.x, height, transform.position.z + 1.1f);
                    break;
                case CharecterController.dir.back:
                    alienDirect = back;
                    nextPosition = new Vector3(transform.position.x, height, transform.position.z - 1.1f);
                    break;
                case CharecterController.dir.right:
                    alienDirect = right;
                    nextPosition = new Vector3(transform.position.x + 1.1f, height, transform.position.z);
                    break;
                case CharecterController.dir.left:
                    alienDirect = left;
                    nextPosition = new Vector3(transform.position.x - 1.1f, height, transform.position.z);
                    break;
            }
            transform.DOJump(nextPosition, 1f, 1, moveSpeed);
            if (AnimalCheck(alienDirect))
            {
                BrokenAnimal(brokenAnimal);
            }
            return;
        }
        NotMove();
    }

    private void NotMove()
    {
        transform.DOJump(notPosition, 1f, 1, moveSpeed);
    }

    public bool GroundCheck()
    {
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {
            if (!hit.collider.CompareTag("highGround"))
                return true;
        }
        return false;
    }
    bool AnimalCheck(Vector3 rayDirection)
    {

        Debug.DrawRay(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, Color.red, 1f);

        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, out hit, 1f, animalLayerMask))
        {
            if (hit.transform.gameObject.layer.Equals((int)StageManager.layer.animal))
            {
                isBrokenPlayer = true;
            }
            brokenAnimal = hit.collider.gameObject;
            return true;
        }
        return false;
    }
    void BrokenAnimal(GameObject animal)
    {
        if (animal.Equals(null))
            return;

        animal.SetActive(false);
        AnimalBroken.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        AnimalBroken.gameObject.SetActive(true);
        AnimalBroken.Play();
        if (isBrokenPlayer) //플레이어가 잡힌 경우
        {
            WaitAndFinish();
        }
        else //구출해낸 동물이 잡힌경우
        {
            CharecterController.instance.RemoveSmallAnimal(animal.transform.GetComponent<SmallAnimalMove>());
            StageManager.instance.DecreaseCatchedAnimals();
        }


    }
    void WaitAndFinish()
    {
        StageManager.instance.StageFail();
    }
}
