using UnityEngine;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour {
    private const float Duration = 3.0f;
    public GameObject bonusPrefab;
    public new Animator animation;
    private BarManager _barManager;
    private GameObject _bonus;
    private bool _isSpawned;
    private float _spawnTime;

    private void Start() {
        _barManager = GameObject.Find("Player").GetComponent<BarManager>();
    }

    private void Update() {
        if (_isSpawned) {
            if (Time.time - _spawnTime > Duration) {
                _isSpawned = false;
                Destroy(_bonus);
            }
        } else {
            var random = Random.Range(1, 10000);
            if (random == 1) {
                _bonus = Instantiate(bonusPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                _isSpawned = true;
                _spawnTime = Time.time;
            } else if (random == 2) {
                animation.SetTrigger("rollAttack");
                _barManager.TakeDamage(3 * _barManager.GetComponent<PlayerManager>().ActiveDamage);
            }
        }
    }
}