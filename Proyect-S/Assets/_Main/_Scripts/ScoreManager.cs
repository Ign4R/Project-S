using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private Text scoreText;
    private int scoreIncrease = 1;
    private int totalScore;
    private LifeController lifeController;
    [SerializeField] private int filesQuantity;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        Instance = this;
        scoreText = GetComponent<Text>();
        lifeController = player.GetComponent<LifeController>();
        scoreText.text = $"{totalScore}/{filesQuantity}";
    }

    private void Update()
    {
        if(totalScore == filesQuantity)
        {
            SceneManager.LoadScene(1);
        }

        if(lifeController.CurrentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void AddScore()
    {
        totalScore += scoreIncrease;
        scoreText.text = $"{totalScore}/{filesQuantity}";
        
    }
}
