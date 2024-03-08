using UnityEngine;

public class LevelController : MonoBehaviour {
    public int level;
    private PlayerManager _playerManager;

    private void Start() {
        _playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        level = PlayerPrefs.GetInt("Level");
    }

    public void levelUp() {
        level++;
        _playerManager.levelText.text = $"Level: {level}";
        PlayerPrefs.SetInt("Level", level);
        Debug.Log(level);
    }
}