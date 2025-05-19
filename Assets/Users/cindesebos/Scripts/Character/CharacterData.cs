using UnityEngine;

namespace Scripts.Character
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "ScriptableObjects/New Character Data")]
    public class CharacterData : MonoBehaviour
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}
