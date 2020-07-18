using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig _waveConfig;
    private float _movementSpeed = 2f;
    private float _rotationSpeed = 2f;
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;

    private void Start()
    {
        _waypoints = _waveConfig.GetPathPrefab();
        _movementSpeed = _waveConfig.GetMoveSpeed();
        _rotationSpeed = _waveConfig.GetRotationSpeed();

        transform.position = _waypoints[_waypointIndex++].transform.position;
        transform.LookAt(_waypoints[_waypointIndex].transform.position);
    }

    private void Update()
    {
        MoveAlongWayPoints();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        _waveConfig = waveConfig;
    }

    private void MoveAlongWayPoints()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), Time.deltaTime * _rotationSpeed);

            Debug.DrawLine(transform.position, targetPosition, Color.red);
            Debug.DrawRay(transform.position, targetPosition - transform.position, Color.green);

            if (transform.position == _waypoints[_waypointIndex].transform.position)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
