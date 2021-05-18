using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaTrigger : MonoBehaviour
{
    public Dialogue dia;

    public void TriggerDia()
    {
        FindObjectOfType<DiaManger>().StarDia(dia);
    }
}
