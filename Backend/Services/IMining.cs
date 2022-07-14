using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.models;

namespace Backend.Interfaces
{
    public interface IMining
    {
        string GetSHA256(string str);
        
    }
}
