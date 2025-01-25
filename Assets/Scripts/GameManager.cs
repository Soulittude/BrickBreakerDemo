using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int lvl)
    {
        level = lvl;

        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    public void Hit(Brick brick)
    {
        score += brick.points;

        if(LevelCleared())
            LoadLevel(level+1);
    }

    private bool LevelCleared()
    {
        for(int i=0;i<bricks.Length;i++)
        {
            if(bricks[i].gameObject.activeInHierarchy && bricks[i].breakable)
                return false;
        }

        return true;
    }

    private void ResetLevel()
    {
        ball.ResetBall();
        paddle.ResetPaddle();

        // for (int i = 0; i < bricks.Length; i++)
        // {
        //     bricks[i].ResetBrick();
        // }
    }

    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");

        NewGame();
    }

    public void Fall()
    {
        lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
            GameOver();
    }
}