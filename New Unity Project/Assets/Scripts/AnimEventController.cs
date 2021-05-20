using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventController : MonoBehaviour
{
    public Collectables collectable;
    
    public void CloseOut()
    {
        collectable.EndExplaination();
    }
}
