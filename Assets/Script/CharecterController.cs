using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    public static CharecterController instance = null;
    [SerializeField]
    int smallAnimalNum = 5;
    [SerializeField]
    List<AudioSource> soundList = new List<AudioSource>();

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
        smallsPos[4] = smallsPos[3];
        smallsRotation[4] = smallsRotation[3];

        smallsPos[3] = smallsPos[2];
        smallsRotation[3] = smallsRotation[2];

        smallsPos[2] = smallsPos[1];
        smallsRotation[2] = smallsRotation[1];

        smallsPos[1] = smallsPos[0];
        smallsRotation[1] = smallsRotation[0];

        smallsPos[0] = new Vector3(animal.transform.position.x, animal.transform.position.y+0.4f, animal.transform.position.z);
        smallsRotation[0] = rotation;
    }
    // Alien 움직이는데 0.5초
    // Animal 움직이는데 0.3초

    public void Move(dir direct)
    {
        SoundManager.instance.PlayCharecterSound(SoundManager.charecterSound.move);
        switch (direct)
        {
            case dir.forward:
                ufo.DownMove();
                animal.UpMove();
                if (StageManager.instance.IsChapter3)
                {
                    if (alien.isActiveAndEnabled)
                        alien.DownMove();
                }
                break;
            case dir.back:
                ufo.UpMove();
                animal.DownMove();
                if (StageManager.instance.IsChapter3)
                {
                    if (alien.isActiveAndEnabled)
                        alien.UpMove();
                }
                break;
            case dir.right:
                ufo.LeftMove();
                animal.RightMove();
                if (StageManager.instance.IsChapter3)
                {
                    if (alien.isActiveAndEnabled)
                        alien.LeftMove();
                }
                break;
            case dir.left:
                ufo.RightMove();
                animal.LeftMove();
                if (StageManager.instance.IsChapter3)
                {
                    if (alien.isActiveAndEnabled)
                        alien.RightMove();
                }
                break;
        }
        for (int i = 0; i < smalls.Count; i++)
            smalls[i].Move(smallsPos[i], smallsRotation[i]);
    }

    public void MoveUp()
    {
        Move(dir.forward);
    }
    public void MoveDown()
    {
        Move(dir.back);
    }
    public void MoveRight()
    {
        Move(dir.right);
    }
    public void MoveLeft()
    {
        Move(dir.left);

    }

}
