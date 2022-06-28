using Mirror;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private Camera _camera;

        [Command]
        private void CmdMove(Vector3 position)
        {
            if (!NavMesh.SamplePosition(position, out var hit, 1f, NavMesh.AllAreas)) 
                return;

            agent.SetDestination(hit.position);
        }

        public override void OnStartAuthority()
        {
            _camera = Camera.main;
        }

        [ClientCallback]
        private void Update()
        {
            if (!hasAuthority) return;
            if (!Input.GetMouseButtonDown(1)) return;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                CmdMove(hit.point);
            }
        }
    }                                                             
}