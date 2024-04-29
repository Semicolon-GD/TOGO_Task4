using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    public static event Action GameOver;

    public static event Action GameWon;
    
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    
    public static int Score { get; private set; }

    private void Start()
    {
        Score=0;
    }

    private void OnEnable()
    {
        ObstacleController.CollectibleCollected += Add;
        ObstacleController.MinusPoint += LosePoint;
        OnScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        ObstacleController.CollectibleCollected -= Add;
        ObstacleController.MinusPoint -= LosePoint;
        OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
        if (score>0)
        {
            scoreText.color = Color.green;
        }
        else
        {
            scoreText.color = Color.red;
        }
       
    }

    private static void Add(GameObject other)
    {
        Score++;
        OnScoreChanged?.Invoke(Score);
    }

    private static void LosePoint(int point)
    {
        if(Score==0)
            GameOver?.Invoke();
        else
        {
            switch (point)
            {
                case 1:
                    Score--;
                    break;
                case 0:
                    GameOver?.Invoke();
                    break;
                default:
                    Score = 0;
                    break;
            }

            OnScoreChanged?.Invoke(Score);
        }
    }

    public static void CalculateResult()
    {
        switch (Score)
        {
            case 0:
                GameOver?.Invoke();
                break;
            case <0:
                GameOver?.Invoke();
                break;
            default:
                GameWon?.Invoke();
                break;
        }
    }
    
}
