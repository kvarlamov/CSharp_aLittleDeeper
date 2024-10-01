using System;

namespace p11_DesighnPatterns.Structural
{
    internal class SimpleDecoratorEx
    {
        public static void Test()
        {
            Console.WriteLine("Client without decorator");
            ClientBehaviour clientBehaviour = new ClientBehaviour();
            clientBehaviour.DoingSomething();

            Console.WriteLine("\n Apply decorator 1");
            ClientDecorator1 clientDecorator1 = new ClientDecorator1(clientBehaviour);
            clientDecorator1.DoingSomething();

            Console.WriteLine("\n Apply decorator 2");
            ClientDecorator2 clientDecorator2 = new ClientDecorator2(clientDecorator1);
            clientDecorator2.DoingSomething();
        }
        
        private interface IClientBehaviour
        {
            void DoingSomething();
        }
        
        private class ClientBehaviour : IClientBehaviour
        {
            public void DoingSomething()
            {
                Console.WriteLine("Client doing something");
            }
        }
        
        private class ClientDecorator1 : IClientBehaviour
        {
            private readonly IClientBehaviour _clientBehaviour;

            public ClientDecorator1(IClientBehaviour clientBehaviour)
            {
                _clientBehaviour = clientBehaviour;
            }

            public void DoingSomething()
            {
                _clientBehaviour.DoingSomething();
                Console.WriteLine("Decorated 1");
            }
        }
        
        private class ClientDecorator2 : IClientBehaviour
        {
            private readonly IClientBehaviour _clientBehaviour;

            public ClientDecorator2(IClientBehaviour clientBehaviour)
            {
                _clientBehaviour = clientBehaviour;
            }

            public void DoingSomething()
            {
                _clientBehaviour.DoingSomething();
                Console.WriteLine("Decorated 2");
            }
        }
    }
    
    internal class DecoratorEx
    {
        public static void Start()
        {
            bool smsEnabled = true;
            bool facebookEnabled = true;
            
            INotifier baseNotifier = new Notifier();
            if (smsEnabled)
                baseNotifier = new SmsNotifierDecorator(baseNotifier);
            if (facebookEnabled)
                baseNotifier = new FacebookNotifierDecorator(baseNotifier);

            NotifierManager manager = new NotifierManager(baseNotifier);
            manager.Notify("You was notified");
        }
    }

    internal sealed class NotifierManager
    {
        private readonly INotifier _notifier;

        public NotifierManager(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void Notify(string msg) => _notifier.Notify(msg);
    }

    /// <summary>
    /// Interface to notify
    /// </summary>
    internal interface INotifier
    {
        void Notify(string msg);
    }

    internal sealed class Notifier : INotifier
    {
        public void Notify(string msg)
        {
            EmailNotify(msg);
        }

        private void EmailNotify(string msg)
        {
            //basic email notify
            Console.WriteLine($"notify email: {msg}");
        }
    }

    internal abstract class NotifyDecorator : INotifier
    {
        protected readonly INotifier _wrappee;

        public NotifyDecorator(INotifier wrappee)
        {
            _wrappee = wrappee;
        }

        public abstract void Notify(string msg);
    }

    internal class SmsNotifierDecorator : NotifyDecorator
    {
        public SmsNotifierDecorator(INotifier wrappee) : base(wrappee)
        {
        }

        public override void Notify(string msg)
        {
            _wrappee.Notify(msg);
            NotifyBySms(msg);
        }

        private void NotifyBySms(string msg)
        {
            Console.WriteLine($"notify sms: {msg}");
        }
    }
    
    internal class FacebookNotifierDecorator : NotifyDecorator
    {
        public FacebookNotifierDecorator(INotifier wrappee) : base(wrappee)
        {
        }

        public override void Notify(string msg)
        {
            _wrappee.Notify(msg);
            NotifyFacebook(msg);
        }

        private void NotifyFacebook(string msg)
        {
            Console.WriteLine($"notify facebook: {msg}");
        }
    }

    public static class TestClient
    {
        public static void HandleResponse()
        {
            Client cl = new Client();
        }
    }

    public abstract class BaseClient
    {
        public int Discount { get; set; }
        
        public abstract void ProcessClient();
    }

    public class Client : BaseClient
    {
        public override void ProcessClient()
        {
            Console.WriteLine("Do something with a client");
        }
    }


    public class VipClient : BaseClient
    {
        private readonly BaseClient _client;

        public VipClient(BaseClient client)
        {
            _client = client;
        }
        
        public override void ProcessClient()
        {
            // additional functionality or client modification
            _client.ProcessClient();
        }
    }
}