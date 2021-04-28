using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{

    public GameObject virtCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtCam.SetActive(true);
            //Debug.Log("I'm on!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType() != typeof(BoxCollider2D))
        {
            virtCam.SetActive(false);
           //Debug.Log("I'm off!");
        }
    }

}