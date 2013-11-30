using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerSelectionsValidation: ValidationResult
    {
        public PlayerSelectionsValidation()
        {
            this._messages = new List<string>();
        }

        List<string> _messages;
        public List<string> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
            }
        }

        public void AddMessage(string msg)
        {
            _messages.Add(msg);
            StringBuilder message = new StringBuilder();
            _messages.Each(s =>
            {
                message.AppendLine(s);
            });
            Message = message.ToString();
        }
    }
}
