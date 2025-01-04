using System.Numerics;

namespace DeedDescriptor.Objects
{
    public class DeedData
    {
        public OwnerTaxID OwnerTaxID;

        public decimal Area { get; set; }

        public string? StreetName { get; set; }
        public string? StreetWidth { get; set; }

        public string? Township { get; set; }
        public string? County{ get; set; }
        public string? State { get; set; }

        public string? ReferencePlan { get; set; }

        public DateTime? Date { get; set; }

        public Dictionary<Guid, OwnerTaxID> AdjoiningPropertyOwners {get;set;}

        public DeedData()
        {
            OwnerTaxID = new OwnerTaxID();
            AdjoiningPropertyOwners = new Dictionary<Guid, OwnerTaxID>();
        }
    }
}
