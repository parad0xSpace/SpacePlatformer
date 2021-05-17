using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private GameObject controller;


    private void Start()
    {
        controller = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag =="claws")
        {
            controller.GetComponent<CharaController>().HasClaws();
        }
        if(gameObject.tag == "pack")
        {
            controller.GetComponent<CharaController>().HasBooster();
        }
    }
}
