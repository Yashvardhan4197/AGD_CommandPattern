using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<ICommand> commandRegistry = new Stack<ICommand>();
    public CommandInvoker()
    {
        SubscribeToEvents();
    }

    public void ProcessCommand(ICommand command)
    {
        ExecuteCommand(command);
        RegisterCommand(command);
    }

    public void Undo()
    {
        
        if (RegistryCountCheck() && CheckActivePlayer())
        {
            Debug.Log("UndoButtonpressed");
            commandRegistry.Pop().Undo();
        }
    }

    private bool RegistryCountCheck()
    {
        if(commandRegistry.Count > 0)
        {
            return true;
        }return false;
    }

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

    private void SubscribeToEvents() => GameService.Instance.EventService.OnReplayButtonClicked.AddListener(SetReplayStack);

    private void SetReplayStack()
    {
        GameService.Instance.ReplayService.SetCommandStack(commandRegistry);
        commandRegistry.Clear();
    }



}
