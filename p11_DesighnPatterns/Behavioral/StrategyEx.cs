using System;
using System.Collections.Generic;

namespace p11_DesighnPatterns.Behavioral
{
    public class StrategyEx
    {
        public static void Test()
        {
            var l = new List<CarStrategy>();
            l.Sort((x,y)=> x.Name.CompareTo(y));
            Navigator nav = new Navigator();
            nav.SetStrategy(new WalkingStrategy());
            nav.Execute();
            nav.Execute(new PublicTransportStrategy(), strategy => strategy.BuildRoute());
        }

        private class Navigator
        {
            public IRouteStrategy Strategy { get; private set; }

            public void SetStrategy(IRouteStrategy strategy)
            {
                Strategy = strategy;
            }

            public void Execute()
            {
                Strategy.BuildRoute();
            }


            public void Execute(IRouteStrategy strategy, Action<IRouteStrategy> createRoute)
            {
                createRoute.Invoke(strategy);
            }
        }

        private interface IRouteStrategy
        {
            void BuildRoute();
        }

        private class CarStrategy : IRouteStrategy
        {
            public string Name { get; set; }
            public void BuildRoute()
            {
                Console.WriteLine("Travelling by car");
            }
        }

        private class WalkingStrategy : IRouteStrategy
        {
            public void BuildRoute()
            {
                Console.WriteLine("Walking");
            }
        }

        private class PublicTransportStrategy : IRouteStrategy
        {
            public void BuildRoute()
            {
                Console.WriteLine("Go on public transport");
            }
        }
    }
}