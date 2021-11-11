using System.Runtime.Serialization;

namespace Data.Specification
{
    public enum PriceSorting
    {
        [EnumMember(Value = "No Order")]
        NoOrder,
        [EnumMember(Value = "Ascending")]
        Ascending,
        [EnumMember(Value = "Descending")]
        Descending
    }
}
