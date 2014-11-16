using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Blogs.Models;
using Orchard.Blogs.Services;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Localization;
using Orchard.Security;
using Orchard.Workflows.Models;
using Orchard.Workflows.Services;

namespace Rimango.Plattform.Activities
{
    public class CreateBlogPostActivity : Task {
        private IContentManager _contentManager;
        private IMembershipService _membershipService;
        private IBlogService _blogService;
        public CreateBlogPostActivity(IContentManager contentManager, IMembershipService membershipService, IBlogService blogService) {
            _contentManager = contentManager;
            _membershipService = membershipService;
            _blogService = blogService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override string Name {
            get { return "CreateBlogPost"; }
        }



        public override LocalizedString Category {
            get { return T("Content"); }
        }

        public override LocalizedString Description {
            get { return T("Creates a new BlogPost"); }
        }

        public override IEnumerable<LocalizedString> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext) {
            return new[] {T("Success"), T("Error")};
        }

        public override string Form {
            get { return Globals.Forms.CreateBlogPostForm; }
        }

        public override IEnumerable<LocalizedString> Execute(WorkflowContext workflowContext, ActivityContext activityContext) {
            LocalizedString outcome = null;
            try {
                var title = activityContext.GetState<string>(Globals.WorkflowParam.Title);
                var body = activityContext.GetState<string>(Globals.WorkflowParam.Body);
                var user = activityContext.GetState<string>(Globals.WorkflowParam.User);

                var blogpost = _contentManager.New("BlogPost");

                blogpost.As<TitlePart>().Title = title;
                blogpost.As<BodyPart>().Text = body;
                blogpost.As<CommonPart>().Owner = _membershipService.GetUser(user);
                blogpost.As<BlogPostPart>().BlogPart = _blogService.Get(17, VersionOptions.Published).As<BlogPart>();

                _contentManager.Create(blogpost, VersionOptions.Published);
                outcome = T("Success");

            }
            catch (Exception exc) {
                workflowContext.SetState("Exception", exc);
                outcome = T("Error");
            }

            yield return outcome;

        }
    }
}