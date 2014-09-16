using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
    public class RetrieveResult<T>
    {
        public RetrieveResult()
        {
            this.Success = false;
            this.Message = "This result is unitialized";            
        }

        public RetrieveResult(T[] result)
        {
            this.Result = result;
            this.Success = true;
            this.Status = RetrieveStatus.Success;
        }

        public RetrieveResult(Exception ex)
        {
            this.Message = "{0}\r\n\r\n{1}"._Format(ex.Message, ex.StackTrace);
            this.Success = false;
            this.Status = RetrieveStatus.Error;
        }

        public RetrieveStatus Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T[] Result { get; set; }
    }
}
