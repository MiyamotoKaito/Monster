using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class FindPath : MonoBehaviour
{
    /// <summary>パスの最大登録数</summary>
    const int _maxPositions = 9;
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
        NavMesh.CalculatePath(_startPos.localPosition, _endPos.localPosition, NavMesh.AllAreas, _path);
        _path.GetCornersNonAlloc(_positions);
    }
}
