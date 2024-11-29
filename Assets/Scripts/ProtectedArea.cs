using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
internal class ProtectedArea : MonoBehaviour
{
    private const float TargetVolumeOfAlarmSignalIfPlayerInside = 1f;
    private const float TargetVolumeOfAlarmSignalIfPlayerOutside = 0f;

    private AudioSource _alarmSound;
    private float _targetVolumeOfAlarmSignal = TargetVolumeOfAlarmSignalIfPlayerOutside;
    private float _volumeChangeSpeed = 0.33f;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("IN");

        if (other.TryGetComponent<Player>(out _))
        {
            _targetVolumeOfAlarmSignal = TargetVolumeOfAlarmSignalIfPlayerInside;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OUT");

        if (other.TryGetComponent<Player>(out _))
        {
            _targetVolumeOfAlarmSignal = TargetVolumeOfAlarmSignalIfPlayerOutside;
        }
    }

    private void Update()
    {
        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _targetVolumeOfAlarmSignal, _volumeChangeSpeed * Time.deltaTime);
    }
}
