using Command.Main;

namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class Cleanse: UnitCommands
    {
        private bool willHitTarget;
        public Cleanse(CommandData commandData) 
        {
            this.commandData = commandData;
            this.willHitTarget = WillHitTarget();
        }
        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(Command.Actions.CommandType.Cleanse).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget()
        {
            return true;
        }
    }
}
