using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOKU : MonoBehaviour
{
    public GameObject tracker;

    private bool modeEnabled;

    private float timerSet = .5f;
    private float timer;


    private void Start()
    {
        tracker.GetComponent<PlayerPrefTracker>().ResetPrefs();
        modeEnabled = false;
        timer = timerSet;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha8) && !modeEnabled && timer <= 0)
        {
            tracker.GetComponent<PlayerPrefTracker>().EnableMovement();
            modeEnabled = true;
            timer = timerSet;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha8) && modeEnabled && timer <= 0)
        {
            tracker.GetComponent<PlayerPrefTracker>().DisableMovement();
            modeEnabled = false;
            timer = timerSet;
        }

        if(timer > -.01)
        {
            timer -= Time.deltaTime;
        }
    }
}
