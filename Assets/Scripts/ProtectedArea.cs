using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
internal class ProtectedArea : MonoBehaviour
{
    private const float MinimumVolume = 0f;
    private const float MaximumVolume = 1f;

    private AudioSource _alarmSound;
    private float _volumeChangeSpeed = 0.33f;

    private Coroutine _changeVolumeProcess;

    private IEnumerator ChangeVolume(float targetVolume)
    {
        float intervalBetweenVolumeChanges = 0.05f;
        var delay = new WaitForSeconds(intervalBetweenVolumeChanges);

        while (_alarmSound.volume != targetVolume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return delay;
        }
    }

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider _)
    {
        StartChangingVolume(MaximumVolume);
    }

    private void OnTriggerExit(Collider _)
    {
        StartChangingVolume(MinimumVolume);
    }

    private void StartChangingVolume(float targetVolume)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeVolume(targetVolume));
    }
}
