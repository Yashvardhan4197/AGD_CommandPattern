using Command.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCommands : ICommand
{
    public int ActorunitID;
    public int TargetunitID;
    public int ActorPlayerID;
    public int TargetPlayerID;

    protected UnitController actorUnit;
    protected UnitController targetUnit;

    public abstract void Execute();

    public abstract bool WillHitTarget();
}
