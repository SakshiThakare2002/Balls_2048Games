using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Yudiz.StarterKit.UI
{
    public class GameOverScreen : Screen
    {
        [SerializeField] private Button HomeButton;
        [SerializeField] private Button RetryButton;

        public override void Show()
        {
            base.Show();
            HomeButton.onClick.AddListener(OnHomeButtonPress);
            RetryButton.onClick.AddListener(OnRetryButtonPress);
        }

        public void OnHomeButtonPress()
        {
            //GameManager.Instance.ResetScore();
            SceneManager.LoadScene(0);
          
            UIManager.Instance.ShowScreen(ScreenName.StartScreen);
            GameStateManager.Instance.ChangeGameState(GameState.MainMenu);
        }

        public void OnRetryButtonPress()
        {
            GameManager.Instance.ResetScore();
            BallSpawner.Instance.SpawnNewBall();

            UIManager.Instance.ShowScreen(ScreenName.MainScreen);
            GameStateManager.Instance.ChangeGameState(GameState.Gameplay);
        }

        public override void Hide()
        {
            base.Hide();
            HomeButton.onClick.RemoveListener(OnHomeButtonPress);
            RetryButton.onClick.RemoveListener(OnRetryButtonPress);
        }

    }
}
