using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家 HUD（头顶显示）- 在屏幕上显示 HP。
/// </summary>
public class PlayerHUD : MonoBehaviour
{
    [Header("引用")]
    public PlayerStats playerStats;

    public Slider healthSlider;

    public Text healthText;

    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogWarning("[PlayerHUD] 没有指定 PlayerStats！请在 Inspector 中拖入 Player 的 PlayerStats 组件。");
            return;
        }

        // 订阅 HP 变化事件
        // 当 PlayerStats 中 HP 改变时，会自动调用 UpdateHealthUI 方法
        playerStats.OnHealthChanged.AddListener(UpdateHealthUI);

        // 订阅死亡事件
        playerStats.OnDeath.AddListener(OnPlayerDeath);
    }

    /// <summary>
    /// 更新 HP 显示。
    /// 参数 currentHP：当前生命值
    /// 参数 maxHP：最大生命值
    /// </summary>
    private void UpdateHealthUI(int currentHP, int maxHP)
    {
        // 更新血条（Slider 的 value 范围是 0~1，所以要算比例）
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHP / maxHP;
        }

        // 更新文字
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHP} / {maxHP}";
        }
    }

    /// <summary>
    /// 玩家死亡时的 UI 处理
    /// </summary>
    private void OnPlayerDeath()
    {
        if (healthText != null)
        {
            healthText.text = "YOU DIED";
            healthText.color = Color.red;
        }

        Debug.Log("[PlayerHUD] 玩家已死亡，显示死亡界面");
    }

    /// <summary>
    /// 销毁时取消订阅事件，防止内存泄漏。
    /// 这是一个好习惯：订阅了事件，就要在不需要时取消订阅。
    /// </summary>
    private void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged.RemoveListener(UpdateHealthUI);
            playerStats.OnDeath.RemoveListener(OnPlayerDeath);
        }
    }
}
