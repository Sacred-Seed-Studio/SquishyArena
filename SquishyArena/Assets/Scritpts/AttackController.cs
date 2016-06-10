using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour, IAttackable, IDamagable
{
    public PlayerType playerType; //Can be an enemy
    Animator anim;

    float damage;
    public float health;

    public bool overrideStats = false; //use this to set the stats in the editor

    bool dead;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (!overrideStats)
        {
            health = GetMaxHealth();
            damage = GameController.GetDamage(playerType);
        }
    }

    public void Attack(AttackType attack, IDamagable toDamage, float modifier = 0)
    {
        PlayAttackSound(attack, toDamage);
        float attackType = 0;
        switch (attack)
        {
            case AttackType.Attack1: attackType = 1; break;
            case AttackType.Attack2: attackType = 2; break;
            case AttackType.Attack3: attackType = 3; break;
        }
        anim.SetFloat("AttackType", attackType);
        anim.SetTrigger("Attack");
        toDamage.TakeDamage(GetDamage() + modifier);
    }

    public void PlayAttackSound(AttackType attack, IDamagable toDamage)
    {
        if (GetComponentInParent<Player>() != null)
        {
            switch (attack)
            {
                case AttackType.Attack1:
                    AudioController.controller.PlaySound(SoundType.Attack1);
                    break;
                case AttackType.Attack2:
                    AudioController.controller.PlaySound(SoundType.Attack2);
                    break;
                case AttackType.Attack3:
                    AudioController.controller.PlaySound(SoundType.Attack3);
                    break;
                default:
                    break;
            }
        }
        else
        {
            AudioController.controller.PlaySound(SoundType.EnemyAttack);
        }


    }
    public float GetDamage()
    {
        return damage;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, health); //Can never go up in health
        if (health == 0) dead = true;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return GameController.GetHealth(playerType);
    }

    public bool IsDead()
    {
        return dead;
    }

    public void ResetHealth()
    {
        StartCoroutine(StartResetHealth(0.5f));
    }


    IEnumerator StartResetHealth(float delay)
    {
        dead = false;
        yield return new WaitForSeconds(delay);
        health = GetMaxHealth();
    }
}
