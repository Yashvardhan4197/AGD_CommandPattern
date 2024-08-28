using Command.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            GameService.Instance.ActionService.GetActionByType(Command.Actions.CommandType.Attack).PerformAction(actorUnit, targetUnit,willHitTarget);
        }

        public override bool WillHitTarget()
        {
            return true;
        }

        public override void Undo()
        {
            actorUnit.RestoreHealth(actorUnit.CurrentPower);
            actorUnit.Owner.ResetCurrentActiveUnit();
        }

    }
}
