using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BuffBehaviour : MonoBehaviour
{
    [SerializeField] private WallDropChance _wallDropChance;
    [SerializeField] public BulletProperties bulletProperties;
    //[SerializeField] private SOfloat _bulletSpeed;
    //[SerializeField] private SOfloat _fireRate;
    //[SerializeField] private SOint _bulletDamage;
    //private float _deltaSpeed=0f;
    private float _deltaFireRate=0f;
    private int _deltaDamage=0;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _buffMat;
    [SerializeField] private Material _debuffMat;
    [SerializeField] private ScriptableEvent _buffPicked;
    [SerializeField] private float maxRate = 0.1f;

    /* public void Speed()
    {
        var _random = Random.Range(0f, 1f);
        if (_random < _wallDropChance._buffChance)
        {
            var value = _bulletSpeed.value;
            _deltaSpeed = value * Random.Range(0.2f, 0.4f);
            _renderer.material = _buffMat;
        }
        else
        {
            var value = _bulletSpeed.value;
            _deltaSpeed = value * Random.Range(-0.05f, -0.1f);
            _renderer.material = _debuffMat;
        }
    } */

    public void FireRate()
    {
        if (bulletProperties.fireRate <= maxRate) return;

        var _random = Random.Range(0f, 1f);
        var oldRate = bulletProperties.fireRate;
        var numBulletSec = 1 / oldRate;
        if (_random < _wallDropChance._buffChance)
        {
            _deltaFireRate = (1 / (numBulletSec + 1)) - oldRate;
            _renderer.material = _buffMat;
        }
        else
        {
            if (numBulletSec > 1)
            {
                _deltaFireRate = (1 / (numBulletSec - 1)) - oldRate;
                _renderer.material = _debuffMat;
            }
        }
    }
    
    public void Damage()
    {
        var _random = Random.Range(0f, 1f);
        if (_random < _wallDropChance._buffChance)
        {
            _deltaDamage = 1;
            _renderer.material = _buffMat;
        }
        else
        {
            if (bulletProperties.damage > 1)
            {
                _deltaDamage = -1;
            }
            _renderer.material = _debuffMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            //_bulletSpeed.value += _deltaSpeed;
            bulletProperties.fireRate += _deltaFireRate;
            bulletProperties.damage += _deltaDamage;
            _buffPicked.InvokeAction();
            gameObject.SetActive(false);
        }
    }

}
