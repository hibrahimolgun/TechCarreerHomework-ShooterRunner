using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dmgText;
    [SerializeField] private TextMeshProUGUI _fireRateText;

    [SerializeField] private BulletProperties _bulletProperties;
    [SerializeField] private ScriptableEvent _buffPicked;

    private void Start()
    {
        _dmgText.text = _bulletProperties.damage.ToString();
        _fireRateText.text = GetArrows().ToString();
        _buffPicked.Subscribe(UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        _dmgText.text = _bulletProperties.damage.ToString();
        _fireRateText.text = GetArrows().ToString();
    }

    private int GetArrows()
    {
        return (int) (1/_bulletProperties.fireRate);
    }

}
