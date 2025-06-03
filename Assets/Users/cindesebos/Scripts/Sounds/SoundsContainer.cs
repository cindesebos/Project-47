using UnityEngine;

namespace Scripts.Sounds
{
    public class SoundsContainer : MonoBehaviour
    {
        [field: SerializeField] public AudioClip MenuMusic { get; private set; }
        [field: SerializeField] public AudioClip GameplayMusic { get; private set; }
        [field: Space]

        [field: SerializeField] public AudioClip ItemPickupSound { get; private set; }
        [field: SerializeField] public AudioClip MovingBookshelfSound { get; private set; }
        [field: SerializeField] public AudioClip InteractionWithArtSound { get; private set; }
        [field: SerializeField] public AudioClip InteractingWithGateAccessMachineSound { get; private set; }
        [field: SerializeField] public AudioClip FuseChangingSound { get; private set; }
        [field: SerializeField] public AudioClip BrokingWindowSound { get; private set; }
        [field: SerializeField] public AudioClip OpeningDoorSound { get; private set; }
        [field: SerializeField] public AudioClip ClosingDoorSound { get; private set; }
        [field: SerializeField] public AudioClip BreakingCryochamberWithCrowbarSound { get; private set; }
        [field: SerializeField] public AudioClip RemovingHandSound { get; private set; }
        [field: SerializeField] public AudioClip ScanningHandAndEyeSound { get; private set; }
        [field: SerializeField] public AudioClip ExplosionSound { get; private set; }
        [field: Space]

        [field: SerializeField] public AudioClip InputedCorrectSafeCodeSound { get; private set; }
        [field: SerializeField] public AudioClip InputedIncorrectSafeCodeSound { get; private set; }
        [field: SerializeField] public AudioClip InputingSafeInputSound { get; private set; }
        [field: Space]

        [field: SerializeField] public AudioClip RotatingLockPinSound { get; private set; }
        [field: SerializeField] public AudioClip InputCurrentCodeSound { get; private set; }
        [field: Space]

        [field: SerializeField] public AudioClip GunShootingSound { get; private set; }

        [field: Space]
        [field: SerializeField] public AudioClip MutantTakingDamage { get; private set; }
    }
}
