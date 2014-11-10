using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.Localization;
    using Orchard.Mvc;
    using Orchard.Themes;

    using Rimango.Plattform.Services;
    using Rimango.Plattform.ViewModels;
    using Rimango.ProfileImage.Models;

    public class UserController : Controller
    {
        private readonly IOrchardServices _orchardServices;

        private readonly IPlattformService _plattformService;
        private Localizer T { get; set; }

        public UserController(
            IOrchardServices orchardServices,
            IPlattformService plattformService)
        {
            _orchardServices = orchardServices;
            _plattformService = plattformService;
        }

        [Themed]
        public ActionResult Register()
        {
            var contentManager = _orchardServices.ContentManager;      

            var shape = _orchardServices.New.User_Register();
            return new ShapeResult(this, shape);
        }

        [Themed]
        [HttpPost]
        public ActionResult Register(RegisterViewModel regForm)
        {
            if (!ModelState.IsValid)
                return new ShapeResult(this, _orchardServices.New.User_Register(regFrom: regForm));

            var plattformUserPart = _plattformService.CreateUser(regForm.Email, regForm.Password);

            return RedirectToAction(
                "ChallengeEmailSent",
                "Account",
                new { area = "Orchard.Users" });

            //return RedirectToRoute(new { Area = "Orchard.User", Controller = "Account", Action = "ChallengeEmailSent" });
        }

        //[Themed]
        //public ActionResult RegisterEditProfile()
        //{
        //    var contentManager = _orchardServices.ContentManager;
        //    var img = contentManager.New("PlattformUser");
        //    var profileImageShape = _orchardServices.ContentManager.BuildEditor(img.As<ProfileImagePart>());


        //}
    }
}