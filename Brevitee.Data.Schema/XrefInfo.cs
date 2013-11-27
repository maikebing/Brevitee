using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Schema
{
    public sealed class XrefInfo
    {
        public XrefInfo(string parentTableName, string xrefTableName, string listTableName)
        {
            this.ParentTableName = parentTableName;
            this.XrefTableName = xrefTableName;
            this.ListTableName = listTableName;
        }

        public string ParentTableName { get; set; }
        public string XrefTableName { get; set; }
        public string ListTableName { get; set; }
        
        public string RenderXrefProperty()
        {
            RazorParser<DaoRazorTemplate<XrefInfo>> razorParser = new RazorParser<DaoRazorTemplate<XrefInfo>>();
            return razorParser.ExecuteResource("XrefProperty.tmpl", new { Model = this });
        }

        public string RenderAddToChildDaoCollection()
        {
            RazorParser<DaoRazorTemplate<XrefInfo>> razorParser = new RazorParser<DaoRazorTemplate<XrefInfo>>();
            return razorParser.ExecuteResource("ChildXrefCollectionAdd.tmpl", new { Model = this });
        }
    }
}
