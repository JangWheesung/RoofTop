using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GunPanelValue
{
    [HideInInspector]
    public TextMeshProUGUI level;
    [HideInInspector]
    public TextMeshProUGUI cost;
    [HideInInspector]
    public TextMeshProUGUI ability;
    [HideInInspector]
    public Button btn;

    public GunPanelValue(GameObject panel)
    {
        level = panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        btn = panel.transform.GetChild(3).GetComponent<Button>();
        ability = panel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        cost = btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
}

public class TurretPanel
{
    [HideInInspector]
    public Button level;
    [HideInInspector]
    public TextMeshProUGUI cost;

    public TurretPanel(GameObject panel)
    {
        level = panel.transform.GetChild(2).GetComponent<Button>();
        cost = level.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
};

public class UIManager : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Slider HPslider;

    [Header("GunPanel")]
    private GunPanelValue maxbulletPanel;
    [SerializeField] private GameObject maxbulletObj;
    private GunPanelValue magazinePanel;
    [SerializeField] private GameObject magazineObj;
    private GunPanelValue powerPanel;
    [SerializeField] private GameObject powerObj;

    private GunLevelManager gunLevelManager;
    private PlayerGun playerGun;
    private PlayerHP playerHP;

    private float maxHp;

    private void Awake()
    {
        maxbulletPanel = new GunPanelValue(maxbulletObj);
        magazinePanel = new GunPanelValue(magazineObj);
        powerPanel = new GunPanelValue(powerObj);

        gunLevelManager = FindObjectOfType<GunLevelManager>();
        playerGun = FindObjectOfType<PlayerGun>();
        playerHP = FindObjectOfType<PlayerHP>();

        maxHp = playerHP.health;
    }

    private void Update()
    {
        StatUI();
        GunPanleUIAll();
    }

    void StatUI()
    {
        waveText.text = "Wave " + WaveManager.instance.wave;
        bulletText.text = $"{playerGun.nowBullet}/{playerGun.maxBullets}";
        magazineText.text = playerGun.magazine.ToString();

        HPslider.maxValue = maxHp;
        HPslider.value = playerHP.health;

        moneyText.text = $"Money {MoneyManager.instance.money}";
    }

    void GunPanleUIAll()
    {
        GunPanleUI(maxbulletPanel, gunLevelManager.maxBulletLevelCostume[gunLevelManager.maxBulletLevel - 1], gunLevelManager.maxBulletLevel);
        GunPanleUI(magazinePanel, gunLevelManager.magazineLevelCostume[Mathf.Clamp(gunLevelManager.magazineLevel - 1, 0, 18)], gunLevelManager.magazineLevel);
        GunPanleUI(powerPanel, null, gunLevelManager.firepowerLevel);

        maxbulletPanel.ability.text = playerGun.maxBullets.ToString();
        magazinePanel.ability.text = playerGun.magazine.ToString();
        powerPanel.ability.text = playerGun.firePower.ToString();
    }

    void GunPanleUI(GunPanelValue gunPanelValue, LevelUpGraph levelUpGraph, int nowLevel)
    {
        if (gunPanelValue == maxbulletPanel && nowLevel >= 9)
        {
            gunPanelValue.level.text = "MAX";
            gunPanelValue.cost.text = "MAX";
        }
        else if (gunPanelValue == powerPanel)
        {
            gunPanelValue.level.text = nowLevel.ToString();
            gunPanelValue.cost.text = (10 + ((int)Mathf.Floor((nowLevel - 1) / 2) * 3)).ToString();
        }
        else
        {
            gunPanelValue.level.text = nowLevel.ToString();
            gunPanelValue.cost.text = levelUpGraph.cost.ToString();
        }
    }

    public void ExitBtn(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }
}
