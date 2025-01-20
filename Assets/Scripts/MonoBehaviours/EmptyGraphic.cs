using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The EmptyGraphic class is an optimized alternative to using a transparent Image component for raycasting purposes.
/// This class inherits from the Unity Graphic class and overrides the OnPopulateMesh method to clear the VertexHelper,
/// effectively rendering nothing.
///
/// By attaching this script to a GameObject, you can make the object a raycast target without the overhead associated
/// with rendering an actual image. This is particularly useful for empty or invisible objects that still need to
/// respond to user interactions.
///
/// This class requires a CanvasRenderer component to be attached to the same GameObject.
/// </summary>
[RequireComponent(typeof(CanvasRenderer))]
public class EmptyGraphic : Graphic
{
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}
}
