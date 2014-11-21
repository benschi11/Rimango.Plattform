using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.Blogs.Services;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using Rimango.ProfileImage;

namespace Rimango.Plattform.Forms
{
    public class CreateBlogPostForm : Component, IFormProvider
    {
        protected dynamic New { get; set; }

        private IBlogService _blogService;

        public CreateBlogPostForm(IShapeFactory shapeFactory, IBlogService blogService) {
            New = shapeFactory;
            _blogService = blogService;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, dynamic> formFactory =
                shape =>
                {

                    var form = New.Form(
                        Id: "CreateBlogPostActivtiy",
                        _Type: New.FieldSet(
                            Title: T("Title"),
                            _Title: New.Textbox(
                                Id: Globals.WorkflowParam.Title,
                                Name: Globals.WorkflowParam.Title,
                                Title: T("Title"),
                                Description: T("The Title of the BlogPost."),
                                Classes: new[] { "large", "text", "tokenized" }),
                            _User: New.Textbox(
                                Id: Globals.WorkflowParam.User, Name: Globals.WorkflowParam.User,
                                Title: T("User"),
                                Description: T("The User created this BlogPost."),
                                Classes: new[] { "large", "text", "tokenized" }),
                            _Body: New.Textarea(
                                Id: Globals.WorkflowParam.Body, Name: Globals.WorkflowParam.Body,
                                Title: T("Body"),
                                Description: T("Body-Text of the BlogPost"),
                                Classes: new[] { "tokenized" }),
                            _BlogId: New.SelectList(
                                Id: Globals.WorkflowParam.BlogId, Name: Globals.WorkflowParam.BlogId,
                                Title: T("Blog"),
                                Description: T("The Blog in which the BlogPost is posted."),
                                Size: 10,
                                Multiple: false)
                           
                            ));

                    foreach (var blog in _blogService.Get(VersionOptions.Published))
                    {
                        form._Type._BlogId.Add(new SelectListItem { Value = blog.Id.ToString(), Text = blog.Name });
                    }

                    return form;
                };

            context.Form(Globals.Forms.CreateBlogPostForm, formFactory);
        }
    }

    public class PostOnPageFormValidator : IFormEventHandler
    {
        public Localizer T { get; set; }
        public void Building(BuildingContext context) { }
        public void Built(BuildingContext context) { }
        public void Validated(ValidatingContext context) { }

        public void Validating(ValidatingContext context)
        {
            if (context.FormName != Globals.Forms.CreateBlogPostForm) return;

            var user = context.ValueProvider.GetValue(Globals.WorkflowParam.User).AttemptedValue;
            var title = context.ValueProvider.GetValue(Globals.WorkflowParam.Title).AttemptedValue;
            var body = context.ValueProvider.GetValue(Globals.WorkflowParam.Body).AttemptedValue;
            var blogId = context.ValueProvider.GetValue(Globals.WorkflowParam.BlogId).AttemptedValue;

            if (String.IsNullOrWhiteSpace(user))
            {
                context.ModelState.AddModelError(Globals.WorkflowParam.User, T("The User is a required field.").Text);
            }

            if (String.IsNullOrWhiteSpace(title))
            {
                context.ModelState.AddModelError(Globals.WorkflowParam.Title, T("The Title is a required field.").Text);
            }

            if (String.IsNullOrWhiteSpace(body))
            {
                context.ModelState.AddModelError(Globals.WorkflowParam.Body, T("The Body is a required field.").Text);
            }

            if (String.IsNullOrWhiteSpace(blogId))
            {
                context.ModelState.AddModelError(Globals.WorkflowParam.BlogId, T("Blog is a required field.").Text);
            }
        }
    }
}