using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Models
{
    using Orchard.ContentManagement.Records;

    public class PlattformUserPartRecord : ContentPartRecord
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Title { get; set; }
        public virtual DateTime CreatedUtc { get; set; }
        public virtual bool ProfileFinished { get; set; }
    }
}