using UnityEngine;
using UnityEngine.AI;

public class FindPath : MonoBehaviour
{
    /// <summary>パスの最大登録数</summary>
    const int _maxPositions = 5;
    /// <summary>pathの配列のプロパティ</summary>
    public Vector3[] Positions => _positions;
    /// <summary>開始地点</summary>
    [SerializeField, Header("Start")]
    private Transform _startPos;
    /// <summary>到達地点</summary>
    [SerializeField, Header("End")]
    private Transform _endPos;
    /// <summary>開始地点から到達地点までの座標の配列</summary>
    private Vector3[] _positions = new Vector3[_maxPositions];
    /// <summary>パス</summary>
    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();
    }
    private void Start()
    {
        //Pathの計算
        NavMesh.CalculatePath(_startPos.position, _endPos.position, NavMesh.AllAreas, _path);
        _path.GetCornersNonAlloc(_positions);
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _positions.Length - 1; i++)
        {
            Gizmos.DrawLine(_positions[i], _positions[i + 1]);
        }
    }
}
