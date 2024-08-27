using Command.Main;
using Command.Player;
using Command.Actions;
using Assets.Scripts.Commands;
using Assets.Scripts.Commands.ConcreteCommands;

namespace Command.Input
{
    public class InputService
    {
        private MouseInputHandler mouseInputHandler;

        private InputState currentState;
        private CommandType selectedCommandType;
        private TargetType targetType;

        public InputService()
        {
            mouseInputHandler = new MouseInputHandler(this);
            SetInputState(InputState.INACTIVE);
            SubscribeToEvents();
        }

        public void SetInputState(InputState inputStateToSet) => currentState = inputStateToSet;

        private void SubscribeToEvents() => GameService.Instance.EventService.OnActionSelected.AddListener(OnActionSelected);

        public void UpdateInputService()
        {
            if(currentState == InputState.SELECTING_TARGET)
                mouseInputHandler.HandleTargetSelection(targetType);
        }

        public void OnActionSelected(CommandType selectedActionType)
        {
            this.selectedCommandType = selectedActionType;
            SetInputState(InputState.SELECTING_TARGET);
            TargetType targetType = SetTargetType(selectedActionType);
            ShowTargetSelectionUI(targetType);
        }

        private void ShowTargetSelectionUI(TargetType selectedTargetType)
        {
            int playerID = GameService.Instance.PlayerService.ActivePlayerID;
            GameService.Instance.UIService.ShowTargetOverlay(playerID, selectedTargetType);
        }

        private TargetType SetTargetType(CommandType selectedActionType) => targetType = GameService.Instance.ActionService.GetTargetTypeForAction(selectedActionType);

        public void OnTargetSelected(UnitController targetUnit)
        {
            SetInputState(InputState.EXECUTING_INPUT);
            UnitCommands unitCommand=CreateUnitCommand(targetUnit);
            GameService.Instance.ProcessUnitCommand(unitCommand);
            //GameService.Instance.PlayerService.PerformAction(selectedCommandType, targetUnit);
        }

        private UnitCommands CreateUnitCommand(UnitController targetUnit)
        {
            CommandData commandData = CreateCommandData(targetUnit);

            switch (selectedCommandType)
            {
                case CommandType.Attack:
                    return new Attack(commandData);
                case CommandType.AttackStance:
                    return new AttackStance(commandData);
                case CommandType.BerserkAttack:
                    return new BerserkAttack(commandData);
                case CommandType.ThirdEye:
                    return new ThirdEye(commandData);
                case CommandType.Heal:
                    return new Heal(commandData);
                case CommandType.Meditate:
                    return new Meditate(commandData);
                case CommandType.Cleanse:
                    return new Cleanse(commandData);
            }
            throw new System.Exception("Command Not Found");
        }

        private CommandData CreateCommandData(UnitController targetUnit)
        {
            CommandData newCommandData=new CommandData(GameService.Instance.PlayerService.ActiveUnitID,targetUnit.UnitID,GameService.Instance.PlayerService.ActivePlayerID,targetUnit.Owner.PlayerID);
            return newCommandData;
        }

    }
}