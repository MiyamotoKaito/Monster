using System.Collections.Generic;
using UnityEngine;

public class Drink : ActionBase
{
    [SerializeField] private SubGoalType _subGoal;
    [SerializeField] private float _drinkAmount;

    // Effectsを明示的に定義
    public override Dictionary<string, int> Effects =>
        new Dictionary<string, int>()
        {
            { _subGoal.ToString(), 1 },
            { "HasTarget", 0 },
            { "AtTarget", 0 }
        };

    public override bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public override void Execute(GAgent agent)
    {
        _target = agent.TargetObj != null ? agent.TargetObj.transform : null;

        if (_target == null)
        {
            Debug.Log("<color=red>飲み物のターゲットが見つかりませんでした</color>");
        }
    }

    public override bool Perform(GAgent agent)
    {
        // ターゲットが消滅していたら失敗
        if (_target == null) return false;

        // SurvivalStatsに通知して喉の渇きタイマーをリセット
        var survival = agent.GetComponent<SurvivalStats>();
        if (survival != null)
        {
            survival.OnDrank();
        }
        else
        {
            Debug.LogWarning("Drink: SurvivalStatsコンポーネントが見つかりません");
        }

        // 飲み物を消去
        GameObject.Destroy(_target.gameObject);
        return true;
    }
}