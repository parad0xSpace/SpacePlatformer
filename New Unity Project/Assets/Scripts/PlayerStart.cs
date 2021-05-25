using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public GameObject controller;

    void Start()
    {
        if (PlayerPrefs.GetInt("ClimbingClaws") == 1)
        {
            controller.GetComponent<CharaController>().hasClaws = true;
        }
        else
        {
            controller.GetComponent<CharaController>().hasClaws = false;
        }

        if (PlayerPrefs.GetInt("BoosterPack") == 1)
        {
            controller.GetComponent<CharaController>().hasPack = true;
        }
        else
        {
            controller.GetComponent<CharaController>().hasPack = false;
        }
    }
}
