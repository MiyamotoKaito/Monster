using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// GOAPのアクションクラス
/// </summary>
public abstract class GAction : MonoBehaviour
{
    // 動作名
    private string actionName = "Action";
    // コスト
    public float cost = 1f;
    // ターゲットオブジェクト
    public GameObject _target;
    // ターゲットのタグ
    public GameObject _targetTag;
    // 動作の持続時間
    public float _duration = 0f;
    // 前提条件
    public WorldState[] _preConditions;
    // 効果
    public WorldState[] _afterEffects;
    //NavMeshAgentコンポーネントへの参照
    public NavMeshAgent _agent;
    //前提条件の辞書
    public Dictionary<string, int> preConditions;
    //効果の辞書
    public Dictionary<string, int> effects;
    //エージェントの信念
    public WorldState agentBeliefs;
    //動作が実行中かどうかのフラグ
    private bool _running = false;
    //コンストラクタ
    public GAction()
    {
        preConditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }
    private void Awake()
    {
        //前提条件を辞書に登録する
        foreach (WorldState w in _preConditions)
        {
            preConditions.Add(w.Key, w.Value);
        }
        //効果を辞書に登録する
        foreach (WorldState w in _afterEffects)
        {
            effects.Add(w.Key, w.Value);
        }
        //NavMeshAgentコンポーネントを取得する
        _agent = GetComponent<NavMeshAgent>();
    }
    //動作が達成可能かどうかをチェックする
    public bool IsAchievable()
    {
        return true;
    }
    //前提条件が与えられた条件で達成可能かどうかをチェックする
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        //前提条件をチェックする
        foreach (KeyValuePair<string, int>pair in preConditions)
        {
            if(!conditions.ContainsKey(pair.Key))
            {
                return false;
            }
        }
        return true;
    }
    //動作の前処理
    public abstract bool PrePerform();
    //動作の後処理
    public abstract bool PostPerform();
}