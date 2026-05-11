using UnityEngine;

/// <summary>
/// 兵种数据定义 - 管理小队成员的配置数据。
/// 
/// 根据设计文档：
/// - 每支小队 = 2近战 + 2远程 + 1治疗
/// - 最高 7 级
/// 
/// 使用方法同 ResourceData：
/// 右键 -> Create -> Game Data -> Unit Data
/// </summary>
[CreateAssetMenu(fileName = "NewUnit", menuName = "Game Data/Unit Data")]
public class UnitData : ScriptableObject
{
    [Header("基础信息")]
    public string unitName;

    [TextArea(2, 4)]
    public string description;

    public UnitRole role;
    public CivilizationType civilization;

    [Header("属性")]
    public int maxHealth = 50;
    public int attack = 10;
    public int defense = 5;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f; // 近战短，远程长

    [Header("等级成长（每级增加的数值）")]
    public int healthPerLevel = 10;
    public int attackPerLevel = 3;
    public int defensePerLevel = 2;

    [Header("视觉（暂用占位符）")]
    public Color unitColor = Color.blue; // MVP 阶段用颜色区分兵种
}

/// <summary>
/// 兵种角色类型
/// </summary>
public enum UnitRole
{
    Melee,   // 近战
    Ranged,  // 远程
    Healer   // 治疗
}
