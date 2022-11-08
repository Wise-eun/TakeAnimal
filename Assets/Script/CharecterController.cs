using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
   
   public  AlienMoveReNew alien;  
   public  AnimalMoveReNew animal;

    bool Re = false;
    Stack<Vector3> AnimalMoveOrder = new Stack<Vector3>();
    Stack<Vector3> AlienMoveOrder = new Stack<Vector3>();

    public void MoveUp()
    {
        if(!StageManager.instance.IsTake)
        {
            StageManager.instance.IncreaseMove();
            AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);

            alien.UpMove();
            animal.DownMove();
        }
      

    }
    public void MoveDown()
    {
        if (!StageManager.instance.IsTake)
        {
            StageManager.instance.IncreaseMove();
            AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);
            alien.DownMove();
            animal.UpMove();
        }
    }
    public void MoveRight()
    {
        if (!StageManager.instance.IsTake)
        {
            StageManager.instance.IncreaseMove();
            AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);
            alien.RightMove();
            animal.LeftMove();
        }
    }
    public void MoveLeft()
    {
        if (!StageManager.instance.IsTake)
        {
            StageManager.instance.IncreaseMove();
            AnimalMoveOrder.Push(animal.gameObject.transform.position);
            AlienMoveOrder.Push(alien.gameObject.transform.position);
            alien.LeftMove();
            animal.RightMove();
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
