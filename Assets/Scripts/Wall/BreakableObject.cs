using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEditor.ProjectWindowCallback;
using UnityEngine.UI;


public class BreakableObject : MonoBehaviour
{
    //[SerializeField] protected ScriptableAudio audioEffect;
    [SerializeField] public Transform _wallScale;
    private int _wallHealth;
    [SerializeField] TextMeshProUGUI _wallHealthText;
    private AudioSource audioSource;
    private int _index;

    [SerializeField] public BulletProperties bulletProperties;

    [SerializeField] ScriptableEvent _wallDestroyed;
    [SerializeField] SOfloat _forwardSpeed;
    private int _initialIndex;
    [SerializeField] private BuffBehaviour _BuffPrefab;
    [SerializeField] private SOfloat _sectionLength;
    [SerializeField] private ScriptableEvent _buffPicked;
    
    //wall type 0 = firerate, 1 = damage
    [SerializeField] private Image dmgImage;
    [SerializeField] private Image firerateImage;
    private int _walltype;
    private bool _nearWallDead=false;
    [SerializeField] private ScriptableEvent _playerDead;

    public void SetIndex(int _newIndex)
    {
        _index = _newIndex;
        InitWall();
    }

    private void Awake()
    {
        dmgImage.enabled = false;
        firerateImage.enabled = false;
        audioSource = GetComponent<AudioSource>();
        _wallHealthText.text = "????";
        _buffPicked.Subscribe(SetWallHealth);
    }

    private void InitWall()
    {
        _initialIndex = _index;
        _wallDestroyed.Subscribe(OnWallDestroyed);
        SetWallHealth();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //audioEffect.Play(audioSource);

        if (other.GetComponent<BulletMovement>() != null)
        {
            TakeDamage(other.GetComponent<BulletMovement>().bulletProperties.damage);
            other.GetComponent<BulletMovement>().PlayOnHit();
        }

        if (other.GetComponent<PlayerController>() != null)
        {
            _playerDead.InvokeAction();
        }
    }

    private void TakeDamage(int amount)
    {
        if (_nearWallDead == true) return;

        DmgAnimation();
        _wallHealth -= amount;
        
        if (_wallHealth <= 0)
        {
            gameObject.SetActive(false);
            _wallDestroyed.InvokeAction();
            SpawnBuff();
        }
        else
        {
            _wallHealthText.text = _wallHealth.ToString();
        }
    }

    private void OnWallDestroyed()
    {
        if (_index == 0)
        {
            dmgImage.enabled = false;
            firerateImage.enabled = false;
            _nearWallDead = true;
        }
        if (gameObject.activeSelf == true)
        {
            _index -= 1;
        }        
    }

    private void SetWallHealth()
    {
        if (_index == 0)
        {
            var dps = bulletProperties.damage * (_sectionLength.value*0.8f / (bulletProperties.fireRate*_forwardSpeed.value));
            _wallHealth = (int) (dps * Random.Range(0.6f, 0.9f));
            SetWallType();
            _wallHealthText.text = _wallHealth.ToString();
        }
    }

    private void OnDisable()
    {
        _wallDestroyed.Unsubscribe(OnWallDestroyed);
        _buffPicked.Unsubscribe(SetWallHealth);
    }

    private void SpawnBuff()
    {
        BuffBehaviour buff = Instantiate(_BuffPrefab, transform.position, Quaternion.identity);
        SetBuffType(buff, _walltype);
    }

//0 = firerate, 1 = damage
    private void SetBuffType(BuffBehaviour buff, int buffType)
    {
        switch (_walltype)
        {
            case 0:
                buff.FireRate();
                break;
            case 1:
                buff.Damage();
                break;
        }
    }

    private void SetWallType()
    {
        var _random = Random.Range(0, 2);
        _walltype = _random;
        if (_walltype == 0)
        {
            firerateImage.enabled = true;
        }
        else
        {
            dmgImage.enabled = true;
        }
    }
    private void DmgAnimation()
    {
        var dmgsequence = DOTween.Sequence();
        dmgsequence.Append(transform.DOScale(1.1f, 0.1f));
        dmgsequence.Append(transform.DOScale(0.9f, 0.1f));
        dmgsequence.Append(transform.DOScale(1f, 0.1f));        
    }
}