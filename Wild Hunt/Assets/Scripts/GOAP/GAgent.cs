using System.Collections.Generic;
using UnityEngine;

public class SubGoal
{
    // サブゴールの条件
    public Dictionary<string, int> SubGoals;

    // サブゴールが削除されるかどうかのフラグ
    // 一部のエージェントがユーザーに目標を与え、その目標が達成された時削除する場合に使用
    public bool Remove;

    //変数を初期化するコンストラクタ
    public SubGoal(Dictionary<string, int> goals, bool remove)
    {
        SubGoals = new Dictionary<string, int>(goals);
        Remove = remove;
    }
}

public class GAgent : MonoBehaviour
{
    //実行するアクションのリスト
    private List<GAction> _gActions = new List<GAction>();
    //サブゴールの辞書
    private Dictionary<SubGoal, int> _subGoals = new Dictionary<SubGoal, int>();

    //private GPlanner _gPlanner;
    private Queue<GAction> _actionQueue;
    private GAction _currentAction;
    private SubGoal _currentGoal;
    private void Awake()
    {
        //エージェントのアクションをすべて取得する
        GAction[] actions = GetComponents<GAction>();
        foreach (GAction a in actions)
        {
            _gActions.Add(a);
        }
    }
    private void LateUpdate()
    {
        
    }
}
