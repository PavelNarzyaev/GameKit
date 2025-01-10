using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
	[Header("Hello")]
	[SerializeField] private TextMeshProUGUI _helloWorldText;

	[Header("Clicks counter")]
	[SerializeField] private Button _clicksButton;
	[SerializeField] private TextMeshProUGUI _clicksCounterText;

	private const int _framesForOneStep = 60;

	private AnimationStep _animationStep;
	private int _framesCounter;
	private int _clicksCounter;

	private enum AnimationStep
	{
		Hello,
		World,
	}

	private void Awake()
	{
		_clicksButton.onClick.AddListener(ClicksButtonClickHandler);
	}

	private void Start()
	{
		RefreshHelloText();
		RefreshClicksCounterText();
	}

	private void Update()
	{
		_framesCounter++;
		if (_framesCounter < _framesForOneStep) return;
		_framesCounter = 0;
		_animationStep = _animationStep == AnimationStep.Hello ? AnimationStep.World : AnimationStep.Hello;
		RefreshHelloText();
	}

	private void ClicksButtonClickHandler()
	{
		_clicksCounter++;
		RefreshClicksCounterText();
	}

	private void RefreshHelloText()
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

	private void RefreshClicksCounterText()
	{
		_clicksCounterText.text = _clicksCounter.ToString(CultureInfo.InvariantCulture);
	}
}
