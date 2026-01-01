using System.Collections.Generic;
using UnityEngine;

public class GSubGoal
{
    public Dictionary<string, int> SubGoals;
    public string Name;
    public bool Remove;
    public GameObject TargetObj;
    public string TargetTag;

    public GSubGoal(string goalName, int priority, bool remove, GameObject target, string targetTag = null)
    {
        SubGoals = new Dictionary<string, int>();
        Name = goalName;
        TargetObj = target;
        TargetTag = targetTag;
        SubGoals.Add(goalName, priority);
        Remove = remove;
    }
}