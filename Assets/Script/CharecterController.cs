using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    public static CharecterController instance = null;
    public  UFOMove alien;  
   public  AnimalMove animal;
    public AnimalMove alien_charecter;

    Stack<Vector3> AnimalMoveOrder = new Stack<Vector3>();
    Stack<Vector3> AlienMoveOrder = new Stack<Vector3>();


    public List<SmallAnimalMove> smalls = new List<SmallAnimalMove>();
    public List<Vector3> smallsPos = new List<Vector3>();
    public List<float> smallsRotation = new List<float>();
    int Posnum = 5;

    public bool newLogic = false;
    public int newlogics = 0;
    public List<AnimalMove> animals = new List<AnimalMove>();

    
    public List<AudioSource> SoundList = new List<AudioSource>();
 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);

        for (int i = 0; i < Posnum; i++)
        {
            smallsPos.Add(new Vector3(0, 0, 0));
            smallsRotation.Add(0);
        }
            
        Posnum = 0;
    }

    public void NewLogicMove() //³²³²µ¿ºÏ
    {
        if(newlogics ==0)
        {
            for (int i = 0; i < animals.Count; i++)
                animals[i].DownMove();
            newlogics++;
        }
       else if (newlogics == 1)
        {
            for (int i = 0; i < animals.Count; i++)
                animals[i].DownMove();
            newlogics++;
        }
        else if (newlogics == 2)
        {
            for (int i = 0; i < animals.Count; i++)
                animals[i].RightMove();
            newlogics++;
        }
        else if (newlogics == 3)
        {
            for (int i = 0; i < animals.Count; i++)
                animals[i].UpMove();
            newlogics= 0;
        }

    }

    public void SaveAnimalPos(float rotation)
    {
        //if (Posnum > 2)
        //Posnum = 0;
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
    public void MoveUp()
    {
        SoundList[0].Play();
        if (!StageManager.instance.IsTake)
        {
            if(newLogic)
            {
                alien.UpMove();
                NewLogicMove();
                return;
            }


            StartCoroutine(AlienUpMove());
            animal.DownMove();
            if (StageManager.instance.IsChapter3)
            {
                if (alien_charecter.isActiveAndEnabled)
                    alien_charecter.UpMove();
            }
            for (int i = 0; i < smalls.Count; i++)
                StartCoroutine(smalls[i].Move(smallsPos[i], smallsRotation[i], (0.03f * (i + 3))));

        }
      

    }
    IEnumerator AlienUpMove()
    {
        yield return new WaitForSeconds(0.1f);
        alien.UpMove();
    }
    IEnumerator AlienDownMove()
    {
        yield return new WaitForSeconds(0.1f);
        alien.DownMove();
    }
    IEnumerator AlienRightMove()
    {
        yield return new WaitForSeconds(0.1f);
        alien.RightMove();
    }
    IEnumerator AlienLeftMove()
    {
        yield return new WaitForSeconds(0.1f);
        alien.LeftMove();
    }
    public void MoveDown()
    {
        SoundList[0].Play();
        if (!StageManager.instance.IsTake)
        {
            if (newLogic)
            {
                alien.DownMove();
                NewLogicMove();
                return;
            }

            StartCoroutine(AlienDownMove());
            animal.UpMove();
            if (StageManager.instance.IsChapter3)
            {
                if (alien_charecter.isActiveAndEnabled)
                    alien_charecter.DownMove();
            }
            for (int i = 0; i < smalls.Count; i++)
                StartCoroutine(smalls[i].Move(smallsPos[i], smallsRotation[i], (0.03f * (i + 3))));
            //smalls[i].Move(smallsPos[i], smallsRotation[i]);

        }
    }
    public void MoveRight()
    {
        SoundList[0].Play();
        if (!StageManager.instance.IsTake)
        {
            if (newLogic)
            {
                alien.RightMove();
                NewLogicMove();
                return;
            }


            StartCoroutine(AlienRightMove());
             animal.LeftMove();
            if (StageManager.instance.IsChapter3)
            {
                if(alien_charecter.isActiveAndEnabled)
                alien_charecter.RightMove();
            }
                
            for (int i = 0; i < smalls.Count; i++)
                StartCoroutine(smalls[i].Move(smallsPos[i], smallsRotation[i], (0.03f * (i + 3))));


        }
    }
    public void MoveLeft()
    {
        SoundList[0].Play();
        if (!StageManager.instance.IsTake)
        {
            if (newLogic)
            {
                alien.LeftMove();
                NewLogicMove();
                return;
            }


            StartCoroutine(AlienLeftMove());
             animal.RightMove();
            if (StageManager.instance.IsChapter3)
            {
                if (alien_charecter.isActiveAndEnabled)
                    alien_charecter.LeftMove();
            }
            for (int i = 0; i < smalls.Count; i++)
                StartCoroutine(smalls[i].Move(smallsPos[i], smallsRotation[i], (0.03f * (i+3))));
               // smalls[i].Move(smallsPos[i], smallsRotation[i]);
        }

    }
}
