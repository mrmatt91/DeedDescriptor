using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;

namespace DeedDescriptor.Objects
{
    public class Curve : Shape
    {
        public decimal? Arc { get; set; }

        public string? CentralAngle { get; set; }
        public decimal? Chord { get; set; }
        public Bearing ChordBearing { get; set; }
        public decimal? Radius { get; set; }
        public Bearing BearingToCenterPoint { get; set; }

        public decimal? Tangent { get; set; }

        public Curve() { }

        public Curve(string point, string description)
        {
            Point = point;
            Description = description;
        }

        public Curve(string point, string description, string curveLine1, string curveLine2, string curveLine3, string curveLine4)
            : this(point, description)
        {
            var curveLine1Parts = curveLine1.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Arc = decimal.Parse(curveLine1Parts[1].Replace("Arc", ""));
            CentralAngle = curveLine1Parts[4];

            var curveLine2Parts = curveLine2.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Chord = decimal.Parse(curveLine2Parts[1].Replace("Chord", ""));
            ChordBearing = new Bearing(string.Join(" ", new string[] { curveLine2Parts[4], curveLine2Parts[5], curveLine2Parts[6], curveLine2Parts[7], curveLine2Parts[8] }));

            var curveLine3Parts = curveLine3.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Radius = decimal.Parse(curveLine3Parts[1].Replace("Radius", ""));
            BearingToCenterPoint = new Bearing(string.Join(" ", new string[] { curveLine3Parts[6], curveLine3Parts[7], curveLine3Parts[8], curveLine3Parts[9], curveLine3Parts[10] }));

            Tangent = decimal.Parse(curveLine4.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[1]);
        }

        public Curve(string point, string description, string? arc, string? centralAngle, string? chord, string chordBearing, string? radius, string bearingToCenterPoint, string? tangent)
        {
            Point = point;
            Description = description;
            Arc = decimal.Parse(arc.Replace("Arc", ""));
            CentralAngle = centralAngle;
            Chord = decimal.Parse(chord.Replace("Chord", ""));
            ChordBearing = new Bearing(chordBearing);
            Radius = decimal.Parse(radius.Replace("Radius", ""));
            BearingToCenterPoint = new Bearing(bearingToCenterPoint);
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

        public override string TranslateToDeedDescription()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Proceeding along a curve extending to the {DetermineCurveDirection(BearingToCenterPoint.GetDegrees(), ChordBearing.GetDegrees())} ");
            stringBuilder.Append($"with a radius of {Radius.Format()} feet, having an arc length of {Arc.Format()} feet, with a central angle of {CentralAngle}, and having a chord bearing & distance of {ChordBearing}, {Chord.Format()} feet, to a point of tangency; ");
            return stringBuilder.ToString();
        }
        public static string DetermineCurveDirection(double centerBearingDegrees, double chordBearingDegrees)
        {
            // Normalize the bearings to 0-360 degrees
            centerBearingDegrees = (centerBearingDegrees + 360) % 360;
            chordBearingDegrees = (chordBearingDegrees + 360) % 360;

            // Calculate the difference between the chord bearing and the center point bearing
            double bearingDifference = chordBearingDegrees - centerBearingDegrees;

            // Normalize the bearing difference to the range -180 to 180
            if (bearingDifference > 180)
            {
                bearingDifference -= 360;
            }
            else if (bearingDifference < -180)
            {
                bearingDifference += 360;
            }

            // Determine if the curve is extending to the right or left
            return $"extending to the {(bearingDifference > 0 ? "right" : "left")}";
        }

    }
}
