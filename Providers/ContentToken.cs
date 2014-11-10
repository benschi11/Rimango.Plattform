using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Providers
{
    using Orchard.ContentManagement;
    using Orchard.Localization;
    using Orchard.Tokens;

    using Rimango.Plattform.Models;
    using Rimango.ProfileImage.Models;

    public class ContentToken : ITokenProvider
    {
        private IContentManager _contentManager;

        public Localizer T { get; set; }

        public ContentToken(IContentManager contentManger)
        {
            _contentManager = contentManger;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            context.For("Content", T("Content Items"), T("Content Items"))
                .Token("PlattformUserPart", T("PlattformUserPart"), T("Gets the PlattformUserPart"));

            context.For("PlattformUserPart", T("PlattformUserPart"), T("PlattformUserPart"))
                .Token(
                    "ProfileFinished",
                    T("ProfileFinished"),
                    T("Gets the Property 'ProfileFinished' from PlattformUserPart"));
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For<IContent>("Content")
                .Token("PlattformUserPart", content => content.As<PlattformUserPart>() ?? null)
                .Chain("PlattformUserPart", "PlattformUserPart", content => content.As<PlattformUserPart>());

            context.For<PlattformUserPart>("PlattformUserPart").Token("ProfileFinished", p => p.ProfileFinished);
        }
    }
}