using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMove : MonoBehaviour
{
    // Start is called before the first frame update


    public void Move()
    {

    }

    public void AnimalCheck()
    {
        /*
          Debug.DrawRay(new Vector3(transform.position.x, 2, transform.position.z), direction, Color.red,1f);

        if (Physics.Raycast(new Vector3(transform.position.x, 2, transform.position.z), direction, out animalHit))
        {
            if (animalHit.collider.CompareTag("Animal"))
            {
                Debug.Log("앞에 동물!");
                StartCoroutine(WaitAndFinish());
                AnimalBroken.gameObject.SetActive(true);
                AnimalBroken.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);

            }
        }
     
    }


    IEnumerator WaitAndFinish()
    {
        yield return new WaitForSeconds(0.1f);
        AnimalBroken.Play();
        yield return new WaitForSeconds(0.3f);
        StageManager.instance.StageFail();
        // StageManager.instance.StageFinish();
    }   */
    }
}
