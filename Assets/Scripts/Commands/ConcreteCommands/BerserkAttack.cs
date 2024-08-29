
using Command.Main;
using Command.Commands;

namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class BerserkAttack:UnitCommands
    {
        private bool willHitTarget;
        public BerserkAttack(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(Command.Commands.CommandType.BerserkAttack).PerformAction(actorUnit, targetUnit, willHitTarget);
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
                  targetUnit.RestoreHealth(actorUnit.CurrentPower + 2);
            }
            else
            {
                if (!actorUnit.IsAlive())
                {
                    actorUnit.Revive();
                }
                  actorUnit.RestoreHealth(actorUnit.CurrentPower + 2);
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
        }

    }
}
