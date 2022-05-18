using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent _healthChanged;

    private Animator _animator;
    private float _healthMax = 100;
    private float _healthCurrent;
    private float _delay = 1.5f;

    public event UnityAction HealthChanged 
    {
        add => _healthChanged.AddListener(value);
        remove => _healthChanged.RemoveListener(value);
    }

    public float HealthMax => _healthMax;
    public float HealthCurrent => _healthCurrent;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
                
        _healthCurrent = _healthMax;
    }

    public void TakeDamage(float damage) 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.Hit);

        _healthCurrent = Mathf.Clamp(_healthCurrent - damage, 0, _healthMax);
        _healthChanged?.Invoke();

        if (_healthCurrent == 0) 
        {
            StartCoroutine(Die());
        }
    }

    public void Heal(float value) 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.HealthUp);
        _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0, _healthMax);
        _healthChanged?.Invoke();        
    }

    private IEnumerator Die() 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.Dying);
        yield return new WaitForSeconds(_delay);
        
        gameObject.SetActive(false);
    }    
}
