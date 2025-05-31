public interface IAttacker
{
    float Damage { get; }

    void ApplyAttack(IDamageable target, float calculatedDamage);
}