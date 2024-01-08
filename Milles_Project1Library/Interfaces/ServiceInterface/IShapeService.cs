using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces.ServiceInterface
{
    public interface IShapeService
    {
        void CreateShape();
        void ReadShapes();
        void UpdateShape();
        void DeleteShape();
    }
}
