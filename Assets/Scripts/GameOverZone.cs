using UnityEngine;
using Yudiz.StarterKit.UI;

public class GameOverZone : MonoBehaviour
{
    private float collisionTimer = 0f;
    private const float requiredCollisionTime = 1f;
    private bool isGameOverTriggered = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ReleasedBall") && !isGameOverTriggered)
        {
            collisionTimer += Time.deltaTime;
            Debug.Log(collisionTimer);
            if (collisionTimer >= requiredCollisionTime)
            {
                Debug.Log("GameOver triggered");
                BallSpawner.Instance.TriggerGameOver();
                isGameOverTriggered = true;
                UIManager.Instance.ShowScreen(ScreenName.GameOverScreen);
                GameStateManager.Instance.ChangeGameState(GameState.GameOver);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ReleasedBall"))
        {
            Debug.Log("Ball exited the zone before 2 seconds");

            collisionTimer = 0f;
            isGameOverTriggered = false;  
        }
    }
}


