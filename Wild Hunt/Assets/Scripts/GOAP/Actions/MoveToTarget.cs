using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : ActionBase
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _stopDistance = 1.5f;

    public override Dictionary<string, int> Preconditions =>
        new Dictionary<string, int>()
        {
            { _precondition.ToString(), 1 }
        };

    public override Dictionary<string, int> Effects =>
        new Dictionary<string, int>()
        {
            { "AtTarget", 1 }  // 固定文字列を使用
        };

    public override bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public override void Execute(GAgent agent)
    {
        _target = agent.TargetObj != null ? agent.TargetObj.transform : null;
    }

    public override bool Perform(GAgent agent)
    {
        // ターゲットがいない場合は失敗として中断
        if (_target == null) return false;

        float distance = Vector3.Distance(agent.transform.position, _target.position);

        // まだ離れている場合
        if (distance > _stopDistance)
        {
            // 移動処理
            Vector3 direction = (_target.position - agent.transform.position).normalized;
            agent.transform.position += direction * _speed * Time.deltaTime;
            return false;
        }

        // 到着した
        Debug.Log($"{GetType().Name} : 目的地に到着しました。");
        return true;
    }
}