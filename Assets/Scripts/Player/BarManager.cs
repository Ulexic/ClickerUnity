using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarManager : MonoBehaviour {
    public Image image;
    public float health;
    public float maxHealth;
    public Animator monster;
    public Animator player;
    private KillManager _killManager;
    private PlayerManager _playerManager;

    private void Start() {
        maxHealth = PlayerPrefs.GetFloat("maxHealth", 90);
        health = PlayerPrefs.GetFloat("health", maxHealth);
        _killManager = GetComponent<KillManager>();
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }

            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.collider.CompareTag("Respawn")) {
                    _playerManager.GainMoney(maxHealth / 3);
                    PlayerPrefs.SetFloat("Money", _playerManager.Money);
                    Destroy(hit.collider.gameObject);
                }
            } else {
                // _direction = _direction == 180 ? 0 : 180;
                // _player.transform.rotation = Quaternion.Euler(0, _direction, 0);
                player.SetTrigger("attack");
                TakeDamage(_playerManager.ActiveDamage);
            }
        }
    }

    public void TakeDamage(float damage) {
        monster.SetTrigger("Hit");
        health -= damage;
        image.fillAmount = health / maxHealth;
        if (health <= 0) {
            monster.SetTrigger("Dead");
            _killManager.Kill();
            _playerManager.GainMoney(maxHealth / 15);

            PlayerPrefs.SetFloat("Money", _playerManager.Money);
        }
    }

    public void InitBar(float newMaxhealth) {
        maxHealth = newMaxhealth;
        health = newMaxhealth;
        image.fillAmount = health / maxHealth;

        PlayerPrefs.SetFloat("maxHealth", newMaxhealth);
        PlayerPrefs.SetFloat("health", newMaxhealth);
    }
}