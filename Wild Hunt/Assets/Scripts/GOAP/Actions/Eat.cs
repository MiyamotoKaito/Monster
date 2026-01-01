using System.Collections.Generic;
using UnityEngine;

public class Eat : ActionBase
{
    public override Dictionary<string, int> Effects =>
    new Dictionary<string, int>()
    {
            { _subGoal.ToString(), 1 },{ "HasTarget", 0 },{ "AtTarget", 0 }               
    };

    [SerializeField] private SubGoalType _subGoal;

    // 明示的にEffectsを定義
    public override bool Perform(GAgent agent)
    {
        if (_target == null) return false;

        var survivalStats = agent.GetComponent<SurvivalStats>();
        if (survivalStats != null)
        {
            survivalStats.OnAte();
        }
        else
        {
            Debug.LogWarning("EAT: SurvivalStatsコンポーネントが見つかりません");
        }

        GameObject.Destroy(_target.gameObject);
        return true;
    }
}