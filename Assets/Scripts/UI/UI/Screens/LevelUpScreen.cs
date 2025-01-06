using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yudiz.StarterKit.UI
{
    public class LevelUpScreen : Screen
    {
        [SerializeField]private Button ContinueButton;

        public override void Show()
        {
            base.Show();
            ContinueButton.onClick.AddListener(OnContinueButton);
        }

        public void OnContinueButton()
        {
            UIManager.Instance.HideScreen(ScreenName.LevelUpScreen);
            UIManager.Instance.ShowScreen(ScreenName.MainScreen);
            GameStateManager.Instance.ChangeGameState(GameState.Gameplay);
        }

        public override void Hide()
        {
            base.Hide();
            ContinueButton.onClick.RemoveListener(OnContinueButton);
        }

    }
}
