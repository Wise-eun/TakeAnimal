using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Debug.Log("Meet");

            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);

         
        }
    }

}
