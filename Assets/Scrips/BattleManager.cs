using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<Fighter> _fighters = new List<Fighter>();
    [SerializeField]
    private int _fightersNeededToStart = 2;
    [SerializeField]
    private UnityEvent _onBattleStarted;
    [SerializeField]
    private UnityEvent _onBattleEnd;
    private Coroutine _battleCoroutine;

    public void AddFighter (Fighter fighter)
    {
        _fighters.Add(fighter);
        if (_fighters.Count >= _fightersNeededToStart)
        {
            StartBattle();
        }
    }

    public void RemoverFighter(Fighter fighter)
    {
        _fighters.Remove(fighter);
        if (_battleCoroutine != null)
        {
            StopCoroutine(_battleCoroutine);
        }
    }

    private void InitializeFighters()
    {
        foreach (var fighter in _fighters)
        {
            fighter.InitializeFighter();
        }

    }

    public void StartBattle()
    {
        InitializeFighters();
        _battleCoroutine = StartCoroutine(BattleCoroutine());

    }

    private IEnumerator BattleCoroutine()
    {
        _onBattleStarted?.Invoke();
        while (_fighters.Count > 1)
        {
            Fighter attacker = _fighters[Random.Range(0, _fighters.Count)];
            Fighter defender = _fighters[Random.Range(0, _fighters.Count)];
            while (defender == attacker)
            {
                defender = _fighters[Random.Range(0, _fighters.Count)];
            }
            attacker.transform.LookAt(defender.transform);
            defender.transform.LookAt(attacker.transform);
            Attack attack = attacker.AttackData.attacks[Random.Range(0, attacker.AttackData.attacks.Length)];
            float damage = Random.Range(attack.minDamage, attack.maxDamage);
            attacker.CharacterAnimator.Play(attack.animationName);
            SoundManager.instance.Play(attack.soundName);
            yield return new WaitForSeconds(attack.attackDuration);
            defender.Health.TakeDamage(damage);
            if (defender.Health.CurrentHealth <= 0)
            {
                RemoverFighter(defender);
            }
            yield return new WaitForSeconds(2f);
        }
        _onBattleEnd?.Invoke();
    }
}
