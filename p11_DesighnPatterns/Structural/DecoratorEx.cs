using System;

namespace p11_DesighnPatterns.Structural
{
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
}