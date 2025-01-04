namespace DeedDescriptor.Objects
{
    public class OwnerTaxID
    {
        public string? Name { get; set; }
        public bool IsTaxMap { get; set; }
        public string? IDNumber { get; set; }

        public void CopyData(OwnerTaxID ownerTaxID)
        {
            Name = ownerTaxID.Name;
            IDNumber = ownerTaxID.IDNumber;
            IsTaxMap = ownerTaxID.IsTaxMap;
        }
    }
}
