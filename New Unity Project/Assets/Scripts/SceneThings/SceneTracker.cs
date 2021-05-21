using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    private int levelNumber;

    private bool sceneChanged;

    public Transform player;

    public GameObject sceneChanger;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneChanged = false;

        if(scene.name == "Lvl1")
        {
            levelNumber = 1;
            if ((PlayerPrefs.GetInt("PreviousScene") != levelNumber))
            {
                sceneChanged = true;
                PlayerPrefs.SetInt("PreviousScene", levelNumber);
            }
            else
            {
                sceneChanged = false;
            }
        }
        else if(scene.name == "Lvl2")
        {
            levelNumber = 2;
            if ((PlayerPrefs.GetInt("PreviousScene") != levelNumber))
            {
                sceneChanged = true;
                PlayerPrefs.SetInt("PreviousScene", levelNumber);
            }
            else
            {
                sceneChanged = false;
            }
        }
        else if (scene.name == "Lvl3")
        {
            levelNumber = 3;
            if ((PlayerPrefs.GetInt("PreviousScene") != levelNumber))
            {
                sceneChanged = true;
                PlayerPrefs.SetInt("PreviousScene", levelNumber);
            }
            else
            {
                sceneChanged = false;
            }
        }
        else if (scene.name == "Lvl4")
        {
            levelNumber = 4;
            if ((PlayerPrefs.GetInt("PreviousScene") != levelNumber))
            {
                sceneChanged = true;
                PlayerPrefs.SetInt("PreviousScene", levelNumber);
            }
            else
            {
                sceneChanged = false;
            }
        }

        if(!sceneChanged)
        {
            Debug.Log("Getting previous spawn position " + PlayerPrefs.GetFloat("RespawnX")+ ", " + PlayerPrefs.GetFloat("RespawnY"));
            player.position = new Vector3(PlayerPrefs.GetFloat("RespawnX"), PlayerPrefs.GetFloat("RespawnY"), player.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "levelChanger")
        {
            if (levelNumber == 1)
            {
                sceneChanger.SetActive(true);
                sceneChanger.GetComponent<Animator>().SetTrigger("LoadTwo");
            }
            else if (levelNumber == 2)
            {
                sceneChanger.SetActive(true);
                sceneChanger.GetComponent<Animator>().SetTrigger("LoadThree");
            }
            else if (levelNumber == 3)
            {
                sceneChanger.SetActive(true);
                sceneChanger.GetComponent<Animator>().SetTrigger("LoadFour");
            }
            else if (levelNumber == 4)
            {
                sceneChanger.SetActive(true);
                sceneChanger.GetComponent<Animator>().SetTrigger("LoadEnd");
            }
        }
    }
}
