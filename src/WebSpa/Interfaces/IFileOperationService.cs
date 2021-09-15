using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSpa.Models;

namespace WebSpa.Interfaces
{
    public interface IFileOperationService
    {
        ReadFromDatesFileResponse getDatesFromFile(string dataFileName);
    }
}
