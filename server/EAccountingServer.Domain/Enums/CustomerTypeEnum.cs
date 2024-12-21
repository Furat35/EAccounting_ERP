using Ardalis.SmartEnum;

namespace EAccountingServer.Domain.Enums
{
    public sealed class CustomerTypeEnum(string name, int value) : SmartEnum<CustomerTypeEnum>(name, value)
    {
        public static readonly CustomerTypeEnum Alicilar = new("Ticari Alıcılar", 1);
        public static readonly CustomerTypeEnum Saticilar = new("Ticari Satıcılar", 2);
        public static readonly CustomerTypeEnum Personel = new("Personel", 3);
        public static readonly CustomerTypeEnum Ortaklar = new("Şirket Ortakları", 4);
    }
}
