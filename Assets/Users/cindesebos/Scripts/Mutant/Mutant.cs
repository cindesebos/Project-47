using UnityEngine;
using Zenject;

namespace Scripts.Mutant
{
    public class Mutant : MonoBehaviour
    {
        [SerializeField] private MutantData _data;
        [SerializeField] private MutantHealth _health;
        [SerializeField] private MutantMover _mover;
        [SerializeField] private MutantChaser _chaser;
        [SerializeField] private MutantAttacker _attacker;
        [SerializeField] private MutantView _view;
        [SerializeField] private Animator _animator;
        [Space]

        [SerializeField] private BodyPartDamageMultiplier _headPart;
        [SerializeField] private BodyPartDamageMultiplier _bodyPart;
        [SerializeField] private BodyPartDamageMultiplier[] _armsParts;
        [SerializeField] private BodyPartDamageMultiplier[] _legsParts;
        [Space]

        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private Transform _eyePosition;

        private Character.Character _character;
        [SerializeField] private bool _isAttacking;

        private void OnValidate()
        {
            _health ??= GetComponent<MutantHealth>();
            _mover ??= GetComponent<MutantMover>();
            _chaser ??= GetComponent<MutantChaser>();
            _attacker ??= GetComponent<MutantAttacker>();
            _view ??= GetComponent<MutantView>();
            _animator ??= GetComponent<Animator>();

            if (_data) InitializeParts();
        }

        [Inject]
        private void Construct(Character.Character character)
        {
            _character = character;
        }

        private void Start()
        {
            InitializeParts();

            _health.Initialize(_data);
            _mover.Initialize(_data, _patrolPoints);
            _chaser.Initialize(_data, _eyePosition, _mover);
            _attacker.Initialize(_data);
            _view.Initialize(_animator, _attacker);

            _health.OnAppliedDamage += OnAppliedDamage;
            _attacker.OnAttackStarted += OnAttackStarted;
            _attacker.OnAttackFinished += OnAttackFinished;
        }

        private void InitializeParts()
        {
            _headPart.Initialize(_data.HeadDamageMultiplier, _data, _health);
            _bodyPart.Initialize(_data.BodyDamageMultiplier, _data, _health);

            foreach (var armPart in _armsParts) armPart.Initialize(_data.ArmsDamageMultiplier, _data, _health);

            foreach (var legPart in _legsParts) legPart.Initialize(_data.LegsDamageMultiplier, _data, _health);
        }

        private void OnAppliedDamage() => _chaser.OnAttackedByCharacter(_character.transform);

        private void OnAttackStarted()
        {
            _isAttacking = true;

            _mover.Stay();
        }

        private void OnAttackFinished() => _isAttacking = false;

        private void Update()
        {
            if (_isAttacking) return;

            _mover.Handle();
            _chaser.Handle();
        }

        private void OnDestroy()
        {
            _health.OnAppliedDamage -= OnAppliedDamage;
            _attacker.OnAttackStarted -= OnAttackStarted;
            _attacker.OnAttackFinished -= OnAttackFinished;
        }
    }
}
