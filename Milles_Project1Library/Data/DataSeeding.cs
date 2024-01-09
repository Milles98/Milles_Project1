using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.ShapeStrategyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Data
{
    public class DataSeeding
    {
        private readonly ProjectDbContext _dbContext;

        public DataSeeding(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            _dbContext.Database.Migrate();

            SeedRectangle();
            SeedParallelogram();
            SeedTriangle();
            SeedRhombus();
        }

        private void SeedRectangle()
        {
            var strategy = new RectangleStrategy();
            SetRandomDimensions(strategy);
            SaveResult(strategy);
        }

        private void SeedParallelogram()
        {
            var strategy = new ParallelogramStrategy();
            SetRandomDimensions(strategy);
            SaveResult(strategy);
        }

        private void SeedTriangle()
        {
            var strategy = new TriangleStrategy();
            SetRandomDimensions(strategy);
            SaveResult(strategy);
        }

        private void SeedRhombus()
        {
            var strategy = new RhombusStrategy();
            SetRandomDimensions(strategy);
            SaveResult(strategy);
        }

        private void SetRandomDimensions(IShapeStrategy strategy)
        {
            decimal[] randomDimensions = GenerateRandomDimensions(strategy.GetDimensionCount());
            strategy.SetDimensions(randomDimensions);
        }

        private decimal[] GenerateRandomDimensions(int dimensionCount)
        {
            Random random = new Random();
            decimal[] randomDimensions = new decimal[dimensionCount];

            for (int i = 0; i < dimensionCount; i++)
            {
                randomDimensions[i] = (decimal)random.NextDouble() * 2;
            }

            return randomDimensions;
        }

        private void SaveResult(IShapeStrategy strategy)
        {
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
            _dbContext.SaveChanges();
        }
    }
}
