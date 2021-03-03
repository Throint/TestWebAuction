using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TestRazor.Model;

namespace TestRazor.Services
{ 

    public interface ICheck
    {
          Task DoWork(CancellationToken cancellationToken);
    }
}
    