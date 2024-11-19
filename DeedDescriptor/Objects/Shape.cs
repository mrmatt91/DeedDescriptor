namespace DeedDescriptor.Objects
{
    public class Shape
    {
        public string Point { get; set; }

        public string? Description { get; set; }

        public virtual void Clone(Shape shape) { }

        public virtual string ShapeToTextTranslation() 
        {
            return "";
        }
    }
}
