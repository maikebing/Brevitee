using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class Tbl
    {
        public Tbl() { }
        
        public Tbl(string name)
        {
            this.Name = name;
            this.Conx = "Default";
        }

        public Tbl(string name, string connectionName)
            : this(name)
        {
            this.Conx = connectionName;
        }
        
        Col _pk;
        public Col Pk
        {
            get
            {
                return _pk;
            }
            set
            {
                _pk = value;
                if (_pk != null)
                {
                    _pk.Empty = false;
                }
            }
        }

        List<Col> _columns = new List<Col>();
        public Col[] Cols
        {
            get
            {
                return _columns.ToArray();
            }
            set
            {
                _columns = new List<Col>(value);
            }
        }

        List<Fk> _foreignKeys = new List<Fk>();
        public Fk[] Fks
        {
            get
            {
                return _foreignKeys.ToArray();
            }
            set
            {
                _foreignKeys = new List<Fk>(value);
            }
        }
        
        public string Name { get; set; }

        internal string Conx { get; set; }

        public void AddColumn(string name)
        {
            AddColumn(new Col { Name = name });
        }

        public void AddColumn(Col column)
        {
            _columns.Add(column);
        }

        public void AddFk(string referencesTable, string name, bool allowNull = true)
        {
            _foreignKeys.Add(new Fk { Empty = allowNull, Name = name, Ref = referencesTable });
        }
    }
