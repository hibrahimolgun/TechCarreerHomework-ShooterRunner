using DG.Tweening;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private SOfloat _maxHealth;
    [SerializeField] private SOfloat _currentHealth;
    [SerializeField] private ScriptableEvent _dmgDone;
    private Collider _collider;
    [SerializeField] private ParticleSystem _bossDead;
    [SerializeField] private ScriptableEvent _playerDead;
    [SerializeField] private ScriptableEvent _bossDeadEvent;
    [SerializeField] private ScriptableEvent _youWinEvent;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _currentHealth.value = _maxHealth.value;
    }

    private void OnEnable()
    {
        _bossDeadEvent.Subscribe(OnBossDead);
    }

    private void OnDisable()
    {
        _bossDeadEvent.Unsubscribe(OnBossDead);
    }

    private void OnBossDead()
    {
        _bossDead.Play();
        _youWinEvent.InvokeAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletMovement>() != null)
        {
            TakeDamage(other.GetComponent<BulletMovement>().bulletProperties.damage);
            other.GetComponent<BulletMovement>().PlayOnHit();
        }
    }

    private void TakeDamage(int amount)
    {
        DmgAnimation();
        
        if (_currentHealth.value <= 0)
        {
            _bossDeadEvent.InvokeAction();
        }
        else
        {
            _currentHealth.value -= amount;
            _dmgDone.InvokeAction();
        }
    }


    private void DmgAnimation()
    {
        var dmgsequence = DOTween.Sequence();
        dmgsequence.Append(transform.DOScale(1.05f, 0.1f));
        dmgsequence.Append(transform.DOScale(0.95f, 0.1f));
        dmgsequence.Append(transform.DOScale(1f, 0.1f));      
    }
}
