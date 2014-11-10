using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Services
{
    using System.Web.Routing;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.Mvc.Extensions;
    using Orchard.Security;
    using Orchard.Services;
    using Orchard.Users.Events;
    using Orchard.Users.Models;
    using Orchard.Users.Services;
    using Orchard.Utility.Extensions;
    using System.Web.Mvc;

    using Rimango.Plattform.Models;

    public class PlattformService : IPlattformService
    {
        private readonly IContentManager _contentManager;

        private readonly IOrchardServices _orchardServices;

        private readonly IUserService _userService;

        private readonly IUserEventHandler _userEventHandler;

        private readonly IClock _clock;

        private readonly IMembershipService _membershipService;

        public PlattformService(
            IContentManager contentManager, 
            IOrchardServices orchardServices,
            IUserService userService,
            IUserEventHandler userEventHandler,
            IClock clock,
            IMembershipService membershipService)
        {
            _contentManager = contentManager;
            _orchardServices = orchardServices;
            _userService = userService;
            _userEventHandler = userEventHandler;
            _clock = clock;
            _membershipService = membershipService;

        }
        public PlattformUserPart CreateUser(string email, string password)
        {
            var user = _contentManager.New("PlattformUser");

            var plattformUserPart = user.As<PlattformUserPart>();

            plattformUserPart.CreatedUtc = _clock.UtcNow;

            var userPart = user.As<UserPart>();

            userPart.Email = email;
            userPart.UserName = email;
            userPart.NormalizedUserName = email.ToLowerInvariant();
            userPart.HashAlgorithm = "SHA1";
            userPart.RegistrationStatus = UserStatus.Approved;
            userPart.EmailStatus = UserStatus.Pending;

            _membershipService.SetPassword(userPart, password);

            _contentManager.Create(user);

            var siteUrl = _orchardServices.WorkContext.CurrentSite.BaseUrl;

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            
            _userService.SendChallengeEmail(user.As<UserPart>(), nonce => url.MakeAbsolute(url.Action("ChallengeEmail", "Account", new { Area = "Orchard.Users", nonce = nonce }), siteUrl));

            _userEventHandler.SentChallengeEmail(userPart);

            return plattformUserPart;

        }
    }
}