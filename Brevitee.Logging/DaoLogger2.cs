using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Logging
{
    /// <summary>
    /// A database logger that uses a 
    /// a schema that will grow less over
    /// time by breaking out the parts
    /// of the event into separate tables
    /// </summary>
    public class DaoLogger2: Logger
    {
        public override void CommitLogEvent(LogEvent logEvent)
        {
            SourceName source = GetSource(logEvent);
            CategoryName category = GetCategory(logEvent);
            UserName user = GetUser(logEvent);
            ComputerName computer = GetComputer(logEvent);
            Signature signature = GetSignature(logEvent);

            Event instance = new Event();
            instance.SignatureId = signature.Id;
            instance.ComputerId = computer.Id;
            instance.CategoryId = category.Id;
            instance.SourceId = source.Id;
            instance.UserId = user.Id;
            instance.EventId = logEvent.EventID;
            instance.Occurred = logEvent.Time;
            instance.Severity = (int)logEvent.Severity;
            instance.Save();
            logEvent.MessageVariableValues.Each((val, pos)=>
            {
                Param p = instance.Params.AddNew();
                p.Position = pos;
                p.Value = val;
            });
            instance.Save();
        }

        private static Signature GetSignature(LogEvent logEvent)
        {
            Signature signature = Signature.OneWhere(sc => sc.Value == logEvent.MessageSignature);
            if (signature == null)
            {
                signature = new Signature();
                signature.Value = logEvent.MessageSignature;
                signature.Save();
            }
            return signature;
        }

        private static ComputerName GetComputer(LogEvent logEvent)
        {
            ComputerName computer = ComputerName.OneWhere(cc => cc.Value == logEvent.User);
            if (computer == null)
            {
                computer = new ComputerName();
                computer.Value = logEvent.Computer;
                computer.Save();
            }
            return computer;
        }

        private static UserName GetUser(LogEvent logEvent)
        {
            UserName user = UserName.OneWhere(uc => uc.Value == logEvent.User);
            if (user == null)
            {
                user = new UserName();
                user.Value = logEvent.User;
                user.Save();
            }
            return user;
        }

        private static CategoryName GetCategory(LogEvent logEvent)
        {
            CategoryName category = CategoryName.OneWhere(cc => cc.Value == logEvent.Source);
            if (category == null)
            {
                category = new CategoryName();
                category.Value = logEvent.Category;
                category.Save();
            }
            return category;
        }

        private static SourceName GetSource(LogEvent logEvent)
        {
            SourceName source = SourceName.OneWhere(sc => sc.Value == logEvent.Source);
            if (source == null)
            {
                source = new SourceName();
                source.Value = logEvent.Source;
                source.Save();
            }
            return source;
        }
    }
}
