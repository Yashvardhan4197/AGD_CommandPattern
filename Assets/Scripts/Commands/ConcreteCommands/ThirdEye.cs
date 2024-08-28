using Command.Main;


namespace Assets.Scripts.Commands.ConcreteCommands
{
    public class ThirdEye: UnitCommands
    {
        private bool willHitTarget;
        public ThirdEye(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }
        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(Command.Actions.CommandType.AttackStance).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override bool WillHitTarget()
        {
            return true;
        }
        public override void Undo()
        {
            if(willHitTarget)
            {
                int healthToConvert = (int)(targetUnit.CurrentHealth * 0.25f);
                targetUnit.RestoreHealth(healthToConvert);
                targetUnit.CurrentPower -= healthToConvert;
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
        }

    }
}
