using System.Diagnostics.CodeAnalysis;
using CommandTerminal;

namespace Hero
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public class HeroCommand
    {
        [RegisterCommand(Help = "Sets Hero Health")]
        private static void CommandSetHP(int health)
        {
            HeroManager.Instance.SetHealth(health);

            Terminal.Log("Set Hero Health to {0}.", health);
        }

        private static void FrontCommandSetHP(CommandArg[] args)
        {
            var health = args[0].Int;

            if (Terminal.IssuedError) return;

            CommandSetHP(health);
        }


        [RegisterCommand(Help = "Adds Hero Health")]
        private static void CommandAddHP(int money)
        {
            HeroManager.Instance.AddHealth(money);

            Terminal.Log("Added {0} Hero Health. (now {1}.)", money, HeroManager.Instance.health);
        }

        private static void FrontCommandAddHP(CommandArg[] args)
        {
            var health = args[0].Int;

            if (Terminal.IssuedError) return;

            CommandAddHP(health);
        }


        [RegisterCommand(Help = "Sets Money")]
        private static void CommandSetMoney(int money)
        {
            HeroManager.Instance.SetMoney(money);

            Terminal.Log("Set Money to {0}.", money);
        }

        private static void FrontCommandSetMoney(CommandArg[] args)
        {
            var money = args[0].Int;

            if (Terminal.IssuedError) return;

            CommandSetMoney(money);
        }


        [RegisterCommand(Help = "Adds Money")]
        private static void CommandAddMoney(int money)
        {
            HeroManager.Instance.AddMoney(money);

            Terminal.Log("Added {0} Money. (now {1}.)", money, HeroManager.Instance.health);
        }

        private static void FrontCommandAddMoney(CommandArg[] args)
        {
            var money = args[0].Int;

            if (Terminal.IssuedError) return;

            CommandAddMoney(money);
        }


        [RegisterCommand(Help = "Sets Exp")]
        private static void CommandSetExp(int exp)
        {
            HeroManager.Instance.SetExp(exp);

            Terminal.Log("Set Exp to {0}.", exp);
        }

        private static void FrontCommandSetExp(CommandArg[] args)
        {
            var exp = args[0].Int;

            if (Terminal.IssuedError) return;

            CommandSetExp(exp);
        }


        [RegisterCommand(Help = "Adds Exp")]
        private static void CommandAddExp(int exp)
        {
            HeroManager.Instance.AddExp(exp);

            Terminal.Log("Added {0} Exp. (now {1}.)", exp, HeroManager.Instance.exp);
        }

        private static void FrontCommandAddExp(CommandArg[] args)
        {
            var exp = args[0].Int;

            if (Terminal.IssuedError) return;

            CommandAddExp(exp);
        }
    }
}