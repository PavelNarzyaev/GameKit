using System;
using UnityEngine;

public class Layers : MonoBehaviour
{
	public RectTransform rectTransform;

	private void Start()
	{
		var layers = Enum.GetValues(typeof(LayerNames.Layer));

		foreach (var layer in layers)
		{
			var layerContainer = new GameObject(layer.ToString());

			layerContainer.transform.SetParent(rectTransform, false);

			var layerRectTransform = layerContainer.AddComponent<RectTransform>();
			layerRectTransform.anchorMin = Vector2.zero;
			layerRectTransform.anchorMax = Vector2.one;
			layerRectTransform.offsetMin = Vector2.zero;
			layerRectTransform.offsetMax = Vector2.zero;
		}
	}
}
