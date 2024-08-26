using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<ICommand> _command;
    public CommandInvoker()
    {
        _command = new Stack<ICommand>();
    }

    public void ProcessCommand(ICommand command)
    {
        ExecuteCommand(command);
        RegisterCommand(command);
    }

    private void ExecuteCommand(ICommand command)=>command.Execute();
    private void RegisterCommand(ICommand command)=>_command.Push(command);

}
