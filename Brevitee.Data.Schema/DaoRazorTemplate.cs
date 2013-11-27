using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;
using System.Web.Mvc.Razor;
using Brevitee;
using System.Reflection;
using System.IO;

namespace Brevitee.Data.Schema
{
    public abstract class DaoRazorTemplate<TModel>: RazorBaseTemplate
    {
        public DaoRazorTemplate()
        {
            Generated = new StringBuilder();
        }

        public string Namespace { get; set; }

        public TModel Model { get; set; }

        public void WriteClassProperty(Column column)
        {
            Write(column.RenderClassProperty());
        }

        public void WriteColumnsClassProperty(Column column)
        {
            Write(column.RenderColumnsClassProperty());
        }

        public void WriteForeignKeyColumnProperty(ForeignKeyColumn fk, int num)
        {
            Write(fk.RenderClassProperty(num, Namespace));
        }

        public void WriteAddToChildDaoCollection(ForeignKeyColumn fk)
        {
            Write(fk.RenderAddToChildDaoCollection());
        }

        public void WriteAddToChildDaoCollection(XrefInfo xref)
        {
            Write(xref.RenderAddToChildDaoCollection());
        }

        public void WriteReferencingList(ForeignKeyColumn fk)
        {
            Write(fk.RenderListProperty());
        }

        public void WriteXrefProperty(XrefInfo xref)
        {
            Write(xref.RenderXrefProperty());
        }
    }
}
