using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Commands;

namespace Command.Replay
{
    public class ReplayService
    {
        private Stack<ICommand> recordedCommandRegistry;

        public ReplayService() => SetReplayState(ReplayState.DEACTIVE);

        public ReplayState replayState { get; private set; }

        public void SetReplayState(ReplayState replayState) => this.replayState = replayState;


        public void SetCommandStack(Stack<ICommand> commandStack)
        {
            Debug.Log("Tf the size is:" + commandStack.Count);
            recordedCommandRegistry = new Stack<ICommand>(commandStack);
            Debug.Log("ff the size is:" + recordedCommandRegistry.Count);
            Debug.Log("ff the size is:" + recordedCommandRegistry.Count);

        }

        public IEnumerator ExecuteNext()
        {
            yield return new WaitForSeconds(1);
            if (recordedCommandRegistry.Count > 0)
            {
                Debug.Log("hello cutiee");
                GameService.Instance.CommandInvoker.ProcessCommand(recordedCommandRegistry.Peek());
                recordedCommandRegistry.Pop();
            }
        }

    }
    public enum ReplayState
    {
        DEACTIVE,
        ACTIVE
    }

}
