using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 玩家属性系统 - 管理 HP、防御、等级等数值。
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("基础属性")]
    public int maxHealth = 100;

    public int defense = 20;

    public int level = 1;

    [Header("事件")]
    public UnityEvent<int, int> OnHealthChanged;

    public UnityEvent OnDeath;

    public int CurrentHealth { get; private set; }

    // 是否已死亡
    public bool IsDead { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
        IsDead = false;

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    /// <summary>
    /// 受到伤害的方法。
    /// </summary>
    public void TakeDamage(int damage, bool isTrueDamage = false)
    {
        if (IsDead) return; // 已经死了就不再受伤

        int actualDamage;

        if (isTrueDamage)
        {
            // 真实伤害：直接扣，不算防御
            actualDamage = damage;
        }
        else
        {
            // 普通伤害
            actualDamage = Mathf.Max(damage - defense, 1);
        }

        // 扣血
        CurrentHealth = Mathf.Clamp(CurrentHealth - actualDamage, 0, maxHealth);

        Debug.Log($"[PlayerStats] {gameObject.name} 受到 {actualDamage} 点伤害！剩余HP: {CurrentHealth}/{maxHealth}");

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

       
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 恢复生命值
    /// </summary>
    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        Debug.Log($"[PlayerStats] {gameObject.name} 恢复 {amount} 点HP！当前HP: {CurrentHealth}/{maxHealth}");
    }

    /// <summary>
    /// 死亡处理
    /// </summary>
    private void Die()
    {
        IsDead = true;
        Debug.Log($"[PlayerStats] {gameObject.name} 已死亡！");
        OnDeath?.Invoke();

        // 死亡后简单禁用玩家控制
        var controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }
    }
}
