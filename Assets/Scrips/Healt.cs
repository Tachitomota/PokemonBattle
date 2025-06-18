using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Healt : MonoBehaviour
{
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private float _maxHealth = 100F;
    private float _currentHealth;
    public float  CurrentHealth => _currentHealth;
    [SerializeField]
    private UnityEvent<float> _onTakeDamage;
    [SerializeField]
    private UnityEvent _onDie;
    public void TakeDamage (float damage)
    {
        _currentHealth -= damage;
        _onTakeDamage.Invoke(damage);
        if (_currentHealth <= 0)
        {
            Die();
        }
        UpdateHealtSlider();
    }

    public void Die()
    {
        _currentHealth = 0;
        _onDie.Invoke();
    }

    public void InitializableHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealtSlider();
    }

    private void UpdateHealtSlider()
    {
        _healthSlider.value = _currentHealth / _maxHealth;
    }
}
