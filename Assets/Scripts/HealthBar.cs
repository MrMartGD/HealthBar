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
    
    private Coroutine _coroutine;
            
    private void Start() 
    {
        _slider.maxValue = _player.HealthMax;
        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        _player.HealthChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= ChangeValue;
    }

    private void ChangeValue() 
    {
        TryStopCoroutine(ref _coroutine);
        
        _coroutine = StartCoroutine(TargetValueSetter());
    }

    private IEnumerator TargetValueSetter() 
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (_slider.value != _player.HealthCurrent) 
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _player.HealthCurrent, _delta);
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
