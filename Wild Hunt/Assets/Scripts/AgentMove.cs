using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class AgentMove : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField, Header("Speed")]
    private float _speed;
    /// <summary>目的地から止まるまでの距離</summary>
    [SerializeField, Header("StoppedDistance")]
    private float _distance;
    /// <summary>現在のポジションの配列の番号</summary>
    private int _currentPosIndex = 0;

    private FindPath _findPath;
    private void Awake()
    {
        _findPath = GetComponent<FindPath>();
    }

    private void Update()
    {
        var targetPos = _findPath.Positions[_currentPosIndex];
        if (Vector3.Distance(transform.position, targetPos) < _distance)
        {
            _currentPosIndex += _currentPosIndex + 1 < _findPath.Positions.Length - 1 ? 1 : 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
    }
}
