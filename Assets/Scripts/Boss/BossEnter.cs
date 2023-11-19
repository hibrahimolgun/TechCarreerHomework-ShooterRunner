using UnityEngine;

public class BossEnter : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private SOfloat _forwardSpeed;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            _forwardSpeed.value = 0;
            other.GetComponent<PlayerController>().DontMove();
        }
    }
}
