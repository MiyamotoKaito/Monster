using System.Collections.Generic;
using UnityEngine;
public class Node
{
    // ノードの親ノード
    public Node Parent;
    // ノードのコスト
    public float Cost;
    // ノードの状態
    public Dictionary<string, int> State;
    // ノードのアクション
    public GAction Action;

    // コンストラクタ
    public Node(Node parent, float cost, Dictionary<string, int> allState, GAction action)
    {
        Parent = parent;
        Cost = cost;
        State = allState;
        Action = action;
    }
}
/// <summary>
/// GOAPのプランナー
/// </summary>
public class GPlanner : MonoBehaviour
{
    /// <summary>
    /// 最短経路のアクションの道をプランニングした戻り値Queue
    /// </summary>
    /// <param name="actions"></param>
    /// <param name="goal"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Queue<GAction> Plan(List<GAction> actions,
                                 Dictionary<string, int> goal,
                                 WorldState state)
    {
        // 利用可能なアクションのリスト
        List<GAction> usebleActions = new List<GAction>();
        // 利用可能なアクションをフィルタリング
        foreach (GAction action in actions)
        {
            if (action.IsAchievable())
            {
                usebleActions.Add(action);
            }
        }
        // 葉ノードのリスト
        List<Node> leaves = new List<Node>();

        // 開始ノードの作成
        //最初のノードなので親ノードはnull、コストは0、
        //状態はエージェントの現在の状態、アクションはnull
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usebleActions, goal);
        if (success)
        {
            Debug.Log("NO Plan");
            return null;
        }

        // 最小コストの葉ノード
        Node cheapest = null;
        // 最小コストの葉ノードを見つける
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.Cost < cheapest.Cost)
                {
                    cheapest = leaf;
                }
            }
        }
        // プランのアクションリスト
        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        // ルートノードまでさかのぼりアクションを収集
        while (n != null)
        {
            if (n.Action != null)
            {
                result.Insert(0, n.Action);
            }
            n = n.Parent;
        }
        // アクションリストをキューに変換
        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in result)
        {
            queue.Enqueue(a);
        }
        Debug.Log($"The Plan is : ");
        foreach (GAction a in queue)
        {
            Debug.Log($"Q : {a.name}");
        }
        // プランを返す
        return queue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="leaves"></param>
    /// <param name="usebleActions"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    private bool BuildGraph(Node parent,
                            List<Node> leaves,
                            List<GAction> usebleActions,
                            Dictionary<string, int> goal)
    {
        // フラグ：パスが見つかったかどうか
        bool foundPath = false;
        // すべての利用可能なアクションをループ
        foreach (GAction action in usebleActions)
        {
            // アクションが親ノードの状態で実行可能かどうかをチェック
            if (action.IsAchievableGiven(parent.State))
            {
                // 新しい状態を作成
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.State);
                // アクションの効果を新しい状態に適用
                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    // 効果が既に存在する場合は更新、存在しない場合は追加
                    if (!currentState.ContainsKey(eff.Key))
                    {
                        // 効果が存在しない場合は追加
                        currentState.Add(eff.Key, eff.Value);
                    }
                }
                // 新しいノードを作成
                Node node = new Node(parent, parent.Cost + action.cost, currentState, action);
                // ゴールが達成されたかどうかをチェック
                if (GoalAchived(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                // ゴールが達成されていない場合、再帰的にグラフを構築
                else
                {
                    List<GAction> subset = ActionSubSet(usebleActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }
    ///ゴールが達成されたかどうかをチェック
    private bool GoalAchived(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        // ゴールの各条件をチェック
        foreach (KeyValuePair<string, int> g in goal)
        {
            // 状態にゴールの条件が存在しない場合、ゴールは達成されていない
            if (!state.ContainsKey(g.Key))
            {
                return false;
            }
        }
        // すべてのゴール条件が状態に存在する場合、ゴールは達成されている
        return true;
    }
    private List<GAction> ActionSubSet(List<GAction> usebleActions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in usebleActions)
        {
            if (!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }
        return subset;
    }
}
