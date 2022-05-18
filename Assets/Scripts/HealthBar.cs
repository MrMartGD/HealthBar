using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;
    [SerializeField] private float _delta = 0.01f;
    [SerializeField] private float _delay = 0.01f;
    
    private Coroutine _increase;
    private Coroutine _decrease;
    private float _targetValue;

    public void Increase(float value) 
    {
        TryStopCoroutine(ref _increase);
        TryStopCoroutine(ref _decrease);

        _targetValue = Mathf.Clamp(_targetValue + value, 0, _slider.maxValue);
        _increase = StartCoroutine(ChangeValue());
    }

    public void Decrease(float value) 
    {
        TryStopCoroutine(ref _increase);
        TryStopCoroutine(ref _decrease);

        _targetValue = Mathf.Clamp(_targetValue - value, 0, _slider.maxValue);
        _decrease = StartCoroutine(ChangeValue());
    }
        
    private void Start() 
    {
        _slider.maxValue = _player.HealthMax;
        _slider.value = _slider.maxValue;
        _targetValue = _slider.value;
    }

    private IEnumerator ChangeValue() 
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (_slider.value != _targetValue) 
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _delta);
            yield return waitForSeconds;
        }
    }

    private void TryStopCoroutine(ref Coroutine coroutine) 
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        else 
        {
            return;
        }
    }
}
