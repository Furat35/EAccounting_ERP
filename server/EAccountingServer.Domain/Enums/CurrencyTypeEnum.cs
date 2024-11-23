using Ardalis.SmartEnum;

namespace EAccountingServer.Domain.Enums
{
    public class CurrencyTypeEnum(string name, int value)
        : SmartEnum<CurrencyTypeEnum>(name, value)
    {
        public static readonly CurrencyTypeEnum TL = new("TL", 1);
        public static readonly CurrencyTypeEnum USD = new("USD", 2);
        public static readonly CurrencyTypeEnum EU = new("EU", 3);
    }
}
