using Assets.Scripts.Commands;
using Command.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCommands : ICommand
{
    public CommandData commandData;

    protected UnitController actorUnit;
    protected UnitController targetUnit;

    public abstract void Execute();

    public abstract bool WillHitTarget();
}
