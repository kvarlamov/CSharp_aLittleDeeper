using System;

namespace p11_DesighnPatterns.Behavioral
{
    internal sealed class CommandEx
    {
        public static void Start()
        {
            // client
            var controller = new CleanerController();
            controller.TurnOnAction();
            controller.ReturnToBaseAction();
        }
    }

    // client
    internal sealed class CleanerController
    {
        private readonly CleanerMediator _invoker;
        private readonly Cartman _cartman;

        public CleanerController()
        {
            _invoker = new CleanerMediator();
            _cartman = new Cartman();
        }
        public void TurnOnAction()
        {
            _invoker.SetCommand(new StartCleaningAllCommand(_cartman));
            _invoker.SendCommand();
        }

        public void ReturnToBaseAction()
        {
            _invoker.SetCommand(() => _cartman.ReturnToCharging());
            _invoker.ExCommand();
        }
    }
    
    // Invoker - хранит ссылку на команду, запускает команду
    internal sealed class CleanerMediator
    {
        private Command _command;
        private Action _commandAct;

        public CleanerMediator()
        {
            _command = new NoneCommand(null);
        }

        // classic style
        public void SetCommand(Command command)
        {
            _command = command;
        }

        // functional style
        public void SetCommand(Action command)
        {
            _commandAct = command;
        }

        // classic style
        public void SendCommand()
        {
            _command.Execute();
        }

        // functional style
        public void ExCommand()
        {
            _commandAct?.Invoke();
        }
    }

    // receiver - тот, кто выполняет команду (Содержит бизнес логику), ничего не занет об отправителе команды
    public sealed class Cartman
    {
        public void StartCleaningAll()
        {
            Console.WriteLine("Cleaning all started");
        }

        public void ReturnToCharging()
        {
            Console.WriteLine("Return to base to charge");
        }
    }

    internal abstract class Command
    {
        protected readonly Cartman Cartman;

        public Command(Cartman cartman)
        {
            Cartman = cartman;
        }
        
        public abstract void Execute();
    }

    internal class NoneCommand : Command
    {
        public NoneCommand(Cartman cartman) : base(cartman)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
    
    internal class StartCleaningAllCommand : Command
    {
        public StartCleaningAllCommand(Cartman cartman) : base(cartman)
        {
        }

        public override void Execute()
        {
            Cartman.StartCleaningAll();
        }
    }
}