namespace DeedDescriptor.Objects
{
    public class Curve : Shape
    {
        public decimal? Arc { get; set; }
        public decimal? Chord { get; set; }
        public decimal? Radius { get; set; }

        public decimal? Tangent { get; set; }

        public Curve(string point, string description)
        {
            Point = point;
            Description = description;
        }

        public Curve() { }

        public Curve(string point, string description, string? arc, string? chord, string? radius, string? tangent)
        {
            Point = point;
            Description = description;
            Arc = decimal.Parse(arc.Replace("Arc",""));
            Chord = decimal.Parse(chord.Replace("Chord", ""));
            Radius = decimal.Parse(radius.Replace("Radius", ""));
            Tangent = decimal.Parse(tangent.Replace("Tangent", ""));
        }

        public override void Clone(Shape shape)
        {
            var curveObj = (Curve)shape;
            Point = curveObj.Point;
            Description = curveObj.Description;
            Arc = curveObj.Arc;
            Chord = curveObj.Chord;
            Radius = curveObj.Radius;
            Tangent = curveObj.Tangent;
        }

        public override string ShapeToTextTranslation()
        {
            return base.ShapeToTextTranslation();
        }
    }
}
