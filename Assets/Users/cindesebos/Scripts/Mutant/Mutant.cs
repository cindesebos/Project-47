using UnityEngine;

namespace Scripts.Mutant
{
    public class Mutant : MonoBehaviour
    {
        [SerializeField] private MutantData _data;
        [SerializeField] private MutantHealth _health;
        [SerializeField] private MutantMover _mover;
        [Space]

        [SerializeField] private BodyPartDamageMultiplier _headPart;
        [SerializeField] private BodyPartDamageMultiplier _bodyPart;
        [SerializeField] private BodyPartDamageMultiplier[] _armsParts;
        [SerializeField] private BodyPartDamageMultiplier[] _legsParts;

        private void OnValidate()
        {
            _health ??= GetComponent<MutantHealth>();
            _mover ??= GetComponent<MutantMover>();

            if (_data) InitializeParts();
        }

        private void Start()
        {
            InitializeParts();

            _health.Initialize(_data);
        }

        private void InitializeParts()
        {
            _headPart.Initialize(_data.HeadDamageMultiplier, _data, _health);
            _bodyPart.Initialize(_data.BodyDamageMultiplier, _data, _health);

            foreach (var armPart in _armsParts) armPart.Initialize(_data.ArmsDamageMultiplier, _data, _health);

            foreach (var legPart in _legsParts) legPart.Initialize(_data.LegsDamageMultiplier, _data, _health);
        }

        private void Update()
        {
            _mover.Handle();
        }
    }
}
