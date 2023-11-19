using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button _resButton;
    [SerializeField] private ScriptableEvent _playerDead;

    private void Start()
    {
        _playerDead.Subscribe(OnPlayerDead);
        _resButton.interactable = false;
        _resButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _resButton.onClick.AddListener(ResetScene);
    }

    private void OnDisable()
    {
        _resButton.onClick.RemoveListener(ResetScene);
    }

    private void OnPlayerDead()
    {
        _resButton.gameObject.SetActive(true);
        _resButton.interactable = true;
    }

    private void ResetScene()
    {
        _playerDead.Unsubscribe(OnPlayerDead);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
