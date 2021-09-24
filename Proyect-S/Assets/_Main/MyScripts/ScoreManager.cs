using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    private Text scoreText;
    private int scoreIncrease = 1;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    public void AddScore()
    {
        scoreText.text = $"{scoreIncrease}/3";
        scoreIncrease++;
    }
}
