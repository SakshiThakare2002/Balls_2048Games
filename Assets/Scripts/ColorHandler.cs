using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public static ColorHandler Instance;

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

    public Color GetColorForValue(int value)
    {
        switch (value)
        {
            case 2:
                return Color.red;

            case 4:
                return new Color(1f, 0.5f, 0f); // Orange

            case 8:
                return Color.green;

            case 16:
                return new Color(135 / 255f, 206 / 255f, 235 / 255f); // Sky Blue

            case 32:
                return Color.blue;

            case 64:
                return Color.cyan;

            case 128:
                return Color.magenta;

            case 256:
                return new Color(165 / 255f, 42 / 255f, 42 / 255f); // Brown

            case 512:
                return Color.yellow;

            case 1024:
                return Color.white;

            case 2048:
                return Color.black;

            default:
                return Color.gray; // Default color for undefined values

        }

    }
}
