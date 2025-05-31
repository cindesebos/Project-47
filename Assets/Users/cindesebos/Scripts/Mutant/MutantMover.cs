using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Mutant
{
    public class MutantMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _stoppingDistance;
        [SerializeField] private int _currentPoint = 0;

        [SerializeField] private Transform[] _patrolPoints;

        private void OnValidate()
        {
            _navMeshAgent ??= GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            if (_patrolPoints.Length <= 0) return;

            _navMeshAgent.speed = _walkSpeed;

            _navMeshAgent.SetDestination(_patrolPoints[_currentPoint].position);
        }

        public void Handle()
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _stoppingDistance) SetNextPoint();
        }

        private void SetNextPoint()
        {
            if (_patrolPoints.Length <= 0) return;

            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;

            _navMeshAgent.SetDestination(_patrolPoints[_currentPoint].position);
        }
    }
}
