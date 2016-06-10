using UnityEngine;
using System.Collections;

public interface IAttackable
{
    void Attack(AttackType attack, IDamagable toDamage, float modifier = 0);
    float GetDamage();

}
