using UnityEngine;

[CreateAssetMenu(fileName = "BulletProperties", menuName = "Scriptables/BulletProperties", order = 0)]
public class BulletProperties : ScriptableObject
{
    public float speed;
    public int damage;
    public float lifeTime;
    public float fireRate;
    public float offset;
}
