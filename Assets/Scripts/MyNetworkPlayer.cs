using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar][SerializeField] private string displayName = "Missing Name";
    [SyncVar][SerializeField] private Color color;

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    public void SetRandomColor()
    {
        color = Random.ColorHSV();
        GetComponentInChildren<MeshRenderer>().material.SetColor("_Color",Random.ColorHSV());
    }
}
