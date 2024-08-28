using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayService : MonoBehaviour
{
    private Stack<ICommand> recordedCommandRegistry;

    public ReplayService() => SetReplayState(ReplayState.DEACTIVE);

    public ReplayState replayState {  get; private set; }

    public void SetReplayState(ReplayState replayState)=>this.replayState = replayState;

    
    public void SetCommandStack(Stack<ICommand> commandStack)=>recordedCommandRegistry=commandStack;

    public void ExecuteNext()
    {
        if (recordedCommandRegistry.Count > 0)
        {
            GameService.Instance.CommandInvoker.ProcessCommand(recordedCommandRegistry.Pop());
        }
    }

}
public enum ReplayState
{
    DEACTIVE,
    ACTIVE
}
