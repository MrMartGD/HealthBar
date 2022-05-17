using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _healthMax = 100;
    [SerializeField] private HealthBar _healthBar;

    private Animator _animator;
    private float _healthCurrent;
    private float _delay = 1.5f;
    
    public void TakeDamage(float damage) 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.Hit);

        _healthCurrent -= damage;
        _healthCurrent = Mathf.Clamp(_healthCurrent, 0, _healthMax);

        if (_healthCurrent == 0) 
        {
            StartCoroutine(Die());
        }
    }

    public void IncreaseHealth(float value) 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.HealthUp);

        _healthCurrent += value;
        _healthCurrent = Mathf.Clamp(_healthCurrent, 0, _healthMax);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        _healthBar.SetMaxHealth(_healthMax);
        _healthCurrent = _healthMax;
    }

    private void LateUpdate()
    {
        _healthBar.SetHealth(_healthCurrent);
    }

    private IEnumerator Die() 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.Dying);
        yield return new WaitForSeconds(_delay);
        
        gameObject.SetActive(false);
    }    
}
