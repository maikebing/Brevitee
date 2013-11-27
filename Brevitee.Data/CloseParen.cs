using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class CloseParen: FilterToken
    {
        public CloseParen()
        { }

        public override string ToString()
        {
            return ")";
        }
    }
}
