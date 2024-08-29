using Command.Main;
using Command.Commands;
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
            GameService.Instance.ActionService.GetActionByType(Command.Commands.CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget()
        {
            return true;
        }

        public override void Undo()
        {
            if(willHitTarget)
            {
                if (targetUnit.CurrentHealth < targetUnit.CurrentMaxHealth)
                {
                    targetUnit.TakeDamage(actorUnit.CurrentPower);        
                }
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }

    }
}
