using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class GunPanelValue
{

}

public class UIManager : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private Slider HPslider;
    [Header("Stat")]
    //[SerializeField] private GameObject 

    private GunLevelManager gunLevelManager;
    private PlayerGun playerGun;
    private PlayerHP playerHP;

    private float maxHp;

    private void Awake()
    {
        //gunLevelManager = FindObjectOfType<GunLevelManager>();

        playerGun = FindObjectOfType<PlayerGun>();
        playerHP = FindObjectOfType<PlayerHP>();

        maxHp = playerHP.health;
    }

    private void Update()
    {
        TextUp();
        HPBarUp();
    }

    void TextUp()
    {
        waveText.text = "Wave " + WaveManager.instance.wave;
        bulletText.text = $"{playerGun.nowBullet}/{playerGun.maxBullets}";
        magazineText.text = playerGun.magazine.ToString();
    }

    void HPBarUp()
    {
        HPslider.maxValue = maxHp;
        HPslider.value = playerHP.health;
    }
}
