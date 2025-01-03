using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yudiz.StarterKit.UI
{
    public class StartScreen : Screen
    {
        [SerializeField] private Button startButton;
        public override void Show()
        {
            base.Show();
            GameStateManager.Instance.ChangeGameState(GameState.MainMenu);
            startButton.onClick.AddListener(OnStartButtonPress);
        }

        public void OnStartButtonPress()
        {
            UIManager.Instance.ShowScreen(ScreenName.MainScreen);
        }


        public override void Hide()
        {
            base.Hide();
            startButton.onClick.RemoveListener(OnStartButtonPress);
        }

    }
}