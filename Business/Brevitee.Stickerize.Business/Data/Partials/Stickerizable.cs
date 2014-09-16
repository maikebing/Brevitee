using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;
using Brevitee.Configuration;
using Brevitee.Logging;
using Brevitee;
using Brevitee.Stickerize.Business;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class Stickerizable
    {
        public const string DefaultName = "the heck of it";

        static Stickerizable _defaultStickerizable;
        static object _defaultLock = new object();
        public static Stickerizable Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _defaultStickerizable, () => GetByName(DefaultName));
            }
        }

        
        public Stickerization Stickerize(DateTime at, long stickerId, long stickerizeeId)
        {
            return Stickerize(
                at,
                Sticker.OneWhere(c => c.Id == stickerId), 
                Stickerizee.OneWhere(c => c.Id == stickerizeeId));
        }

        public Stickerization Stickerize(DateTime at, Sticker sticker, Stickerizee stickerizee)
        {
            Args.ThrowIfNull(sticker, "sticker");
            Args.ThrowIfNull(stickerizee, "stickerizee");

            return Stickerization.Create(at, sticker.Id.Value, this.Id.Value, stickerizee.Id.Value);
        }

        public static Stickerizable GetByName(string name)
        {
            CreateResult<Stickerizable> create = Create(name);
            if (create.Status == CreateStatus.Error)
            {
                throw new Exception(create.Message);
            }

            return create.Result;
        }

        /// <summary>
        /// Creates and returns a stickerizable with the specified
        /// info or returns an existing one with the same name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="_for"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static CreateResult<Stickerizable> Create(string name, string _for = null, string description = null)
        {
            if (string.IsNullOrEmpty(_for))
            {
                _for = name;
            }
            Stickerizable it = Stickerizable.OneWhere(c => c.Name == name);
            CreateResult<Stickerizable> result = new CreateResult<Stickerizable>();

            try
            {
                if (it == null)
                {
                    it = new Stickerizable();
                    it.Name = name;
                    it.For = _for;
                    it.Description = description;
                    it.Created = DateTime.UtcNow.Date;
                    it.Save();
                    result = new CreateResult<Stickerizable>(it);
                }
                else
                {
                    result = new CreateResult<Stickerizable>(it);
                    result.Status = CreateStatus.Duplicate;
                }
            }
            catch (Exception ex)
            {
                result = new CreateResult<Stickerizable>(ex);
            }

            return result;
        }
    }
}
