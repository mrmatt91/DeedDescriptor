namespace DeedDescriptor.Objects
{
    public class Curve : Shape
    {
        public string? Arc { get; set; }
        public string? Chord { get; set; }
        public string? Radius { get; set; }

        public string? Tangent { get; set; }

        public Curve(string point, string description)
        {
            Point = point;
            Description = description;
        }

        public Curve() { }

        public Curve(string? arc, string? chord, string? radius, string? tangent)
        {
            Arc = arc;
            Chord = chord;
            Radius = radius;
            Tangent = tangent;
        }
    }
}
