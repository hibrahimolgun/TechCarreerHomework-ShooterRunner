using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _dmgDone;
    [SerializeField] private GameObject _timer;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private ScriptableEvent _playerDead;
    [SerializeField] private ScriptableEvent _bossDead;

    private void Awake()
    {
        _timer.SetActive(false);
    }

    private void OnEnable()
    {
        _dmgDone.Subscribe(StartTimer);
        _bossDead.Subscribe(StopTimer);
    }

    private void StartTimer()
    {
        _timer.SetActive(true);
        StartCoroutine(TimerCountdown());
        _dmgDone.Unsubscribe(StartTimer);
    }

    private IEnumerator TimerCountdown()
    {
        var time = 10;
        while (time >= 0)
        {
            if (time <= 3) 
            {
                LastSecondsAnimation();
                ChangeTextColor();
            }
            _timerText.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        _playerDead.InvokeAction();
        _bossDead.InvokeAction();
    }

    private void StopTimer()
    {
        StopAllCoroutines();
    }
    
    private void OnDisable()
    {
        _bossDead.Unsubscribe(StopTimer);
    }
    
    private void LastSecondsAnimation()
    {
        var dmgsequence = DOTween.Sequence();
        dmgsequence.Append(_timerText.transform.DOScale(1.3f, 0.1f));
        dmgsequence.Append(_timerText.transform.DOScale(1.1f, 0.1f));
        dmgsequence.Append(_timerText.transform.DOScale(1.2f, 0.1f));
    }

    private void ChangeTextColor()
    {
        _timerText.color = Color.red;
    }
    
}
