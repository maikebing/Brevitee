using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
    public class UpdateResult<T>
    {
        public UpdateResult(T target)
        {
            this.Success = true;
            this.Target = target;
        }

        public UpdateResult(Exception ex)
        {
            this.Success = false;
            this.Message = "{0}\r\n\r\n{1}"._Format(ex.Message, ex.StackTrace);            
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Target { get; set; }
    }
}
