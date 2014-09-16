using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee;

namespace Brevitee.Messaging
{
    public interface IEmailComposer
    {
        
        Email Compose(string subject, string emailName, params object[] data);
        void SetEmailTemplate(string emailName, FileInfo file);
        void SetEmailTemplate(string emailName, string templateContent);
        string GetEmailBody(string emailName, params object[] data);
    }
}
