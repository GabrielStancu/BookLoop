using System.Runtime.Serialization;

namespace Data.Specification
{
    public enum OfferType
    {
        [EnumMember(Value = "Any Offer")]
        AnyOffer,
        [EnumMember(Value = "Sell")]
        Sell,
        [EnumMember(Value = "Exchange")]
        Exchange,
        [EnumMember(Value = "Donation")]
        Donation
    }
}
