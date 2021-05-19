using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefTracker : MonoBehaviour
{
    public CharaController controller;

    private bool gmActive = false;

    private void Update()
    {
        if(Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                DebugAll();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ResetPrefs();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha8) && !gmActive)
            {
                ActivateGodMode();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) && gmActive)
            {
                DisableGodMode();
            }
        }
    }

    public void DebugAll()
    {
        Debug.Log(PlayerPrefs.GetInt("ClimbingClaws"));
        Debug.Log(PlayerPrefs.GetInt("BoosterPack"));
        Debug.Log(PlayerPrefs.GetFloat("PlayerYPos"));
        Debug.Log(PlayerPrefs.GetFloat("PlayerXPos"));
        Debug.Log(PlayerPrefs.GetInt("TalkedToCarmen"));
        Debug.Log(PlayerPrefs.GetInt("TalkedToRameez"));
        Debug.Log(PlayerPrefs.GetInt("TalkedToKhina"));
        Debug.Log(PlayerPrefs.GetInt("TalkedToLyra"));
        Debug.Log(PlayerPrefs.GetFloat("RespawnX"));
        Debug.Log(PlayerPrefs.GetFloat("RespawnY"));
    }

    public void ResetPrefs()
    {
        PlayerPrefs.SetInt("ClimbingClaws", 0);
        PlayerPrefs.SetInt("BoosterPack", 0);
        PlayerPrefs.SetFloat("PlayerXPos", 0);
        PlayerPrefs.SetFloat("PlayerYPos", 0);
        PlayerPrefs.SetInt("TalkedToCarmen", 0);
        PlayerPrefs.SetInt("TalkedToRameez", 0);
        PlayerPrefs.SetInt("TalkedToKhina", 0);
        PlayerPrefs.SetInt("TalkedToLyra", 0);
        PlayerPrefs.SetFloat("RespawnX", 0);
        PlayerPrefs.SetFloat("RespawnY", 0);
    }

    public void ActivateGodMode()
    {
        Debug.Log("KAME......HAME..........");
        ResetPrefs();
        //Application.OpenURL("https://www.youtube.com/watch?v=3Ph3SfdoWYE");
        controller.HasClaws();
        controller.HasBooster();
        controller.moveSpeed = 18f;
        controller.jumpForce = 30f;
        gmActive = true;
    }

    public void DisableGodMode()
    {
        Debug.Log("HAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        ResetPrefs();
        controller.GetComponent<CharaController>().hasClaws = false;
        controller.GetComponent<CharaController>().hasPack = false;
        controller.GetComponent<CharaController>().moveSpeed = 12f;
        controller.GetComponent<CharaController>().jumpForce = 25f;
        gmActive = false;
    }
}
