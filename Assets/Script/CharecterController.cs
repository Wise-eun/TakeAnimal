using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    [SerializeField]
    AlienMoveReNew alien;
    [SerializeField]
    AnimalMoveReNew animal;

    bool Re = false;
    Stack<int> MoveOrder = new Stack<int>();
    public void MoveUp()
    {
        if(!Re)
        {
            StageManager.instance.IncreaseMove();
            MoveOrder.Push(0);
        }

        alien.UpMove();
        animal.DownMove();

    }
    public void MoveDown()
    {
        if (!Re)
        {
            StageManager.instance.IncreaseMove();
            MoveOrder.Push(1);
        }   
        alien.DownMove();
        animal.UpMove();
    }
    public void MoveRight()
    {
        if (!Re)
        {
            StageManager.instance.IncreaseMove();
            MoveOrder.Push(2);
        }
        alien.RightMove();
        animal.LeftMove();
    }
    public void MoveLeft()
    {
        if (!Re)
        {
            StageManager.instance.IncreaseMove();
            MoveOrder.Push(3);
        }
        alien.LeftMove();
        animal.RightMove();

    }
    int num;
    public void MoveRe()
    {
        if(StageManager.instance.MoveNum!= 0)
        {
            Re = true;
            StageManager.instance.DecreaseMove();
            num = MoveOrder.Pop();
            if (num == 0)
                MoveDown();
            else if (num == 1)
                MoveUp();
            else if (num == 2)
                MoveLeft();
            else if (num == 3)
                MoveRight();

            Re = false;
        }

    }
}
