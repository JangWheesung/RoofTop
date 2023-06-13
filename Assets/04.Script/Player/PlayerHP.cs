using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Panel
{
    public Transform textPanel;
    public TextMeshProUGUI[] totalTexts = new TextMeshProUGUI[3];
    public Button exitBtn;

    public Panel(Transform panel)
    {
        textPanel = panel.GetChild(1);
        totalTexts[0] = textPanel.GetChild(0).GetComponent<TextMeshProUGUI>();
        totalTexts[1] = textPanel.GetChild(1).GetComponent<TextMeshProUGUI>();
        totalTexts[2] = textPanel.GetChild(2).GetComponent<TextMeshProUGUI>();
        exitBtn = panel.GetChild(2).GetComponent<Button>();
    }
}

public class PlayerHP : Living
{
    PlayerGun playerGun;

    Panel panel;
    [SerializeField] GameObject gameoverPanel;

    private void Awake()
    {
        playerGun = FindObjectOfType<PlayerGun>();
    }

    protected override void Die()
    {
        base.Die();

        panel = new Panel(gameoverPanel.transform);

        gameoverPanel.GetComponent<Image>().DOFade(1, 1f).OnComplete(() =>
        {
            panel.totalTexts[0].text = $"Total Wave : {WaveManager.instance.wave}";
            panel.totalTexts[1].text = $"Total Money : {MoneyManager.instance.money}";
            panel.totalTexts[2].text = $"Total Damage : {playerGun.totalDmg}";

            panel.textPanel.DOMoveY(-120, 1f).OnComplete(() =>
            {
                panel.exitBtn.GetComponent<Image>().DOFade(1, 1f).OnComplete(() => { Time.timeScale = 0; });
            });
        });
    }
}
