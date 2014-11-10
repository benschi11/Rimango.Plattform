using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimango.Plattform.Services
{
    using Orchard;

    using Rimango.Plattform.Models;

    public interface IPlattformService : IDependency
    {
        PlattformUserPart CreateUser(string email, string password);
    }
}
