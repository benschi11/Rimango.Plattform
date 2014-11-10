using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Drivers
{
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.Localization;
    using Orchard.UI.Notify;

    using Rimango.Plattform.Models;

    public class PlattformUserPartDriver : ContentPartDriver<PlattformUserPart>
    {
        private readonly INotifier _notifier;
        public Localizer T { get; set; }
        public PlattformUserPartDriver(INotifier notifier)
        {
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }
        protected override string Prefix
        {
            get { return "PlattformUser"; }
        }

        protected override DriverResult Editor(PlattformUserPart part, dynamic shapeHelper)
        {
            if(string.IsNullOrWhiteSpace(part.FirstName) || string.IsNullOrWhiteSpace(part.LastName))
                _notifier.Warning(T("Please add the required information to your profile"));

            return ContentShape("Parts_PlattformUser_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/PlattformUser", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(PlattformUserPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                part.ProfileFinished = true;
            }
            return Editor(part, shapeHelper);
        }
    }
}