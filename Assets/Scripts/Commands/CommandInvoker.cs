using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<ICommand> commandRegistry = new Stack<ICommand>();
    public CommandInvoker()
    {
        
    }

    public void ProcessCommand(ICommand command)
    {
        ExecuteCommand(command);
        RegisterCommand(command);
    }

    public void Undo()
    {
        if (!RegistryCountCheck() && CheckActivePlayer())
        {
            commandRegistry.Pop().Undo();
        }
    }

    private bool RegistryCountCheck() => commandRegistry.Count > 0;

    private bool CheckActivePlayer()
    {
        if((commandRegistry.Peek() as UnitCommands).commandData.ActorPlayerID == GameService.Instance.PlayerService.ActivePlayerID)
        {
            return true;
        }
        return false;
    }

    private void ExecuteCommand(ICommand command)=>command.Execute();
    private void RegisterCommand(ICommand command)=>commandRegistry.Push(command);



}
