using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public Animator anim;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "OneHit")
        {
            anim.SetTrigger("die");
        }
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
