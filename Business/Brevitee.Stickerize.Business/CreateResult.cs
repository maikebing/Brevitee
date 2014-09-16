using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
    public class CreateResult<T>
    {
		public static implicit operator T(CreateResult<T> result)
		{
			return result.Result;
		}

		public static implicit operator CreateResult<T>(T result)
		{
			return new CreateResult<T>(result);
		}

        public CreateResult()
        {
            this.Success = false;
            this.Message = "This result is unitialized";            
        }

        public CreateResult(T result)
        {
            this.Result = result;
            this.Success = true;
            this.Status = CreateStatus.Success;
        }

        public CreateResult(Exception ex)
        {
            this.Message = "{0}\r\n\r\n{1}"._Format(ex.Message, ex.StackTrace);
            this.Success = false;
            this.Status = CreateStatus.Error;
        }

        public CreateStatus Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
