using Mirror;
using TMPro;
using UnityEngine;

namespace Player
{
    public class MyNetworkPlayer : NetworkBehaviour
    {
    
        [SerializeField] private TMP_Text displayNameText;
        [SerializeField] private Renderer displayColourRenderer;

        [SyncVar(hook = nameof(HandleDisplayNameUpdated))][SerializeField] private string displayName = "Missing Name";
    
    
        [SyncVar(hook = nameof(HandleDisplayColourUpdated))][SerializeField] 
        private Color color;

        #region Server

        [Server]
        public void SetDisplayName(string displayName)
        {
            RpcLogNewName(displayName);
            this.displayName = displayName;
        }
        [Server]
        public void SetRandomColor()
        {
            color = Random.ColorHSV();
        }

        [ClientRpc] //Attribute to mark method as a command to send to all clients.
        private void RpcLogNewName(string newDisplayName)
        {
            Debug.Log($"New name is {newDisplayName}.");
        }
        #endregion
    
        #region Client

        private void HandleDisplayColourUpdated(Color oldColor, Color newColor)
        {
            displayColourRenderer.material.SetColor("_Color", newColor);
        }

        private void HandleDisplayNameUpdated(string oldName, string newName)
        {
            displayNameText.text = newName;
        }

        [ContextMenu("Set my name.")]
        public void SetMyName()
        {
            CmdSetDisplayName("My New Name.");
        }
    
        [Command]
        private void CmdSetDisplayName(string newDisplayName)
        {
            SetDisplayName(newDisplayName);
        }

        #endregion
    }
}
