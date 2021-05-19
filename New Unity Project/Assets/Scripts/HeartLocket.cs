using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLocket : MonoBehaviour
{
    float my;
    float be;
    float loved;

    void Start()
    {
        my = 6f;
        be = 9f;
        loved = 420f;
    }

    void KissKissFallInLove()
    {
        Vector3 myBeloved = new Vector3(my, be, loved);
    }
}
