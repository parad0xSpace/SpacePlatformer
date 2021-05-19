using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform cam;
    Vector3 previousCamPos;
    public float parallaxScale;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;
        Vector3 pos = transform.position;
        pos.x += parallax;
        transform.position = pos;
        previousCamPos = cam.position;
    }
}
