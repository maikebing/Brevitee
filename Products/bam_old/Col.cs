using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data.Schema;


    public class Col
    {
        public Col()
        {
            this.Type = DataTypes.String;
            this.Empty = true;            
        }

        public DataTypes Type { get; set; }        
        public bool Empty { get; set; }
        public string Name { get; set; }
    }

