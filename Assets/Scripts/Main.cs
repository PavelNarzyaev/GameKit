using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _helloWorldText;

	private const int _framesForOneStep = 60;

	private AnimationStep _animationStep;
	private int _framesCounter;

	private enum AnimationStep
	{
		Hello,
		World,
	}

	private void Start()
	{
		RefreshText();
	}

	private void Update()
	{
		_framesCounter++;
		if (_framesCounter < _framesForOneStep) return;
		_framesCounter = 0;
		_animationStep = _animationStep == AnimationStep.Hello ? AnimationStep.World : AnimationStep.Hello;
		RefreshText();
	}

	private void RefreshText()
	{
		switch (_animationStep)
		{
			case AnimationStep.Hello:
				_helloWorldText.text = "Hello";
				break;
			case AnimationStep.World:
				_helloWorldText.text = "World!!!";
				break;
		}
	}
}
