using UnityEngine;

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
}
