using System.Collections.Generic;

public class GNode
{
    public GNode Parent;
    public int Cost;
    public Dictionary<string, int> State;
    public IAction Action;

    public GNode(GNode parent, int cost, Dictionary<string, int> allState, IAction action)
    {
        Parent = parent;
        Cost = cost;
        State = allState;
        Action = action;
    }
}