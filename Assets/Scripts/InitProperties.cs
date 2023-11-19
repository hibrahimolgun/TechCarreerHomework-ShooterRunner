using UnityEngine;

public class InitProperties : MonoBehaviour
{
    [SerializeField] private BulletProperties _bulletProperties;
    [SerializeField] private SOfloat _forwardSpeed;
    [SerializeField] private BulletProperties initialBulletProperties;

    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        _forwardSpeed.value = 10f;
        _bulletProperties.damage = initialBulletProperties.damage;
        _bulletProperties.fireRate = initialBulletProperties.fireRate;
        _bulletProperties.speed = _forwardSpeed.value + initialBulletProperties.speed;
        _bulletProperties.lifeTime = initialBulletProperties.lifeTime;
        _bulletProperties.offset = initialBulletProperties.offset;
    }
    

    public void UpgradeSpeed()
    {
        initialBulletProperties.speed += 1f;
    }
    public void UpgradeDamage()
    {
        initialBulletProperties.damage += 1;
    }
    public void UpgradeFireRate()
    {
        var oldRate = initialBulletProperties.fireRate;
        var numBulletSec = 1 / oldRate;
        initialBulletProperties.fireRate = 1 / (numBulletSec + 1);
    }
}
