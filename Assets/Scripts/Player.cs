using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(Animator))]
internal class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Camera _camera;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit placeInfo))
            {
                _agent.SetDestination(placeInfo.point);
            }
        }

        _animator.SetBool("isWalking", _agent.remainingDistance > _agent.stoppingDistance);
    }
}
