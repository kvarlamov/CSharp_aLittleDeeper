using System;

namespace p11_DesighnPatterns
{
    public enum TransportType
    {
        RailRoad,
        Sea,
        Plain
    }
    
    public class FactoryMethodEx
    {
        public static void Test()
        {
            var order = new Order(new SeaLogistic());
            order.PlanLogistic();
        }
    }

    public class Order
    {
        private readonly Logistic _logistic;

        public Order(Logistic logistic)
        {
            _logistic = logistic;
        }

        public void PlanLogistic()
        {
            _logistic.Calculate();
        }
    }

    public interface ITransport
    {
        TransportType TransportType { get; }
    }

    public class Ship : ITransport
    {
        public Ship()
        {
            TransportType = TransportType.Sea;
        }
        
        public TransportType TransportType { get; }
    }

    public class Train : ITransport
    {
        public Train()
        {
            TransportType = TransportType.RailRoad;
        }
        
        public TransportType TransportType { get; }
    }
    
    public class Aircraft : ITransport
    {
        public Aircraft()
        {
            TransportType = TransportType.Plain;
        }
        
        public TransportType TransportType { get; }
    }

    public abstract class Logistic
    {
        public string Name { get; set; }
        public abstract ITransport CreateTransport();

        public void Calculate()
        {
            var transport = CreateTransport();
            Console.WriteLine(transport.TransportType);
        }
    }

    

    public class RailRoadLogistic : Logistic
    {
        public override ITransport CreateTransport()
        {
            return new Train();
        }
    }

    public class SeaLogistic : Logistic
    {
        public override ITransport CreateTransport()
        {
            return new Ship();
        }
    }
}