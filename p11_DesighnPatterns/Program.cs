using System;
using p11_DesighnPatterns.Behavioral;
using p11_DesighnPatterns.Structural;

namespace p11_DesighnPatterns
{
    class Program
    {
        static void Main(string[] args)
        {

            #region creational

            //SingletonExample.TestSingleton();
            //BuilderEx.TestBuilder();
            // BuilderSingletonEx.Test();
            // FactoryMethodEx.Test();
            //AbstractFactoryEx.Test();

            #endregion

            #region structural

            //ProxyEx.Test();
            //Adapter.Test();
            //FacadeEx.Test();
            //DecoratorEx.Start();
            SimpleDecoratorEx.Test();

            #endregion

            #region behavioral

            //StrategyEx.Test();
            // TemplateMethodEx.Test();
            //MediatorEx.Start();
            //VisitorEx.Test();

            #endregion
            
        }
    }
}