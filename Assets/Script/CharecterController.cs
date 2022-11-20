using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    public static CharecterController instance = null;
    public  AlienMoveReNew alien;  
   public  AnimalMoveReNew animal;

    bool Re = false;
    Stack<Vector3> AnimalMoveOrder = new Stack<Vector3>();
    Stack<Vector3> AlienMoveOrder = new Stack<Vector3>();


    public List<SmallAnimalMove> smalls = new List<SmallAnimalMove>();
    public List<Vector3> smallsPos = new List<Vector3>();
    public List<float> smallsRotation = new List<float>();
    int Posnum = 3;

    public bool newLogic = false;
    public int newlogics = 0;
    public List<AnimalMoveReNew> animals = new List<AnimalMoveReNew>();
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

        smallsPos[2] = smallsPos[1];
        smallsRotation[2] = smallsRotation[1];

        smallsPos[1] = smallsPos[0];
        smallsRotation[1] = smallsRotation[0];

        smallsPos[0] = new Vector3(animal.transform.position.x, animal.transform.position.y+0.4f, animal.transform.position.z);
        smallsRotation[0] = rotation;
    }
    public void MoveUp()
    {

        if(!StageManager.instance.IsTake)
        {
            if(newLogic)
            {
                alien.UpMove();
                NewLogicMove();
                return;
            }

            StageManager.instance.IncreaseMove();
           // AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);

            alien.UpMove();     
             animal.DownMove();
            for (int i = 0; i < smalls.Count; i++)
                smalls[i].Move(smallsPos[i], smallsRotation[i]);
        }
      

    }
    public void MoveDown()
    {
        if (!StageManager.instance.IsTake)
        {
            if (newLogic)
            {
                alien.DownMove();
                NewLogicMove();
                return;
            }

            StageManager.instance.IncreaseMove();
           // AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);
            alien.DownMove();
            animal.UpMove();
            for (int i = 0; i < smalls.Count; i++)
                smalls[i].Move(smallsPos[i], smallsRotation[i]);

        }
    }
    public void MoveRight()
    {
        if (!StageManager.instance.IsTake)
        {
            if (newLogic)
            {
                alien.RightMove();
                NewLogicMove();
                return;
            }

            StageManager.instance.IncreaseMove();
           // AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);
            alien.RightMove();
             animal.LeftMove();
            for (int i = 0; i < smalls.Count; i++)
                smalls[i].Move(smallsPos[i], smallsRotation[i]);

        }
    }
    public void MoveLeft()
    {
        if (!StageManager.instance.IsTake)
        {
            if (newLogic)
            {
                alien.LeftMove();
                NewLogicMove();
                return;
            }

            StageManager.instance.IncreaseMove();
          //  AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);
            alien.LeftMove();
             animal.RightMove();
            for (int i = 0; i < smalls.Count; i++)
                smalls[i].Move(smallsPos[i], smallsRotation[i]);
        }

    }
    int num;
    public void MoveRe()
    {
        if (!StageManager.instance.IsTake)
        {
            if (StageManager.instance.MoveNum != 0)
            {

                StageManager.instance.DecreaseMove();
                StartCoroutine(animal.ReMoveToPosition(AnimalMoveOrder.Pop()));
                alien.ReMove(AlienMoveOrder.Pop());
                //animal.gameObject.transform.position = AnimalMoveOrder.Pop();
                //alien.gameObject.transform.position = AlienMoveOrder.Pop();
            }
        }
    }
}
