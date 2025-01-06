using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Yudiz.StarterKit.UI
{
    public class MainScreen : Screen
    {
        [SerializeField] private TMP_Text LevelText; 
        [SerializeField] private TMP_Text ScoreText;

        [SerializeField] private Button HomeButton;
        [SerializeField] private Button RetryButton;

        
        private IEnumerator WaitForGameManager()
        {
            while (GameManager.Instance == null)
            {
                yield return null; 
                Debug.LogWarning("GameManager not found. Creating dynamically.");
                GameObject gameManagerObj = new GameObject("GameManager");
                gameManagerObj.AddComponent<GameManager>();
            }

            GameManager.Instance.OnScoreUpdate += UpdateScoreAndLevel;

            UpdateScoreAndLevel(GameManager.Instance.Score, GameManager.Instance.Level);

            Debug.Log("Subscribed to OnScoreUpdate event.");
        }

        public override void Show()
        {
            base.Show();
            GameManager.Instance.OnScoreUpdate += UpdateScoreAndLevel;
            HomeButton.onClick.AddListener(OnHomeButtonPress);
            RetryButton.onClick.AddListener(OnRetryButtonPress);
            GameStateManager.Instance.ChangeGameState(GameState.Gameplay);
            UpdateScoreAndLevel(GameManager.Instance.Score, GameManager.Instance.Level);
        }

        public void OnHomeButtonPress()
        {
            SceneManager.LoadScene(0);
           
            UIManager.Instance.ShowScreen(ScreenName.StartScreen);
            GameStateManager.Instance.ChangeGameState(GameState.MainMenu);
        }

        public void OnRetryButtonPress()
        {
            BallSpawner.Instance.canRelease = false;
            
            GameManager.Instance.ResetScore();
            BallSpawner.Instance.ClearAllBalls();
            BallSpawner.Instance.SpawnNewBall();
            
            UIManager.Instance.ShowScreen(ScreenName.MainScreen);
            GameStateManager.Instance.ChangeGameState(GameState.Gameplay);
        }

        public override void Hide()
        {
            base.Hide();
            GameManager.Instance.OnScoreUpdate -= UpdateScoreAndLevel;
            HomeButton.onClick.RemoveListener(OnHomeButtonPress);
            RetryButton.onClick.RemoveListener(OnRetryButtonPress);
        }

        private void UpdateScoreAndLevel(int score, int level)
        {
            Debug.Log($"Updating UI - Score: {score}, Level: {level}");
            LevelText.text = $"{level}";
            ScoreText.text = $"{score}";
        }
    }
}
