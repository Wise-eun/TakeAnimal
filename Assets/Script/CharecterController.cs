using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    public static CharecterController instance = null;
    [SerializeField]
    int smallAnimalNum = 5;

    public enum dir
    {  
        forward = -90,
        back = 90,
        right = 0,
        left = 180  
    }

    List<Vector3> smallsPos = new List<Vector3>();
    List<float> smallsRotation = new List<float>();
    List<SmallAnimalMove> smalls = new List<SmallAnimalMove>();
    private UFOMove ufo;
    private AnimalMove animal;
    private AlienMove alien;

    bool isMoving = false;

    public UFOMove UFO { set => ufo = value; }
    public AnimalMove Animal { set => animal = value; }
    public AlienMove Alien { set => alien = value; }
    public void ClearSmallAnimals()
    {
        smalls.Clear();
    }
    public void RemoveSmallAnimal(SmallAnimalMove smallAnimal)
    {
        smalls.Remove(smallAnimal);
    }
    public void AddSmallAnimal(SmallAnimalMove smallAnimal)
    {
        smalls.Add(smallAnimal);
    }

    void Awake()
    {
        Init();
        SettingSmallAnimal();
    }
    private void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private void SettingSmallAnimal()
    {
        for (int i = 0; i < smallAnimalNum; i++)
        {
            smallsPos.Add(new Vector3(0, 0, 0));
            smallsRotation.Add(0);
        }
    }
    public void SaveAnimalPos(float rotation)
    {
        for(int j=4;j>0;j--)
        {
            smallsPos[j] = smallsPos[j-1];
            smallsRotation[j] = smallsRotation[j-1];
        }
        smallsPos[0] = new Vector3(animal.transform.position.x, animal.transform.position.y+0.4f, animal.transform.position.z);
        smallsRotation[0] = rotation;
    }
    public IEnumerator Move(dir direct)
    {
        if (!isMoving && !StageManager.instance.IsTake)
        {
            isMoving = true;
            SoundManager.instance.PlayCharecterSound(SoundManager.charecterSound.move);
            switch (direct)
            {
                case dir.forward:
                    ufo.DownMove();
                    animal.UpMove();
                    if (StageManager.instance.IsChapter3)
                    {
                            alien.DownMove();
                    }
                    break;
                case dir.back:
                    ufo.UpMove();
                    animal.DownMove();
                    if (StageManager.instance.IsChapter3)
                    {
                            alien.UpMove();
                    }
                    break;
                case dir.right:
                    ufo.LeftMove();
                    animal.RightMove();
                    if (StageManager.instance.IsChapter3)
                    {
                            alien.LeftMove();
                    }
                    break;
                case dir.left:
                    ufo.RightMove();
                    animal.LeftMove();
                    if (StageManager.instance.IsChapter3)
                    {
                            alien.RightMove();
                    }
                    break;
            }
            for (int i = 0; i < smalls.Count; i++)
                smalls[i].Move(smallsPos[i], smallsRotation[i]);
            yield return new WaitForSeconds(0.2f);
            isMoving = false;
        }
    }

    public void MoveUp()
    {
        StartCoroutine(Move(dir.forward));
    }
    public void MoveDown()
    {
        StartCoroutine(Move(dir.back));
    }
    public void MoveRight()
    {
        StartCoroutine(Move(dir.right));
    }
    public void MoveLeft()
    {
        StartCoroutine(Move(dir.left));

    }

}
