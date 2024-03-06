using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DebugButtonScript : NetworkBehaviour
{
    public void ChangeColor()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(255, 0, 255, 1);
    }
}
