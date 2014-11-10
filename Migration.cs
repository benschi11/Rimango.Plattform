using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform
{
    using Orchard.ContentManagement.MetaData;
    using Orchard.Core.Contents.Extensions;
    using Orchard.Data.Migration;

    using Rimango.Plattform.Models;

    public class Migration : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(typeof(PlattformUserPartRecord).Name, table => table
             .ContentPartRecord()
             .Column<string>("FirstName", c => c.WithLength(50))
             .Column<string>("LastName", c => c.WithLength(50))
             .Column<string>("Title", c => c.WithLength(10))
             .Column<DateTime>("CreatedUtc")
             );

            ContentDefinitionManager.AlterPartDefinition("PlattformUserPart", part => part
                .Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("PlattformUser", type => type
                .WithPart("PlattformUserPart")
                .WithPart("UserPart")
                .WithPart("ProfileImagePart"));
            return 1;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable(
                typeof(PlattformUserPartRecord).Name,
                table => table.AddColumn<bool>("ProfileFinished", c => c.WithDefault(false)));
            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition(
                "PlattformUser",
                type => type
                    .RemovePart("CommonPart")
                    .WithPart("UserRolesPart"));

            return 3;
        }
    }
}