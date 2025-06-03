using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Mutant
{
    public class MutantMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private Transform[] _patrolPoints;
        private float _walkSpeed;
        private float _stoppingDistance;
        private int _currentPoint = 0;

        [SerializeField] private Transform _target;

        private void OnValidate()
        {
            _navMeshAgent ??= GetComponent<NavMeshAgent>();
        }

        public void Initialize(MutantData data, Transform[] patrolPoints)
        {
            _walkSpeed = data.WalkSpeed;
            _stoppingDistance = data.StoppingDistance;
            _patrolPoints = patrolPoints;

            _navMeshAgent.speed = _walkSpeed;

            if (_patrolPoints.Length <= 0) return;

            _navMeshAgent.SetDestination(_patrolPoints[_currentPoint].position);
        }

        public void Handle()
        {
            if (_target == null)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _stoppingDistance) SetNextPoint();
            }
            else StartChassing();
        }

        private void SetNextPoint()
        {
            if (_patrolPoints.Length <= 0) return;

            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;

            _navMeshAgent.SetDestination(_patrolPoints[_currentPoint].position);
        }

        private void StartChassing()
        {
            if (_target == null) return;

            _navMeshAgent.SetDestination(_target.position);
        }

        public void SetChassingTarget(Transform target) => _target = target;

        public void Stay() => _navMeshAgent.SetDestination(transform.position);

        public void RemoveChassingTarget() => _target = null;
    }
}
