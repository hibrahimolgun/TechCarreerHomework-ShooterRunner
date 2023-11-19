using UnityEngine;

public class Joystick2D : MonoBehaviour
{
    [SerializeField] private SOVector3 _strength;
    
    private Vector3 _initialClickPosition;
    private Vector3 _currentClickPosition;
    [SerializeField] private float _joystickSize;
    private float _fixY;

    [SerializeField] private GameObject _initialClickVisual;
    [SerializeField] private GameObject _currentClickVisual;
    [SerializeField] private RectTransform _initRect;
    [SerializeField] private RectTransform _currentRect;

    private void Update()
    {
        JoyStrength();
        JoyVisual();
    }

    private void JoyStrength()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialClickPosition = Input.mousePosition;
            _fixY = _initialClickPosition.y; // fix Y position of the joystick
        }
        if (Input.GetMouseButton(0))
        {
            _currentClickPosition = Input.mousePosition;
            _strength.value = Vector3.ClampMagnitude(_currentClickPosition - _initialClickPosition, _joystickSize);
            _strength.value.y = 0f  ;
            _strength.value.z = 0f; // fix Y position of the joystick
            _strength.value /= _joystickSize; // normalize
        }
        if (Input.GetMouseButtonUp(0))
        {
            _strength.value = Vector3.zero;
        }
    }

    private void JoyVisual()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initRect.position = new Vector3(_initialClickPosition.x, _fixY, 0);
            _currentRect.position = _initRect.position;
            _initialClickVisual.SetActive(true);
            _currentClickVisual.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            _currentRect.position = _initRect.position + Vector3.ClampMagnitude(_currentClickPosition - _initialClickPosition, _joystickSize);
            _currentRect.position = new Vector3(_currentRect.position.x, _fixY, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _initialClickVisual.SetActive(false);
            _currentClickVisual.SetActive(false);
        }
    }
}