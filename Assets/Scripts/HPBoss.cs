using UnityEngine;
using UnityEngine.UI;

public class HPBoss : MonoBehaviour
{
    public static HPBoss instance;

    [Header("UI Elements")]
    public Slider hpSlider;

    private platform platformScript;

    private void Awake()
    {
        instance = this;
        platformScript = Object.FindFirstObjectByType<platform>();
    }

    // ฟังก์ชันนี้จะถูกเรียกจาก SpawnBoss ทันทีที่บอสเกิด
    public void ShowHpBoss(Boss boss)
    {
        hpSlider.gameObject.SetActive(true);

        hpSlider.maxValue = boss.maxHP;
        hpSlider.value = boss.maxHP;

        if (platformScript == null) platformScript = Object.FindFirstObjectByType<platform>();
        if (platformScript != null) platformScript.OnBossSpawn();

        boss.onHPChanged += UpdateHP;
        boss.onBossDeath += HideUI;
    }

    private void UpdateHP(int current, int max)
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = max;
            hpSlider.value = current;
        }
    }

    private void HideUI()
    {
        if (hpSlider != null)
            hpSlider.gameObject.SetActive(false);

        if (platformScript != null) platformScript.OnBossDefeated();
    }
}
