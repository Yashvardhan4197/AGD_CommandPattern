using Command.Main;
using Command.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class Meditate: UnitCommands
    {
        private bool willHitTarget;

        public Meditate(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }
        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(Command.Commands.CommandType.Meditate).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget()
        {
            return true;
        }

        public override void Undo()
        {
            if (willHitTarget)
            {
                var healthToDecrease = (int)(targetUnit.CurrentMaxHealth * 0.2f);
                targetUnit.CurrentMaxHealth -= healthToDecrease;
                targetUnit.TakeDamage(healthToDecrease);
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
        }

    }
}
