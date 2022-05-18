using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Animator _animator;
    private float _healthMax = 100;
    private float _healthCurrent;
    private float _delay = 1.5f;

    public float HealthMax => _healthMax;
    
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
                
        _healthCurrent = _healthMax;
    }

    private IEnumerator Die() 
    {
        _animator.SetTrigger(AnimatorControllerPlayer.States.Dying);
        yield return new WaitForSeconds(_delay);
        
        gameObject.SetActive(false);
    }    
}
