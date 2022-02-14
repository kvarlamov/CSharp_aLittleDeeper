using System;

namespace p11_DesighnPatterns.Behavioral
{
    public class TemplateMethodEx
    {
        public static void Test()
        {
            HumanAI human = new HumanAI();
            OrcAI orc = new OrcAI();

            Console.WriteLine("Human turn");
            human.Turn();
            Console.WriteLine("Orc turn");
            orc.Turn();
        }
    }

    public abstract class GameAI
    {
        //template method
        public void Turn()
        {
            CollectResources();
            Build();
            CreateUnits();
            Attack();
        }

        protected abstract void CreateUnits();

        protected abstract void Build();

        protected abstract void CollectResources();

        //second template method
        private void Attack()
        {
            FindEnemy();
            SendTroop();
        }

        protected abstract void SendTroop();

        protected abstract void FindEnemy();
    }

    public class HumanAI : GameAI
    {
        protected override void CreateUnits()
        {
            Console.WriteLine("HumanAI created units");
        }

        protected override void Build()
        {
            Console.WriteLine("HumanAI built");
        }

        protected override void CollectResources()
        {
            Console.WriteLine("HumanAI collected resources");
        }

        protected override void SendTroop()
        {
            Console.WriteLine("HumanAI sent troop");
        }

        protected override void FindEnemy()
        {
            Console.WriteLine("HumanAI found enemy");
        }
    }

    public class OrcAI : GameAI
    {
        protected override void CreateUnits()
        {
            Console.WriteLine("OrcAI created units");
        }

        protected override void Build()
        {
            Console.WriteLine("OrcAI built");
        }

        protected override void CollectResources()
        {
            Console.WriteLine("OrcAI collected resources");
        }

        protected override void SendTroop()
        {
            Console.WriteLine("OrcAI sent troop");
        }

        protected override void FindEnemy()
        {
            Console.WriteLine("OrcAI found enemy");
        }
    }
}