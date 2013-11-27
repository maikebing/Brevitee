using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Schema
{
    public Schema()
    {
        this._tables = new List<Tbl>();
    }

    List<Tbl> _tables;
    public Tbl[] Tables
    {
        get
        {
            return _tables.ToArray();
        }
        set
        {
            _tables.Clear();
            _tables.AddRange(value);
        }
    }

    public void AddTable(Tbl tbl)
    {
        _tables.Add(tbl);
    }
}

