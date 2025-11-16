using UnityEngine;

public class AgentMove : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField, Header("Speed")]
    private float _speed;
    /// <summary>現在のポジションの配列の番号</summary>
    private int _currentPosIndex = 0;
}
