using NuGet.Protocol.Core.Types;

namespace TemDeTudo.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public Decimal Salary { get; set; }

        public int DepatmentId { get; set; }

        public Depatment Depatment { get; set; }

        public List<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
    }


}
