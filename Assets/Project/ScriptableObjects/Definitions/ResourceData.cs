using UnityEngine;

/// <summary>
/// 资源数据定义 - 使用 ScriptableObject 管理资源配置。
/// 
/// ScriptableObject 是什么？
/// - 它是 Unity 提供的一种"数据容器"
/// - 和 MonoBehaviour 不同，它不需要挂在 GameObject 上
/// - 它作为 Asset（资产文件）存在于项目中，像 .png、.wav 一样
/// - 好处：数据和逻辑分离，策划可以直接在 Inspector 里改数值，不用动代码
/// 
/// 如何创建一个资源数据实例：
/// 1. 在 Project 窗口右键 -> Create -> Game Data -> Resource Data
/// 2. 给它命名（如"木之灵"、"菱铁矿"）
/// 3. 在 Inspector 中填写各项属性
/// 
/// [CreateAssetMenu] 这个特性(Attribute)就是让你能在右键菜单里创建它。
/// </summary>
[CreateAssetMenu(fileName = "NewResource", menuName = "Game Data/Resource Data")]
public class ResourceData : ScriptableObject
{
    [Header("基础信息")]
    public string resourceName;

    [TextArea(2, 4)]
    public string description;

    public Sprite icon; // 资源图标（后续美术替换）

    [Header("资源分类")]
    public ResourceType resourceType;
    public CivilizationType exclusiveTo; // 专属文明（None 表示通用）

    [Header("数值")]
    [Tooltip("单个资源点的最大存量")]
    public int maxAmount = 100;

    [Tooltip("每次采集获得的数量")]
    public int gatherAmount = 10;

    [Tooltip("采集所需时间（秒）")]
    public float gatherTime = 2f;

    [Header("再生设置（仅可再生资源有效）")]
    [Tooltip("刷新间隔（秒）")]
    public float respawnTime = 60f;

    [Tooltip("半不可再生资源的折损比例（如0.7表示消耗100只刷新70）")]
    [Range(0f, 1f)]
    public float respawnRatio = 1f;
}

/// <summary>
/// 资源类型枚举
/// </summary>
public enum ResourceType
{
    Renewable,      // 可再生
    NonRenewable,   // 不可再生
    SemiRenewable   // 半不可再生（如水）
}

/// <summary>
/// 文明类型枚举（对应 ID 规范：1=生物，2=机甲，3=人类）
/// </summary>
public enum CivilizationType
{
    None = 0,       // 通用
    Bio = 1,        // 生物文明
    Mech = 2,       // 机甲文明
    Human = 3       // 人类文明
}
