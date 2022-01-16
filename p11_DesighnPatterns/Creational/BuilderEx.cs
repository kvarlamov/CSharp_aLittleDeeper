using System;

namespace p11_DesighnPatterns
{
    public class BuilderEx
    {
        public static void TestBuilder()
        {
            var vehicleBuilder = new Builder(Guid.NewGuid(), "Jeep", 4);
            var trailerBuilder = new Builder(Guid.NewGuid(), "Trailer", 2);
            var jeep = vehicleBuilder
                .SetFuelType("petrol")
                .SetProductionYear(2020)
                .SetTrailer(
                    trailerBuilder
                    .SetProductionYear(2015)
                    .Build())
                .Build();

            Console.WriteLine(jeep.ToString());
            Console.ReadKey();
        }
    }

    public class Vehicle
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly int _numberOfWheels;
        private readonly int _productionYear;
        private readonly bool _isCommercial;
        private readonly string _fuelType;
        private readonly Vehicle _trailer;

        public Vehicle(Guid id, string name, int numberOfWheels, int productionYear, bool isCommercial, string fuelType, Vehicle trailer)
        {
            _id = id;
            _name = name;
            _numberOfWheels = numberOfWheels;
            _productionYear = productionYear;
            _isCommercial = isCommercial;
            _fuelType = fuelType;
            _trailer = trailer;
        }

        public override string ToString()
        {
            return $"Your car:{PrintVehicle(this)}\n trailer:{PrintVehicle(_trailer)}";
        }

        private string PrintVehicle(Vehicle vh) => $"id: {vh._id}\n" +
                                                   $"name: {vh._name}\n" +
                                                   $"production year:{vh._productionYear}\n" +
                                                   $"number of wheels: {vh._numberOfWheels}\n" +
                                                   $"fuel: {vh._fuelType}";
    }
    
    public class Builder
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly int _numberOfWheels;
        private int _productionYear;
        private string _fuelType;
        private bool _isCommercial;
        private Vehicle _trailer;

        public Builder(Guid id, string name, int numberOfWheels)
        {
            _id = id;
            _name = name;
            _numberOfWheels = numberOfWheels;
        }

        public Builder SetProductionYear(int productionYear)
        {
            _productionYear = productionYear;
            return this;
        }
            
        public Builder SetCommercial()
        {
            _isCommercial = true;
            return this;
        }
            
        public Builder SetFuelType(string fuelType)
        {
            _fuelType = fuelType;
            return this;
        }

        public Builder SetTrailer(Vehicle trailer)
        {
            _trailer = trailer;
            return this;
        }

        public Vehicle Build()
        {
            return new Vehicle(_id, _name, _numberOfWheels, _productionYear, _isCommercial, _fuelType, _trailer);
        }
    }
}