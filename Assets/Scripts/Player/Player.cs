using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(Animator))]
internal class Player : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));

    [SerializeField] private PlayerTarget _target;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Camera _camera;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        _target = Instantiate(_target, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        const int IndexOfLeftMouseButton = 0;

        bool isWalking = _agent.remainingDistance > _agent.stoppingDistance;

        if (Input.GetMouseButtonDown(IndexOfLeftMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit placeInfo))
            {
                _agent.SetDestination(placeInfo.point);
                _target.transform.position = placeInfo.point;
            }
        }

        _animator.SetBool(IsWalking, isWalking);
        _target.gameObject.SetActive(isWalking);
    }
}
