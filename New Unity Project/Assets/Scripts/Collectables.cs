using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private GameObject controller;

    public GameObject explainer;

    public Animator imageAnim;

    private void Start()
    {
        controller = gameObject;
        explainer.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "claws")
        {
            //Debug.Log("Touching claws");
            controller.GetComponent<CharaController>().HasClaws();
            Destroy(collision.gameObject);
            ClawExplainer();
        }
        if(collision.gameObject.tag == "pack")
        {
            controller.GetComponent<CharaController>().HasBooster();
            Destroy(collision.gameObject);
            PackExplainer();
        }
        if(collision.gameObject.tag == "anchor")
        {
            //Debug.Log("foo");
            PlayerPrefs.SetFloat("RespawnX", controller.transform.position.x);
            PlayerPrefs.SetFloat("RespawnY", controller.transform.position.y);
            Destroy(collision.gameObject);
        }
    }

    private void ClawExplainer()
    {
        //Debug.Log("Open explainer");
        explainer.SetActive(true);
        imageAnim.SetBool("isOpen", true);
    }

    private void PackExplainer()
    {
        //Debug.Log("Open explainer");
        explainer.SetActive(true);
        imageAnim.SetBool("isOpen", true);
    }

    public void ExplainerAnimator() //Dismiss Button OnClickEvent
    {
        imageAnim.SetBool("isOpen", false);
    }

    public void EndExplaination()
    {
        explainer.SetActive(false);
    }
}
