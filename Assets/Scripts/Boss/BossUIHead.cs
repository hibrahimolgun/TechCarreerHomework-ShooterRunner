using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class BossUIHead : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _bossHit;
    [SerializeField] private Canvas _bossCanvas;
    [SerializeField] private TextMeshProUGUI _bossDmgDoneText;
    private int _bossDmgDone;
    [SerializeField] private BulletProperties _bulletProperties;

    private void Start()
    {
        _bossCanvas.transform.localScale = Vector3.zero;
        _bossDmgDone = 0;
        _bossCanvas.enabled = false;
        _bossHit.Subscribe(OnBossHit);
    }

    private void OnBossHit()
    {
        if (_bossCanvas.enabled == false)
        {
            TakeDamage();
            _bossCanvas.enabled = true;
            _bossCanvas.transform.DOScale(0.1f, 0.1f);
            
        }
        else
        {
            if (_bossCanvas.transform.localScale.x < 1)
            {
                TakeDamage();
                _bossCanvas.transform.DOScale(_bossDmgDone*0.1f, 0.1f);
            }
            else
            {
                TakeDamage();
                var dmgsequence = DOTween.Sequence();
                dmgsequence.Append(_bossDmgDoneText.transform.DOScale(1.2f, 0.1f));
                dmgsequence.Append(_bossDmgDoneText.transform.DOScale(0.8f, 0.1f));
                dmgsequence.Append(_bossDmgDoneText.transform.DOScale(1f, 0.1f));
            }
        }
        
    }

    private void TakeDamage()
    {
        _bossDmgDone += _bulletProperties.damage;
        _bossDmgDoneText.text = _bossDmgDone.ToString();
    }

    private void OnDisable()
    {
        _bossHit.Unsubscribe(OnBossHit);
    }

}
