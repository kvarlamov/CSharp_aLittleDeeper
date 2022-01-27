namespace p11_DesighnPatterns.Structural
{
    public class ChainOfResponsibility
    {
        public static void Test()
        {
            //todo: example isn't completed
        }
    }

    public class OrderingSystem
    {
        private string _data;
        
        public string StartOrdering(string data)
        {
            if (Authorize(data))
            {
                data = Sanitaize(data);
                if (FilterIps(data))
                {
                    if (data.Equals(_data))
                    {
                        return _data;
                    }
                    else
                    {
                        return data;
                    }
                }
            }

            return null;
        }

        private bool FilterIps(string data)
        {
            return data.Contains("badIp");
        }

        private bool Authorize(string data)
        {
            return data.Contains("authorized");
        }
        
        public string Sanitaize(string data)
        {
            return $"{data} sanitazed";
        }
    }
}