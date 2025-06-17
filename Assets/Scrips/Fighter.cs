using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField]
    private string _fighterName;
    public string fighterName => _fighterName;
    [SerializeField]
    private AttackData _attackData;
    public AttackData AttackData => _attackData;
    [SerializeField]
    private Healt _health;
    public Healt Health => _health;
    [SerializeField]
    private Animator _characterAnimator;
    public Animator CharacterAnimator => _characterAnimator;
}
