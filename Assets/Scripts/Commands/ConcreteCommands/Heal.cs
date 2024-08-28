using Command.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class Heal: UnitCommands
    {
        private bool willHitTarget;
        public Heal(CommandData commandData)
        {
            this.commandData=commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(Command.Actions.CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget()
        {
            return true;
        }

        public override void Undo()
        {
            if(willHitTarget)
    {
                targetUnit.TakeDamage(actorUnit.CurrentPower);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }

    }
}
