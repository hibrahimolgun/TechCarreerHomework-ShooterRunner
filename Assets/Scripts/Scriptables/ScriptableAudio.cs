using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAudio", menuName = "ScriptableObjects/ScriptableAudio", order = 1)]
public class ScriptableAudio : ScriptableObject
{
    public AudioClip[] clips;

    [Range(0f, 1f)]
    [SerializeField] public RangedFloat volume;

    [Range(0f, 1f)]
    [SerializeField] public RangedFloat pitch;

    public void Play(AudioSource audioSource)
    {
        if (clips.Length == 0) return;

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.volume = Random.Range(volume.min, volume.max);
        audioSource.pitch = Random.Range(pitch.min, pitch.max);
        audioSource.Play();
    }
}