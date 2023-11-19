using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] public BulletProperties bulletProperties;
    [SerializeField] private ParticleSystem _onHit;

    //[SerializeField] private ScriptableEvent _wallHit;

    /* private void OnEnable()
    {
        _wallHit.Subscribe(PlayOnHit);
    }

    private void OnDisable()
    {
        _wallHit.Unsubscribe(PlayOnHit);
    } */

    private void Update()
    {
        transform.Translate(bulletProperties.speed * Time.deltaTime * Vector3.right);
    }

    public void SetLifetime(float lifetime, ObjectPool<BulletMovement> pool)
    {
        StartCoroutine(DestroyAfterSeconds(lifetime, this, pool));
    }

    public IEnumerator DestroyAfterSeconds(float seconds, BulletMovement bullet, ObjectPool<BulletMovement> pool)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        pool.Return(bullet);
    }

    public void PlayOnHit()
    {
        StartCoroutine(OnHitDelay());
    }

    private IEnumerator OnHitDelay()
    {
        _onHit.Play();
        yield return new WaitForSeconds(_onHit.main.duration);
        gameObject.SetActive(false);
    }

}
