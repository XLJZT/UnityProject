using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStats : MonoBehaviour
{

    public event Action<int, int> UpdateHealthBarOnAttack;

    public Character_SO templateData;
    public Character_SO character;
    public AttackSO attackSO;
    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if (templateData != null)
        {
            character = Instantiate(templateData);
        }
    }
    public int MaxHealth
    {
        get { if (character != null) { return character.maxHealth; } else { return 0; } }
        set { character.maxHealth = value; }
    }
    public int CurrentHealth
    {
        get { if (character != null) { return character.currentHealth; } else { return 0; } }
        set { character.currentHealth = value; }
    }
    public int BaseDefence
    {
        get { if (character != null) { return character.baseDefence; } else { return 0; } }
        set { character.baseDefence = value; }
    }
    public int CurrentDefence
    {
        get { if (character != null) { return character.currentDefence; } else { return 0; } }
        set { character.currentDefence = value; }
    }
    public float AttackRange { get {  return attackSO.attackRange; } }
    public float SkillRange { get { return attackSO.skillRange; } }
    public float CoolDown { get { return attackSO.coolDown; } }
    public float MinDamage { get { return attackSO.minDamage; } }
    public float MaxDamage { get { return attackSO.maxDamage; } }

    public float CriticalMultiplier { get { return attackSO.criticalMultiplier; } }
    public float CriticalChance { get { return attackSO.criticalChance; } }
    
    public void TakeDamage(CharacterStats attacker,CharacterStats defender)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDefence, 0);
        defender.CurrentHealth = Mathf.Max(defender.CurrentHealth - damage, 0);
        //defender受伤的动画 暴击才会有
        if (attacker.isCritical)
        {
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }

        defender.UpdateHealthBarOnAttack?.Invoke(defender.CurrentHealth, defender.MaxHealth);
        if (defender.CurrentHealth <= 0)
        {
            attacker.character.UpdateExp(defender.character.killPoint);
        }
    }

    public void TakeDamage(int damage,CharacterStats defender)
    {
        int currentDamage = Mathf.Max(damage - defender.CurrentDefence, 0);
        defender.CurrentHealth = Mathf.Max(defender.CurrentHealth - currentDamage, 0);
        UpdateHealthBarOnAttack?.Invoke(defender.CurrentHealth, defender.MaxHealth);

    }

    int CurrentDamage()
    {
        float damage = UnityEngine.Random.Range(MinDamage, MaxDamage); ;
        if (isCritical)
        {
            damage *= CriticalMultiplier;
            Debug.Log("暴击：" + damage);

        }
        return (int)damage;
    }
}
