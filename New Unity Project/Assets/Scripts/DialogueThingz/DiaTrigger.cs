using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaTrigger : MonoBehaviour
{
    public Dialogue dia;
    public GameObject diaSpace; //DiaControllerP2
    public CharaController charaCon;

    private void Start()
    {
        diaSpace.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            charaCon.isTalking = true;
            diaSpace.SetActive(true);
            TriggerDia();
        }
    }

    public void TriggerDia()
    {
        FindObjectOfType<DiaManger>().StarDia(dia);
    }
}
