namespace DeedDescriptor.Objects
{
    public abstract class Shape : IDescriptionTranslator
    {
        public string Point { get; set; }

        public string? Description { get; set; }

        internal string? Name { get; set; }

        public Guid OwnerID { get; set; }

        public virtual void Clone(Shape shape) { }

        public abstract string TranslateToDeedDescription();
    }
}
