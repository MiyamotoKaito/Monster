using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : IAction
{
    [SerializeField] protected int _cost;
    [SerializeField] protected WorldStateType _precondition;
    [SerializeField] protected WorldStateType _effect;

    protected Transform _target;

    public virtual Dictionary<string, int> Preconditions =>
        new Dictionary<string, int>() { { _precondition.ToString(), 1 } };

    public virtual Dictionary<string, int> Effects =>
        new Dictionary<string, int>() { { _effect.ToString(), 1 } };

    public int Cost => _cost;

    // Executeでエージェントの現在のターゲットタグを使用してターゲットを検索
    public virtual void Execute(GAgent agent)
    {
        if (agent.TargetObj != null)
        {
            _target = agent.TargetObj.transform;
        }
    }

    public virtual bool CheckPrecondition(GAgent agent)
    {
        return _target != null;
    }

    public abstract bool Perform(GAgent agent);

    // ヘルパーメソッド：タグでターゲットを検索
    protected GameObject FindTargetByTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return null;

        GameObject target = GameObject.FindGameObjectWithTag(tag);

        if (target == null)
        {
            Debug.LogWarning($"[ActionBase] タグ '{tag}' のオブジェクトが見つかりませんでした");
        }

        return target;
    }

    // ヘルパーメソッド：最も近いターゲットを検索
    protected GameObject FindNearestTargetByTag(string tag, Vector3 position)
    {
        if (string.IsNullOrEmpty(tag)) return null;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);

        if (targets.Length == 0)
        {
            Debug.LogWarning($"[ActionBase] タグ '{tag}' のオブジェクトが見つかりませんでした");
            return null;
        }

        GameObject nearest = null;
        float minDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = target;
            }
        }

        return nearest;
    }
}