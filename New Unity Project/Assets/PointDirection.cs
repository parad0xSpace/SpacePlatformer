using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDirection : MonoBehaviour
{
    public GameObject pointer;
    public Transform player;
    [Range(0.0f, 10.0f)]
    public float adjustSize;

    void Update()
    {
        Vector3 pPos = new Vector3(player.position.x, player.position.y, player.position.z);

        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            //look N
            pointer.transform.position = new Vector3(pPos.x, pPos.y + adjustSize, pPos.z);
            Debug.Log("North");
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            //look W
            pointer.transform.position = new Vector3(pPos.x - adjustSize, pPos.y, pPos.z);
            Debug.Log("West");
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            // look E
            Debug.Log("East");
            pointer.transform.position = new Vector3(pPos.x + adjustSize, pPos.y, pPos.z);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            //look S
            pointer.transform.position = new Vector3(pPos.x, pPos.y - adjustSize, pPos.z);
            Debug.Log("South");
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            //look NW
            pointer.transform.position = new Vector3(pPos.x - adjustSize, pPos.y + adjustSize, pPos.z);
            Debug.Log("NorthWest");
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            //look NE
            pointer.transform.position = new Vector3(pPos.x + adjustSize, pPos.y + adjustSize, pPos.z);
            Debug.Log("NorthEast");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            //look SW
            pointer.transform.position = new Vector3(pPos.x - adjustSize, pPos.y - adjustSize, pPos.z);
            Debug.Log("SouthWest");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            //look SE
            pointer.transform.position = new Vector3(pPos.x + adjustSize, pPos.y - adjustSize, pPos.z);
            Debug.Log("SouthEast");
        }
        else
        {
            pointer.transform.position = pPos;
        }
    }
}
