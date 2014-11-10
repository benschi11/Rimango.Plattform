using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Handlers
{
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;
    using Orchard.Users.Models;

    using Rimango.Plattform.Models;
    using Rimango.ProfileImage.Models;

    public class PlattformUserPartHandler : ContentHandler
    {
        public PlattformUserPartHandler(IRepository<PlattformUserPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<UserPart>("PlattformUser"));

        }
    }
}