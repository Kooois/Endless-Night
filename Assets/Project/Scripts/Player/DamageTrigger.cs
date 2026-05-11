using UnityEngine;

/// <summary>
/// 伤害触发器 - 设计文档要求的测试组件。
/// 当另一个玩家进入这个触发区域时，受到指定伤害。
/// </summary>
public class DamageTrigger : MonoBehaviour
{
    [Header("伤害设置")]
    public int damageAmount = 100;

    public bool isTrueDamage = true;

    public float damageCooldown = 1f;

    // 用于冷却计时
    private float _lastDamageTime = -999f;

    /// <summary>
    /// 当有物体进入触发区域时，Unity 自动调用这个方法。
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        TryDealDamage(other);
    }

    /// <summary>
    /// 当物体持续停留在触发区域内时，每帧调用。
    /// 配合冷却时间实现持续伤害。
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        TryDealDamage(other);
    }

    private void TryDealDamage(Collider other)
    {
        // 冷却时间检查
        if (Time.time - _lastDamageTime < damageCooldown) return;

        // 不要伤害自己（检查是否是自己的父物体）
        if (other.transform == transform.parent) return;
        if (other.transform.IsChildOf(transform.root)) return;

        // 尝试获取目标身上的 PlayerStats 组件
        if (other.TryGetComponent<PlayerStats>(out PlayerStats targetStats))
        {
            targetStats.TakeDamage(damageAmount, isTrueDamage);
            _lastDamageTime = Time.time;

            Debug.Log($"[DamageTrigger] {transform.root.name} 对 {other.name} 造成了 {damageAmount} 点{(isTrueDamage ? "真实" : "普通")}伤害！");
        }
    }
}
