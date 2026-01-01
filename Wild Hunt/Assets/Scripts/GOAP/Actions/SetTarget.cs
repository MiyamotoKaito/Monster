using System.Collections.Generic;
using UnityEngine;

public class SetTarget : ActionBase
{
    [SerializeField] private bool _findNearest = true; // 最も近いターゲットを探すか

    public override Dictionary<string, int> Preconditions => new Dictionary<string, int>();

    public override Dictionary<string, int> Effects =>
        new Dictionary<string, int>()
        {
            { _effect.ToString(), 1 }
        };

    public override bool CheckPrecondition(GAgent agent) => true;

    public override void Execute(GAgent agent)
    {
        string targetTag = agent.CurrentTargetTag;

        if (string.IsNullOrEmpty(targetTag))
        {
            Debug.LogWarning("[SetTarget] CurrentTargetTagが設定されていません");
            return;
        }

        GameObject targetObj;

        if (_findNearest)
        {
            targetObj = FindNearestTargetByTag(targetTag, agent.transform.position);
        }
        else
        {
            targetObj = FindTargetByTag(targetTag);
        }

        if (targetObj != null)
        {
            _target = targetObj.transform;
            agent.SetTargetObj(targetObj); // エージェントのTargetObjを更新
            Debug.Log($"[SetTarget] ターゲット設定: {targetObj.name} (タグ: {targetTag})");
        }
    }

    public override bool Perform(GAgent agent)
    {
        if (_target == null) return false;

        // 【重要】実行時にワールドステートを更新
        GOAP.WorldStates.WorldStates.Instance.ModifyState(_effect.ToString(), 1);

        return true;
    }
}