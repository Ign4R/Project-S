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
    private bool starTimer;
    [SerializeField] private GameObject timerText;
    [SerializeField] private float timeValue = 90;
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
        if(starTimer == true)
        {

            if (timeValue > 0)
            { 
                timeValue -= Time.deltaTime;
            }

            if(timeValue <= 30)
            {
                timerText.GetComponent<Text>().color = Color.red;
            }

            if(timeValue <= 0)
            {
                timeValue = 0;
                SceneManager.LoadScene(2);
            }

            DisplayTime(timeValue);
        }

        if(totalScore == filesQuantity)
        {
            starTimer = true;
            timerText.SetActive(true);
        }

        if(lifeController.CurrentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore()
    {
        totalScore += scoreIncrease;
        scoreText.text = $"{totalScore}/{filesQuantity}";
        
    }
}
