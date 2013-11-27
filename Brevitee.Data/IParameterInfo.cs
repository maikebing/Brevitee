using System;
namespace Brevitee.Data
{
    public interface IParameterInfo: IFilterToken
    {
        string ColumnName { get; set; }
        int? Number { get; set; }
        int? SetNumber(int? value);
        object Value { get; set; }
    }
}
