using System;

namespace p11_DesighnPatterns
{
    public class AbstractFactoryEx
    {
        public static void Test()
        {
            Console.WriteLine("Wood furniture");
            var order = new FurnitureOrder(new WoodFurniture());
            order.PrintInfo();
            Console.WriteLine("\nIron furniture");
            order = new FurnitureOrder(new IronFurniture());
            order.PrintInfo();
        }
    }

    public class FurnitureOrder
    {
        private IChair _chair;
        private IDesk _desk;

        public FurnitureOrder(FurnitureFactory factory)
        {
            _chair = factory.GetChairs();
            _desk = factory.GetTables();
        }

        public void PrintInfo()
        {
            Console.WriteLine("Chairs: {0}",_chair.Name);
            Console.WriteLine("Desks: {0}",_desk.Name);
        }
    }

    public abstract class FurnitureFactory
    {
        public abstract IDesk GetTables();
        public abstract IChair GetChairs();
    }

    public class WoodFurniture : FurnitureFactory
    {
        public override IDesk GetTables()
        {
            return new WoodDesk();
        }

        public override IChair GetChairs()
        {
            return new WoodChair();
        }
    }
    
    public class IronFurniture : FurnitureFactory
    {
        public override IDesk GetTables()
        {
            return new IronDesk();
        }

        public override IChair GetChairs()
        {
            return new IronChair();
        }
    }
    
    public interface IChair
    {
        string Name { get; set; }
    }

    public class WoodChair : IChair
    {
        public string Name { get; set; } = "Wood chair";
    }
    
    public class IronChair : IChair
    {
        public string Name { get; set; } = "Iron chair";
    }
    
    public interface IDesk
    {
        string Name { get; set; }
    }

    public class WoodDesk : IDesk
    {
        public string Name { get; set; } = "Wood desk";
    }
    
    public class IronDesk : IDesk
    {
        public string Name { get; set; } = "Iron desk";
    }
    
    
}