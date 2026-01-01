using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GOAP.GPlanner
{
    public static class GPlanner
    {
        public static Queue<IAction> Planning(List<IAction> actions,
                                       Dictionary<string, int> goal,
                                       Dictionary<string, int> currentState)
        {
            Debug.Log($"<color=cyan>[GOAP Planner] 計画開始: ゴール={string.Join(",", goal.Select(x => x.Key + ":" + x.Value))}</color>");

            List<IAction> usableActions = new List<IAction>(actions);
            List<GNode> leaves = new List<GNode>();
            GNode start = new GNode(null, 0, currentState, null);

            if (BuildGraph(start, leaves, usableActions, goal))
            {
                GNode cheapest = null;
                foreach (GNode leaf in leaves)
                {
                    if (cheapest == null || leaf.Cost < cheapest.Cost)
                        cheapest = leaf;
                }

                Debug.Log("<color=green>[GOAP Planner] 計画成功！パスを再構築します</color>");
                return ReconstructPath(cheapest);
            }

            Debug.LogWarning("<color=red>[GOAP Planner] 計画失敗: ゴールに到達できるアクションの組み合わせがありません" +
              "BuildGraphがfalseです</color>");
            return null;
        }

        private static bool BuildGraph(GNode parent, List<GNode> leaves, List<IAction> usableAction, Dictionary<string, int> goal)
        {
            bool foundPath = false;
            foreach (var act in usableAction)
            {
                if (CheckPreconditions(act.Preconditions, parent.State))
                {
                    Dictionary<string, int> newState = CreateNewState(parent.State, act.Effects);
                    GNode newNode = new GNode(parent, parent.Cost + act.Cost, newState, act);

                    if (CanGoalAchieved(goal, newState))
                    {
                        leaves.Add(newNode);
                        foundPath = true;
                    }
                    else
                    {
                        List<IAction> subset = usableAction.Where(a => a != act).ToList();
                        if (BuildGraph(newNode, leaves, subset, goal))
                            foundPath = true;
                    }
                }
                else
                {
                    // なぜダメだったのかのログ（ここが一番重要）
                    Debug.Log($"[Planner] スキップ: {act.GetType().Name} (前提条件が未達成)");
                }
            }
            return foundPath;
        }

        private static bool CanGoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
        {
            foreach (var g in goal)
            {
                if (!state.ContainsKey(g.Key) || state[g.Key] != g.Value) return false;
            }
            return true;
        }

        private static bool CheckPreconditions(Dictionary<string, int> preconditions, Dictionary<string, int> currentState)
        {
            foreach (var pre in preconditions)
            {
                if (!currentState.ContainsKey(pre.Key) || currentState[pre.Key] != pre.Value) return false;
            }
            return true;
        }

        private static Dictionary<string, int> CreateNewState(Dictionary<string, int> currentState, Dictionary<string, int> effects)
        {
            Dictionary<string, int> newState = new Dictionary<string, int>(currentState);
            foreach (var effect in effects) newState[effect.Key] = effect.Value;
            return newState;
        }

        private static Queue<IAction> ReconstructPath(GNode cheapestGoalNode)
        {
            List<IAction> path = new List<IAction>();
            GNode current = cheapestGoalNode;
            while (current.Action != null)
            {
                path.Add(current.Action);
                current = current.Parent;
            }
            path.Reverse();

            var q = new Queue<IAction>(path);
            Debug.Log($"[GOAP Planner] 最終計画: {string.Join(" -> ", q.Select(a => a.GetType().Name))}");
            return q;
        }
    }
}