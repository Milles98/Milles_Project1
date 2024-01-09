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
            SetSpecificDimensions(strategy, new decimal[] { 4.0M, 6.0M });
            SaveResult(strategy);
        }

        private void SeedParallelogram()
        {
            var strategy = new ParallelogramStrategy();
            SetSpecificDimensions(strategy, new decimal[] { 5.0M, 7.0M, 30.0M });
            SaveResult(strategy);
        }

        private void SeedTriangle()
        {
            var strategy = new TriangleStrategy();
            SetSpecificDimensions(strategy, new decimal[] { 3.0M, 4.0M, 5.0M });
            SaveResult(strategy);
        }

        private void SeedRhombus()
        {
            var strategy = new RhombusStrategy();
            SetSpecificDimensions(strategy, new decimal[] { 8.0M, 60.0M });
            SaveResult(strategy);
        }

        private void SetSpecificDimensions(IShapeStrategy strategy, decimal[] specificDimensions)
        {
            strategy.SetDimensions(specificDimensions);
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
