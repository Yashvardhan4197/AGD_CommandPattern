using Command.Main;

namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class Cleanse: UnitCommands
    {
        private bool willHitTarget;
        private int previousPower;
        public Cleanse(CommandData commandData) 
        {
            this.commandData = commandData;
            this.willHitTarget = WillHitTarget();
        }
        public override void Execute()
        {
            previousPower=targetUnit.CurrentPower;
            GameService.Instance.ActionService.GetActionByType(Command.Actions.CommandType.Cleanse).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget()
        {
            return true;
        }

        public override void Undo()
        {
            if (willHitTarget)
            {
                targetUnit.CurrentPower = previousPower;
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
        }

    }
}
