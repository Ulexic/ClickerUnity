using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public Button buyPassiveDamageUpgrade;
    public Button buyActiveDamageUpgrade;
    public TMP_Text activeDamageText;
    public TMP_Text levelText;
    public TMP_Text moneyText;
    public TMP_Text passiveDamageText;
    private readonly float _interval = 3f;
    private BarManager _barManager;
    private float _time;
    public float ActiveDamage { get; set; }
    public float PassiveDamage { get; set; }
    public float Money { get; set; }
    public float ActiveDamageUpgradeCost { get; set; } = 10f;
    public float PassiveDamageUpgradeCost { get; set; } = 10f;
    public float ActiveDamageUpgradeValue { get; set; } = 10f;
    public float PassiveDamageUpgradeValue { get; set; } = 10f;

    private void Start() {
        var btnBuyPassiveDamageUpgrade = buyPassiveDamageUpgrade.GetComponent<Button>();
        var btnBuyActiveDamageUpgrade = buyActiveDamageUpgrade.GetComponent<Button>();

        levelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();
        moneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();
        passiveDamageText = GameObject.Find("PassiveDamageText").GetComponent<TMP_Text>();
        activeDamageText = GameObject.Find("ActiveDamageText").GetComponent<TMP_Text>();

        _time = 0f;
        _barManager = GameObject.Find("Player").GetComponent<BarManager>();

        btnBuyPassiveDamageUpgrade.onClick.AddListener(BuyPassiveDamageUpgrade);
        btnBuyActiveDamageUpgrade.onClick.AddListener(BuyActiveDamageUpgrade);
    }

    private void Update() {
        _time += Time.deltaTime;
        if (PassiveDamage > 0 && _time >= _interval) {
            _barManager.TakeDamage(PassiveDamage);
            _time -= _interval;
        }
    }

    private void BuyPassiveDamageUpgrade() {
        if (Money >= PassiveDamageUpgradeCost) {
            Money -= PassiveDamageUpgradeCost;
            PassiveDamage += PassiveDamageUpgradeValue;
            PassiveDamageUpgradeCost *= 2;
            PassiveDamageUpgradeValue *= 1.2f;

            CheckUpgradeCost();

            moneyText.text = $"Money: ${Money}";
            buyPassiveDamageUpgrade.GetComponentInChildren<TMP_Text>().text =
                $"Passive Damage Upgrade\n${PassiveDamageUpgradeCost}\n+{PassiveDamageUpgradeValue} dmg/s";
            passiveDamageText.text = $"Passive Damage: {PassiveDamage}";

            PlayerPrefs.SetFloat("Money", Money);
            PlayerPrefs.SetFloat("PassiveDamage", PassiveDamage);
            PlayerPrefs.SetFloat("PassiveDamageUpgradeCost", PassiveDamageUpgradeCost);
            PlayerPrefs.SetFloat("PassiveDamageUpgradeValue", PassiveDamageUpgradeValue);

            Debug.Log(PlayerPrefs.GetFloat("PassiveDamageUpgradeCost"));
        }
    }

    private void BuyActiveDamageUpgrade() {
        if (Money >= ActiveDamageUpgradeCost) {
            Money -= ActiveDamageUpgradeCost;
            ActiveDamage += ActiveDamageUpgradeValue;
            ActiveDamageUpgradeCost *= 2;
            ActiveDamageUpgradeValue *= 1.2f;

            CheckUpgradeCost();

            moneyText.text = $"Money: ${Money}";
            buyActiveDamageUpgrade.GetComponentInChildren<TMP_Text>().text =
                $"Active Damage Upgrade\n${ActiveDamageUpgradeCost}\n+{ActiveDamageUpgradeValue} dmg";
            activeDamageText.text = $"Active Damage: {ActiveDamage}";

            PlayerPrefs.SetFloat("Money", Money);
            PlayerPrefs.SetFloat("ActiveDamage", ActiveDamage);
            PlayerPrefs.SetFloat("ActiveDamageUpgradeCost", ActiveDamageUpgradeCost);
            PlayerPrefs.SetFloat("ActiveDamageUpgradeValue", ActiveDamageUpgradeValue);
        }
    }

    public void GainMoney(float money) {
        Money += money;
        moneyText.text = $"Money: ${Money}";

        CheckUpgradeCost();

        PlayerPrefs.SetFloat("Money", Money);
    }

    private void CheckUpgradeCost() {
        if (PassiveDamageUpgradeCost >= Money) {
            buyPassiveDamageUpgrade.interactable = false;
        } else {
            buyPassiveDamageUpgrade.interactable = true;
        }

        if (ActiveDamageUpgradeCost >= Money) {
            buyActiveDamageUpgrade.interactable = false;
        } else {
            buyActiveDamageUpgrade.interactable = true;
        }
    }
}