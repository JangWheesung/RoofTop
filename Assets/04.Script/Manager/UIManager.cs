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
    [SerializeField] private TextMeshProUGUI coreText;
    [SerializeField] private Slider HPslider;
    [SerializeField] private TextMeshProUGUI HPsliderText;

    [Header("GunPanel")]
    private GunPanelValue maxbulletPanel;
    [SerializeField] private GameObject maxbulletObj;
    private GunPanelValue magazinePanel;
    [SerializeField] private GameObject magazineObj;
    private GunPanelValue powerPanel;
    [SerializeField] private GameObject powerObj;

    [Header("CorePanel")]
    private GunPanelValue speedPanel;
    [SerializeField] private GameObject speedObj;
    private GunPanelValue hpPanel;
    [SerializeField] private GameObject hpObj;
    private GunPanelValue healPanel;
    [SerializeField] private GameObject healObj;

    private GunLevelManager gunLevelManager;
    private CoreLevelManager coreLevelManager;

    private PlayerGun playerGun;
    private PlayerHP playerHP;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        maxbulletPanel = new GunPanelValue(maxbulletObj);
        magazinePanel = new GunPanelValue(magazineObj);
        powerPanel = new GunPanelValue(powerObj);
        speedPanel = new GunPanelValue(speedObj);
        hpPanel = new GunPanelValue(hpObj);
        healPanel = new GunPanelValue(healObj);

        gunLevelManager = FindObjectOfType<GunLevelManager>();
        coreLevelManager = FindObjectOfType<CoreLevelManager>();

        playerGun = FindObjectOfType<PlayerGun>();
        playerHP = FindObjectOfType<PlayerHP>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        StatUI();
        GunPanleUIAll();
        CorePanleUIAll();
    }

    void StatUI()
    {
        waveText.text = "Wave " + WaveManager.instance.wave;
        bulletText.text = $"{playerGun.nowBullet}/{playerGun.maxBullets}";
        magazineText.text = playerGun.magazine.ToString();

        HPslider.maxValue = playerHP.maxHealth;
        HPslider.value = playerHP.health;
        HPsliderText.text = $"{Mathf.Round(playerHP.health * 10) / 10f} / {playerHP.maxHealth}";

        moneyText.text = $"Money {MoneyManager.instance.money}";
        coreText.text = $"Core {MoneyManager.instance.core}";
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

    void CorePanleUIAll()
    {
        speedPanel.level.text = $"Level : {coreLevelManager.speedLevel}";
        hpPanel.level.text = $"Level : {coreLevelManager.hpLevel}";
        healPanel.level.text = $"Level : {coreLevelManager.healLevel}";

        speedPanel.ability.text = $"Speed : {playerMovement.walkingSpeed}";
        hpPanel.ability.text = $"Hp : {playerHP.maxHealth}";
        healPanel.ability.text = $"Heal : {playerHP.healing}";

        if (coreLevelManager.speedLevel > 4)
            speedPanel.cost.text = "Max";
        if (coreLevelManager.hpLevel > 4)
            hpPanel.cost.text = "Max";
        if (coreLevelManager.healLevel > 4)
            healPanel.cost.text = "Max";
    }

    public void ExitBtn(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }
}
