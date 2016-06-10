using UnityEngine;
using System.Collections;

public interface IDamagable
{
    void TakeDamage(float damage);
    float GetHealth();
    bool IsDead();

    void ResetHealth();
}
