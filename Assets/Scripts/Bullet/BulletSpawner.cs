using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private SOfloat _forwardSpeed;


    [Header("Bullet Properties")]
    [SerializeField] private BulletMovement _bulletPrefab;
    public ObjectPool<BulletMovement> bullet_pool;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Transform  _bulletParent;

    [SerializeField] public BulletProperties bulletProperties;
    private bool _isDead = false;
    //[SerializeField] private Animator animator;
    //[SerializeField] private SOfloat _bulletSpawnRate;
    //[SerializeField] private SOfloat _bulletSpeed;

    //private float _bulletLifetime = 3.0f;
    //private readonly float _bulletOffset=0.5f; //offset from the spawnpoint
    //private Vector3 offset;
    


    private void Awake()
    {
        bullet_pool = new ObjectPool<BulletMovement>(_bulletPrefab, 50, _bulletParent);
    }

    private void Start()
    {
        FireGun();
    }

    private void Update()
    {
        /* if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(SpawnBullets());
        } */
    }
    
    private void FireGun()
    {
        StartCoroutine(SpawnBullets());
    }

    private void Shoot()
    {
        var offset = new Vector3(0, Random.Range(0f,1f), Random.Range(0f,1f)) * bulletProperties.offset;
        BulletMovement bullet = bullet_pool.Get();
        bullet.transform.SetPositionAndRotation(new Vector3(_bulletSpawnPoint.position.x, _bulletSpawnPoint.position.y + offset.y, _bulletSpawnPoint.position.z + offset.z), _bulletSpawnPoint.rotation);
        bullet.gameObject.SetActive(true);
        //bullet.SetSpeed(_forwardSpeed.value + bulletProperties.speed);
        bullet.SetLifetime(bulletProperties.lifeTime, bullet_pool);
    }

    private IEnumerator SpawnBullets()
    {
        while (_isDead == false)
        {
            Shoot();
            yield return new WaitForSeconds(bulletProperties.fireRate);
        }
    }

    private void OnDisable()
    {
        _isDead = true;
    }
}
