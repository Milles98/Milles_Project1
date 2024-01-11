using Autofac;
using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.CalculatorStrategyService;
using Milles_Project1Library.Services.ShapeStrategyService;

namespace Milles_Project1Library.Data
{
    public class DataSeeding
    {
        private readonly ProjectDbContext _dbContext;

        public DataSeeding(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
        }

        public void Seed()
        {
            _dbContext.Database.Migrate();

            if (!_dbContext.Shape.Any())
            {
                SeedShape<Rectangle>(new decimal[] { 4.0M, 6.0M });
                SeedShape<Parallelogram>(new decimal[] { 5.0M, 7.0M, 30.0M });
                SeedShape<Triangle>(new decimal[] { 3.0M, 4.0M, 5.0M });
                SeedShape<Rhombus>(new decimal[] { 8.0M, 60.0M });

                SeedCalculation(new Addition(), 5.0M, 3.0M);
                SeedCalculation(new Subtraction(), 8.0M, 4.0M);
                SeedCalculation(new Multiplication(), 2.0M, 7.0M);
                SeedCalculation(new Division(), 10.0M, 2.0M);
                SeedCalculation(new SquareRoot(), 3.0M, 2.0M);
                SeedCalculation(new Modulus(), 9.0M, 4.0M);

                _dbContext.SaveChanges();
            }
        }

        private void SeedShape<T>(decimal[] specificDimensions) where T : IShapeStrategy
        {
            var strategy = Activator.CreateInstance<T>();
            strategy.SetDimensions(specificDimensions);

            var resultShape = new Shape
            {
                ShapeType = strategy.ShapeType,
                Base = Math.Round(strategy.Base, 2),
                Height = Math.Round(strategy.Height, 2),
                SideLength = Math.Round(strategy.SideLength, 2),
                Area = Math.Round(strategy.CalculateArea(), 2),
                Perimeter = Math.Round(strategy.CalculatePerimeter(), 2),
                CalculationDate = DateTime.Now
            };

            _dbContext.Shape.Add(resultShape);
        }

        private void SeedCalculation(ICalculatorStrategy strategy, decimal num1, decimal num2)
        {
            if (!IsCalculationSeeded(strategy.GetType().Name))
            {
                decimal result = strategy.Calculate(num1, num2);

                var resultCalculation = new Calculator
                {
                    Operator = strategy.GetType().Name,
                    Number1 = num1,
                    Number2 = num2,
                    Result = result,
                    CalculationDate = DateTime.Now
                };

                _dbContext.Calculator.Add(resultCalculation);
                _dbContext.SaveChanges();
            }
        }

        private bool IsCalculationSeeded(string operatorName)
        {
            return _dbContext.Calculator.Any(c => c.Operator == operatorName);
        }
    }
}
