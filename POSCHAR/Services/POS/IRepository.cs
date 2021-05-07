using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSCHAR.Services.POS
{

    //CHANGE 2.1

    public interface IRepository
    {

        string GeneratePONumber();
        string GenerateGRNumber();
        string GenerateSONumber();
        string GenerateInvenTranNumber();

    }
}
