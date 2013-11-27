using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class OpenParen: FilterToken
    {
        public OpenParen()
        {
        }

        public override string ToString()
        {
            return "(";
        }
    }
}
