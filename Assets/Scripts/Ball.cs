using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    private int value;
    private bool hasMerged = false;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    
    public void SetValue(int value)
    {
        this.value = value;
        UpdateBallAppearence();
        UpdateScale();
    }

    public void UpdateBallAppearence()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = ColorHandler.Instance.GetColorForValue(value);

            if (textMeshPro != null)
            {
                textMeshPro.text = value.ToString();
            }
        }
    }

    private void UpdateScale()
    {
        float sizeFactor = GetSizeFactor(value);
        transform.localScale = Vector3.one * sizeFactor;
    }

    private float GetSizeFactor(int value)
    {
        float minSize = 0.7f;
        float maxSize = 2.0f;

        float sizeFactor = Mathf.Log(value, 2) * 0.4f;

        sizeFactor = Mathf.Clamp(sizeFactor, minSize, maxSize);
        return sizeFactor;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hasMerged) return;

        Ball otherBall = collision.gameObject.GetComponent<Ball>();

        if (otherBall == null) return;

        if (otherBall != null)
        {
            if (otherBall.hasMerged || otherBall.value != this.value) return;
        }

        if (this.GetInstanceID() < otherBall.GetInstanceID())
        {
            HandleMerge(otherBall, collision.contacts[0].point);
        }
    }

    private void HandleMerge(Ball otherBall, Vector3 contactPointPosition)
    {
        this.hasMerged = true;
        otherBall.hasMerged = true;

        int newValue = this.value * 2;

        if (GameManager.Instance != null)
        {
            Debug.Log("GameManager instance is not null");
            GameManager.Instance.AddScore(this.value);
        }
        

        Vector3 spawnPosition = transform.position;
        BallSpawner.Instance.SpawnMergedBall(newValue, contactPointPosition);

        if (BallSpawner.Instance.GetActiveBall() == this.gameObject || BallSpawner.Instance.GetActiveBall() == otherBall.gameObject)
        {
            BallSpawner.Instance.ClearActiveBall();
        }

        Destroy(this.gameObject);
        Destroy(otherBall.gameObject);
    }



}