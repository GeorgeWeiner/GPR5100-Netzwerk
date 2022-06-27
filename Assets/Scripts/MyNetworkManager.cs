using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("I connected to a server.");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        var myNetworkPlayer = conn.identity.GetComponent<MyNetworkPlayer>();
        myNetworkPlayer.SetDisplayName($"Player {numPlayers}");
        myNetworkPlayer.SetRandomColor();
        
        Debug.LogFormat("Added player to the server. The server now has {0} players", numPlayers);
    }
}
