using TMPro;
using UnityEngine;

public class SaveSystem : MonoBehaviour {
    private PlayerManager _playerManager;

    private void Start() {
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        Load();
        _playerManager.passiveDamageText.text = $"Passive Damage: {_playerManager.PassiveDamage}";
        _playerManager.activeDamageText.text = $"Active Damage: {_playerManager.ActiveDamage}";

        _playerManager.buyActiveDamageUpgrade.GetComponentInChildren<TMP_Text>().text =
            $"Active Damage Upgrade\n${_playerManager.ActiveDamageUpgradeCost}\n+{_playerManager.ActiveDamageUpgradeValue} dmg";
        _playerManager.buyPassiveDamageUpgrade.GetComponentInChildren<TMP_Text>().text =
            $"Passive Damage Upgrade\n${_playerManager.PassiveDamageUpgradeCost}\n+{_playerManager.PassiveDamageUpgradeValue} dmg/s";
        _playerManager.levelText.text = $"Level: {PlayerPrefs.GetInt("Level")}";
        _playerManager.moneyText.text = $"Money: ${_playerManager.Money}";
    }

    private void Load() {
        _playerManager.Money = PlayerPrefs.GetFloat("Money", 0);
        _playerManager.ActiveDamage = PlayerPrefs.GetFloat("ActiveDamage", 10);
        _playerManager.PassiveDamage = PlayerPrefs.GetFloat("PassiveDamage", 0);
        _playerManager.PassiveDamageUpgradeCost = PlayerPrefs.GetFloat("PassiveDamageUpgradeCost", 10);
        _playerManager.ActiveDamageUpgradeCost = PlayerPrefs.GetFloat("ActiveDamageUpgradeCost", 10);
        _playerManager.PassiveDamageUpgradeValue = PlayerPrefs.GetFloat("PassiveDamageUpgradeValue", 5);
        _playerManager.ActiveDamageUpgradeValue = PlayerPrefs.GetFloat("ActiveDamageUpgradeValue", 5);
    }
}