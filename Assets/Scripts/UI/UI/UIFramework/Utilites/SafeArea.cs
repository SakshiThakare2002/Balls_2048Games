using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.StarterKit.Utilities
{
	public class SafeArea : MonoBehaviour
	{
		private RectTransform rectTransform;
		private Rect safeArea;
		private Vector2 minAnchor, maxAnchor;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			safeArea = Screen.safeArea;
			minAnchor = safeArea.position;
			maxAnchor = minAnchor + safeArea.size;

			minAnchor.x /= Screen.width;
			minAnchor.y /= Screen.height;

			maxAnchor.x /= Screen.width;
			maxAnchor.y /= Screen.height;

			rectTransform.anchorMin = minAnchor;
			rectTransform.anchorMax = maxAnchor;
		}
	}
}

