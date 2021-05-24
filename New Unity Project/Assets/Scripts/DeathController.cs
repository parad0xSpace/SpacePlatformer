using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public Animator pAnim;

    public GameObject dieParticle;

    public SceneTracker tracker;

    void Start()
    {
        dieParticle.SetActive(false);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "OneHit")
        {
            pAnim.SetTrigger("die");
        }
    }

    public void ParticleOn()
    {
        dieParticle.SetActive(true);
        bool activeStatus = dieParticle.activeSelf;
        Debug.Log(activeStatus);
    }

    public void RestartSceneTrigger()
    {
        tracker.RestartLevel();
    }
}
