using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void FinishedAnimation()
    {
        gameObject.SetActive(false);
    }

    public void LoadLvlOne()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void LoadLvlTwo()
    {
        SceneManager.LoadScene("Lvl2");
    }

    public void LoadLvlThree()
    {
        SceneManager.LoadScene("Lvl3");
    }

    public void LoadLvlFour()
    {
        SceneManager.LoadScene("Lvl4");
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("EndScene");
    }
}
