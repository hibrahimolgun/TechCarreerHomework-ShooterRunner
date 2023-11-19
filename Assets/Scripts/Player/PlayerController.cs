using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SOVector3 _joyStrength;
    [SerializeField] private SOfloat _forwardSpeed;
    [SerializeField] private float _sidewaysSpeed;
    private Animator _animator;
    private int _canMove;
    [SerializeField] private float _zBoundry;
    [SerializeField] private ScriptableEvent _playerDead;
    [SerializeField] private ScriptableEvent _winEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
       
    }
    private void Start()
    {
        _animator.SetBool("DontMove", false);
        _animator.SetBool("IsDead", false);
        _canMove = 1;
    }
    private void OnEnable()
    {
        _winEvent.Subscribe(Dance);
        _playerDead.Subscribe(HitWall);
    }

    private void Update()
    {
        MovementCalculator();
    }

    private void MovementCalculator()
    {
        transform.position += new Vector3(_forwardSpeed.value, 0, -1 * _joyStrength.value.x * _sidewaysSpeed * _canMove) * Time.deltaTime;
        var pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, -_zBoundry, _zBoundry);
        transform.position = pos;
    }

    public void HitWall()
    {
        _forwardSpeed.value = 0;
        _canMove = 0;
        //DOTWEEN HIT WALL
        _animator.SetBool("IsDead", true);
        gameObject.GetComponent<BulletSpawner>().enabled = false;
    }

    public void DontMove()
    {
        _canMove = 0;
        _animator.SetBool("DontMove", true);
    }

    private void OnDisable()
    {
        _winEvent.Unsubscribe(Dance);
        _playerDead.Unsubscribe(HitWall);
    }

    private void Dance()
    {
        gameObject.GetComponent<BulletSpawner>().enabled = false;
        _animator.SetBool("Dance", true);
    }
}
