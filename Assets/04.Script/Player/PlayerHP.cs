using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class Panel
{
    public GameObject gameoverText;
    public Transform textPanel;
    public TextMeshProUGUI[] totalTexts = new TextMeshProUGUI[3];
    public Button exitBtn;

    public Panel(Transform panel)
    {
        gameoverText = panel.GetChild(0).gameObject;
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
        gameoverPanel.SetActive(true);

        panel = new Panel(gameoverPanel.transform);

        gameoverPanel.GetComponent<Image>().DOFade(1, 1f);
        panel.gameoverText.GetComponent<TextMeshProUGUI>().DOFade(1, 1f).OnComplete(() =>
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            panel.totalTexts[0].text = $"Total Wave : {WaveManager.instance.wave}"; 
            panel.totalTexts[1].text = $"Total Money : {MoneyManager.instance.money}";
            panel.totalTexts[2].text = $"Total Damage : {playerGun.totalDmg}";

            panel.textPanel.DOMoveY(420, 1f).OnComplete(() =>
            {
                panel.exitBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(1, 1f);
                panel.exitBtn.GetComponent<Image>().DOFade(1, 1f).OnComplete(() => { Time.timeScale = 0; });
            });
        });
    }
}
