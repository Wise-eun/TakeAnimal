using DG.Tweening;
using UnityEngine;
public class AnimalMove : MonoBehaviour
{


    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    ParticleSystem prisionBroken;

    private float moveSpeed = 0.2f;
    private float height = 0;
    bool isNotMove = false;

    Vector3 forward, back, left, right;
    Vector3 animalDirect;
    Vector3 notPosition;
    Vector3 nextPosition;
    RaycastHit hit;
    int alienLayerMask = 1 << (int)StageManager.layer.alien;
    int prisionLayerMask = 1 << (int)StageManager.layer.prision;
    int barricadeLayerMask = 1 << (int)StageManager.layer.barricade;



    private void Start()
    {
        SettingRay();
    }

    void SettingRay()
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
        transform.DORotate(new Vector3(0, (float)direction, 0), 0.1f);
        if (GroundCheck())
        {
            switch (direction)
            {
                case CharecterController.dir.forward:
                    animalDirect = forward;
                    nextPosition = new Vector3(transform.position.x, height, transform.position.z + 1.1f);
                    break;
                case CharecterController.dir.back:
                    animalDirect = back;
                    nextPosition = new Vector3(transform.position.x, height, transform.position.z - 1.1f);
                    break;
                case CharecterController.dir.right:
                    animalDirect = right;
                    nextPosition = new Vector3(transform.position.x + 1.1f, height, transform.position.z);
                    break;
                case CharecterController.dir.left:
                    animalDirect = left;
                    nextPosition = new Vector3(transform.position.x - 1.1f, height, transform.position.z);
                    break;
            }
            CheckFront();
            if (isNotMove)
            {
                NotMove();
            }
            else
            {
                CheckSmallAnimal(animalDirect);
                transform.DOJump(nextPosition, 1f, 1, moveSpeed);
                CharecterController.instance.SaveAnimalPos((float)direction);
            }
        }
        else
        {
            NotMove();
        }
    }

    private void NotMove()
    {
        notPosition = new Vector3(transform.position.x, height, transform.position.z);
        transform.DOJump(notPosition, 1f, 1, moveSpeed);
    }

    void CheckFront()
    {
        if (StageManager.instance.IsChapter2)
            isNotMove = CheckBarricade(animalDirect);
        if (StageManager.instance.IsChapter3)
            isNotMove = CheckAlien(animalDirect);
    }

    public bool GroundCheck()
    {
        if (Physics.Raycast(groundCheck.position, groundCheck.transform.up, out hit))
        {
            isNotMove = false;
            if (hit.collider.CompareTag("targetGround"))
            {
                StageManager.instance.StageSucceed();
                return true;
            }
            if (hit.collider.CompareTag("ground") || hit.collider.CompareTag("buttonGround"))
            {
                return true;
            }
        }
        return false;
    }

    bool CheckBarricade(Vector3 rayDirection)
    {
        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, out hit, 1f, barricadeLayerMask))
        {
            if (StageManager.instance.IsPushed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    bool CheckAlien(Vector3 rayDirection)
    {

        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, out hit, 1f, alienLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void CheckSmallAnimal(Vector3 rayDirection)
    {
        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), rayDirection, out hit, 1f, prisionLayerMask))
        {
            CharecterController.instance.AddSmallAnimal(hit.collider.gameObject.GetComponent<SmallAnimalMove>());
            hit.collider.gameObject.tag = "Mysmall";
            hit.collider.gameObject.layer = 7;
            hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(false);

            StageManager.instance.IncreaseCatchedAnimals();
            prisionBroken.gameObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            prisionBroken.gameObject.SetActive(true);
            SoundManager.instance.PlayCharecterSound(SoundManager.charecterSound.broken);
            prisionBroken.Play();
        }
    }


}
