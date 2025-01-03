using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasViewportAdjuster : MonoBehaviour
{
    public Transform upperWall;
    public Transform leftWall;
    public Transform rightWall;
    public Transform baseWall;

    private void Start()
    {
        SetWalls(upperWall, leftWall, rightWall,baseWall);
    }

    public void SetWalls(Transform wall1, Transform wall2, Transform wall3, Transform wall4)
    {
        wall1.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.48f, 0.713f,0.3f));
        wall2.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.4f,0.3f));
        wall3.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.4f,0.3f));
        wall4.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.4991f, 0.09f, 0.3f));
    }
}

