using UnityEngine;

public class KillManager : MonoBehaviour {
    public int killCount;
    private BarManager _barManager;
    private LevelController _levelController;
    private GameObject _monster;

    private void Start() {
        killCount = PlayerPrefs.GetInt("killCount");
        _levelController = GetComponent<LevelController>();
        _barManager = GetComponent<BarManager>();
        _monster = GameObject.Find("Monster");
    }

    public void Kill() {
        killCount++;
        PlayerPrefs.SetInt("killCount", killCount);
        Debug.Log(killCount);
        if (killCount % 5 == 0) {
            _levelController.levelUp();
        }

        if (killCount % 5 == 4) {
            _monster.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            _barManager.InitBar(killCount * 20);
        } else {
            _monster.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            _barManager.InitBar(killCount * 10);
        }
    }
}