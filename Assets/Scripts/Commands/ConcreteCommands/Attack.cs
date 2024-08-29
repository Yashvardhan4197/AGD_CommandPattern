using Command.Main;
using Command.Commands;

namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class Attack: UnitCommands
    {
        private bool willHitTarget;

        public Attack(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(Command.Commands.CommandType.Attack).PerformAction(actorUnit, targetUnit,willHitTarget);
        }

        public override bool WillHitTarget()
        {
            return true;
        }

        public override void Undo()
        {
            if (willHitTarget)
            {
                if (!targetUnit.IsAlive())
                {
                    targetUnit.Revive();
                }
                targetUnit.RestoreHealth(actorUnit.CurrentPower);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }

    }
}
