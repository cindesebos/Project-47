using System.Collections;
using UnityEngine;

namespace Scripts.Mutant
{
    public class MutantChaser : MonoBehaviour
    {
        private Transform _eyePosition;
        private float _visionRange;
        private float _visionAngle;
        private LayerMask _characterLayer;

        private MutantMover _mover;

        private Transform _currentTarget;
        private float _maxLostTargetTime = 5f;
        [SerializeField] private float _lostTargetTimer = 0f;

        [SerializeField] private bool _isChasing = false;
        [SerializeField] private bool _isCharacterBehihd = false;
        [SerializeField] private bool _isAttackedByCharacter = false;

        public void Initialize(MutantData data, Transform eyePosition, MutantMover mover)
        {
            _visionAngle = data.VisionAngle;
            _visionRange = data.VisionRange;
            _characterLayer = data.CharacterLayer;
            _maxLostTargetTime = data.MaxLostTargetTime;

            _eyePosition = eyePosition;
            _mover = mover;
        }

        public void Handle()
        {
            if (TryFindTarget() || _isCharacterBehihd || _isAttackedByCharacter)
            {
                _lostTargetTimer = 0f;
                if (!_isChasing)
                {
                    _isChasing = true;
                    _mover.SetChassingTarget(_currentTarget);
                    Debug.Log("Start chasing target");
                }

                _isCharacterBehihd = false;
                _isAttackedByCharacter = false;
            }
            else if (_isChasing)
            {
                _lostTargetTimer += Time.deltaTime;

                if (_lostTargetTimer >= _maxLostTargetTime)
                {
                    StopChasing();

                    _isCharacterBehihd = false;
                }
                else
                {
                    if (_currentTarget != null) _mover.SetChassingTarget(_currentTarget);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
           
            if (other.gameObject.GetComponent<Character.Character>())
            {
                Debug.Log("Character is found: ");

                _isCharacterBehihd = true;

                _currentTarget = other.transform;
            }
        }

        private bool TryFindTarget()
        {
            Collider[] hits = Physics.OverlapSphere(_eyePosition.position, _visionRange, _characterLayer);

            foreach (Collider hit in hits)
            {
                Vector3 directionToTarget = (hit.transform.position - _eyePosition.position).normalized;
                float angle = Vector3.Angle(_eyePosition.forward, directionToTarget);

                if (angle < _visionAngle / 2f)
                {
                    if (Physics.Raycast(_eyePosition.position, directionToTarget, out RaycastHit raycastHit, _visionRange, _characterLayer))
                    {
                        if (raycastHit.collider == hit)
                        {
                            if (hit.TryGetComponent(out Character.Character character))
                            {
                                _currentTarget = hit.transform;
                                Debug.Log($"Target spotted: {_currentTarget.name}");
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void OnAttackedByCharacter(Transform target)
        {
            _currentTarget = target;

            _isAttackedByCharacter = true;
        }

        private void StopChasing()
        {
            _isChasing = false;

            _currentTarget = null;

            _mover.RemoveChassingTarget();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_eyePosition == null)
                return;

            Gizmos.color = new Color(1, 0, 0, 0.3f);

            Vector3 leftBoundary = Quaternion.Euler(0, -_visionAngle / 2, 0) * _eyePosition.forward;
            Vector3 rightBoundary = Quaternion.Euler(0, _visionAngle / 2, 0) * _eyePosition.forward;

            int segmentCount = 10;
            float step = _visionAngle / segmentCount;

            Vector3 prevPoint = _eyePosition.position + leftBoundary * _visionRange;

            for (int i = 1; i <= segmentCount; i++)
            {
                float currentAngle = -_visionAngle / 2 + step * i;
                Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * _eyePosition.forward;
                Vector3 nextPoint = _eyePosition.position + direction * _visionRange;

                Gizmos.DrawLine(_eyePosition.position, nextPoint);
                Gizmos.DrawLine(prevPoint, nextPoint);

                prevPoint = nextPoint;
            }
        }
#endif
    }
}