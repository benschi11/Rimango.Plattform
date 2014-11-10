using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Models
{
    using System.ComponentModel.DataAnnotations;

    using Orchard.ContentManagement;

    public class PlattformUserPart : ContentPart<PlattformUserPartRecord>
    {
        [Required]
        public string FirstName
        {
            get { return Record.FirstName; }
            set { Record.FirstName = value; }
        }

        [Required]
        public string LastName
        {
            get { return Record.LastName; }
            set { Record.LastName = value; }
        }

        public string Title
        {
            get { return Record.Title; }
            set { Record.Title = value; }
        }

        public DateTime CreatedUtc
        {
            get { return Record.CreatedUtc; }
            set { Record.CreatedUtc = value; }
        }

        public bool ProfileFinished
        {
            get
            {
                return Record.ProfileFinished;
            }
            set
            {
                Record.ProfileFinished = value;
            }
        }
    }
}