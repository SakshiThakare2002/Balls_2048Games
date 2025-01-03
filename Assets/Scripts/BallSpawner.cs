using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay = 0.8f;
    [SerializeField] private float fallSpeed = 300f;


    // [SerializeField] private GameObject gameOverUI; // Game Over UI panel
    public List<GameObject> spawnedBalls = new List<GameObject>();

    private GameObject activeBall = null;
    public bool isInput = false;
    public bool canRelease = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChange;
    }

    private void Start()
    {
        SpawnNewBall();
    }

    private void Update()
    {
        if (isInput)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (activeBall == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            canRelease = true;
        }

        if (Input.GetMouseButton(0))
        {
            MoveBallWithMouse();
        }

        if (Input.GetMouseButtonUp(0) && canRelease)
        {
            ReleaseBall();
        }
    }

    private void MoveBallWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 ballPosition = activeBall.transform.position;

        ballPosition.x = Mathf.Clamp(mousePosition.x, -3.2f, 3.2f);
        activeBall.transform.position = new Vector3(ballPosition.x, spawnPoint.position.y, spawnPoint.position.z);
    }

    private void ReleaseBall()
    {
        if (activeBall != null)
        {
            Rigidbody rb = activeBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
                activeBall.tag = "ReleasedBall";
                activeBall = null;

                if (isInput)
                {
                    StartCoroutine(SpawnBallAfterDelay());
                    canRelease = false;
                }
            }
        }
    }

    private IEnumerator SpawnBallAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        SpawnNewBall();
    }

    public void SpawnNewBall()
    {
        if (activeBall == null)
        {
            activeBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity); 
            Ball ballScript = activeBall.GetComponent<Ball>();
            spawnedBalls.Add(activeBall);
            int[] possibleValues = { 2, 4, 8, 16 };
            int randomValue = possibleValues[Random.Range(0, possibleValues.Length)];
            ballScript.SetValue(randomValue);
            activeBall.tag = "Ball";
        }
    }

    public GameObject GetActiveBall()
    {
        return activeBall;
    }

    public void ClearActiveBall()
    {
        activeBall = null;

        //Trigger spawning after the merge
        StartCoroutine(SpawnBallAfterDelay());
    }

    public void SpawnMergedBall(int value, Vector3 position)
    {
        GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
        Ball ballScript = newBall.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.SetValue(value);
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                newBall.tag = "ReleasedBall";
            }
            spawnedBalls.Add(newBall);
        }
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        isInput = false;
        ClearAllBalls();
        StopAllCoroutines();
    }

    private void OnGameStateChange(GameState gameState)
    {
        if (gameState == GameState.Gameplay)
        {
            isInput = true;
        }
        else
        {
            isInput = false;
        }
    }

    public void ClearAllBalls()
    {
        Debug.Log("balls get destroy");
        foreach (var ball in spawnedBalls)
        {
            if (ball != null)
            {
                Destroy(ball);
            }
        }
        spawnedBalls.Clear();
        activeBall = null;
    }

}
