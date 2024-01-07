using Autofac;
using Milles_Project1.Menus;
using Milles_Project1Library.Data;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1
{
    public class App
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IShapeContext _shapeContext;
        private readonly ICalculatorContext _calculatorContext;
        private readonly ICalculatorService _calculatorService;
        public App(ProjectDbContext dbContext, IShapeContext shapeContext, ICalculatorContext calculatorContext, ICalculatorService calculatorService)
        {
            _dbContext = dbContext;
            _shapeContext = shapeContext;
            _calculatorContext = calculatorContext;
            _calculatorService = calculatorService;
        }
        public void RunApplication()
        {
            while (true)
            {
                MainMenu.ShowMenu(_dbContext, _shapeContext, _calculatorContext, _calculatorService);
            }
        }
    }
}
