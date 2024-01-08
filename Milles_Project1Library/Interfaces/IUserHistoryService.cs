using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces
{
    public interface IUserHistoryService
    {
        public void ShowUserHistory();
    }
}
