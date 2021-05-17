using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefTracker : MonoBehaviour
{
    public GameObject controller;

    public void DebugAll()
    {
        Debug.Log(PlayerPrefs.GetInt("ClimbingClaws"));
        Debug.Log(PlayerPrefs.GetInt("BoosterPack"));
        Debug.Log(PlayerPrefs.GetFloat("PlayerYPos"));
        Debug.Log(PlayerPrefs.GetFloat("PlayerXPos"));
    }

    public void ResetPrefs()
    {
        PlayerPrefs.SetInt("ClimbingClaws", 0);
        PlayerPrefs.SetInt("BoosterPack", 0);
        PlayerPrefs.SetFloat("PlayerXPos", 0);
        PlayerPrefs.SetFloat("PlayerYPos", 0);
    }

    public void EnableMovement()
    {
        Debug.Log("KAME......HAME..........");
        //Application.OpenURL("https://www.youtube.com/watch?v=3Ph3SfdoWYE");
        controller.GetComponent<CharaController>().HasClaws();
        controller.GetComponent<CharaController>().HasBooster();
        controller.GetComponent<CharaController>().moveSpeed = 28f;
        controller.GetComponent<CharaController>().jumpForce = 60f;
    }

    public void DisableMovement()
    {
        Debug.Log("HAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        ResetPrefs();
        controller.GetComponent<CharaController>().moveSpeed = 7f;
        controller.GetComponent<CharaController>().jumpForce = 20f;
    }
}
