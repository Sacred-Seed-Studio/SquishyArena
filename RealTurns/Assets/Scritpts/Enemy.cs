using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public enum Personality
    {
        Aggresive,
        Fast,
        Slow
    }
    //All of this will be the same as the player, but it's easier to keep them apart right now
    IAttackable attacker;
    IDamagable damageControl;
    UpdatePlayerUI updateUI;
    PlayerType playerType;

    public Personality personality;
    public bool attacking = true;

    public float attackDelay = 1f;

    public bool autoAttack = false;
    [HideInInspector]
    public PlayerLight playerLight;

    public Canvas menuCanvas;
    bool menuOpen;

    void Awake()
    {
        playerLight = GetComponent<PlayerLight>();
        attacker = GetComponentInChildren<IAttackable>();
        damageControl = GetComponentInChildren<IDamagable>();
        updateUI = GetComponent<UpdatePlayerUI>();
    }

    void Start()
    {
        SetupEnemy();
        playerType = GetComponentInChildren<AttackController>().playerType;
        updateUI.SetMaxHealth(GameController.GetHealth(playerType));
        if (autoAttack) StartCoroutine(StartAttacking());
    }

    void Update()
    {
        updateUI.SetHealth(damageControl.GetHealth());
    }

    //Set up a new enemy
    public void SetupEnemy()
    {
        personality = (Personality)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Personality)).Length);
    }

    AttackType lastAttack;
    IEnumerator StartAttacking()
    {
        attacking = true;
        while (attacking)
        {
            Attack(GetNextAttack(personality, lastAttack));
            yield return new WaitForSeconds(attackDelay);
        }
        yield return null;
    }

    AttackType GetNextAttack(Personality personality, AttackType lastAttack)
    {
        AttackType at = AttackType.Attack1;
        switch (personality)
        {
            case Personality.Aggresive:
                if (UnityEngine.Random.Range(0, 10) % 2 == 0) at = lastAttack;
                else
                {
                    while (at != lastAttack) at = (AttackType)UnityEngine.Random.Range(0, 3);
                }
                break;
            case Personality.Fast:
                while (at != lastAttack) at = (AttackType)UnityEngine.Random.Range(0, 3);
                break;
            case Personality.Slow:
                at = (AttackType)UnityEngine.Random.Range(0, 3);
                break;
        }
        return at;
    }

    public void Attack(AttackType attack, bool turnAttack = false)
    {
        menuCanvas.gameObject.SetActive(false);
        menuOpen = false;
        if (turnAttack)
        {
            attack = GetNextAttack(personality, lastAttack);
        }

        float mod = 0;
        switch (attack)
        {
            case AttackType.Attack1: mod = 0; break;
            case AttackType.Attack2: mod = 0.5f; break;
            case AttackType.Attack3: mod = 1f; break;
        }
        attacker.Attack(attack, GameController.controller.player, mod);
    }

    public void OpenMenu()
    {
        menuOpen = true;
        menuCanvas.gameObject.SetActive(true);
    }
}
