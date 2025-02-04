using Humanizer;
using Radzen.Blazor.Rendering;
using System.Text;

namespace DeedDescriptor.Objects
{
    public class Line : Shape
    {
        public string FirstValue { get; set; }
        public decimal Distance { get; set; }
        public Bearing Bearing { get; set; }
        public bool PreceedsCurve { get; set; }

        public Line() { Name = "Line"; }
        public Line(string point, string description)
        {
            Name = "Line";
            Point = point;
            Description = description;
        }
        public Line(string point, string firstValue, char primaryDirection, int degrees, int minutes, int seconds, char direction, decimal distance, string description)
        {
            Name = "Line";
            Point = point;
            FirstValue = firstValue;
            Bearing = new Bearing(primaryDirection, degrees, minutes, seconds, direction);
            Distance = distance;
            Description = description;
        }
        public Line(string point, string description, string lineData)
        {
            Name = "Line";
            Point = point;
            Description = description;
			PreceedsCurve = lineData.Contains("To Cntr. Pt.");
            var escapedLine = lineData.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace("\v", " ").Replace("\f", " ").Replace("To Cntr. Pt.", " ");
            var lineSegments = escapedLine.Trim().Split(' ', 7, StringSplitOptions.RemoveEmptyEntries);

            FirstValue = lineSegments[0];

            Bearing = new Bearing(lineSegments.Skip(1).Take(5).ToArray());

            if (!PreceedsCurve)
            {
                Distance = decimal.Parse(lineSegments[6]);
            }
        }

        public override void Clone(Shape shape)
        {
            var lineObj = (Line)shape;
            Point = lineObj.Point;
            Description = lineObj.Description;
            FirstValue = lineObj.FirstValue;
			Bearing = new Bearing(lineObj.Bearing);
            Distance = lineObj.Distance;
        }

        public override string TranslateToDeedDescription()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"a bearing{(PreceedsCurve ? ", that is radial to the curve," : "")} of {Bearing.TranslateToDeedDescription()}");

            var distanceValues = Distance.ToString().Split('.');
            var distanceValue1 = int.Parse(distanceValues[0]);
            var distanceString1 = string.Empty;

            if (Distance > 0)
            {
                if (distanceValue1 > 100)
                {
                    var distance1Values = ((decimal)distanceValue1 / 100).ToString().Split('.');
                    var hundredsString = (int.Parse(distance1Values[0]) * 100).ToWords();
                    var tensString = (distance1Values.Length > 1 ? (int.Parse(distance1Values[1])).ToWords() : "");
                    distanceString1 = $"{hundredsString} {tensString}";
                }
                else
                {
                    distanceString1 = int.Parse(distanceValues[0]).ToWords();
                }

                decimal distanceValue2 = Decimal.Parse(distanceValues[1]);

                if (distanceValue2 > 100)
                {
                    distanceValue2 = Math.Round(distanceValue2 / 100 * 10, MidpointRounding.AwayFromZero);
                }
                var distanceValue2Int = distanceValue2;
                string distanceString2 = ((int)distanceValue2).ToWords();


                stringBuilder.Append($"with a distance of {distanceString1} and {(distanceValues[1] == "00" ? "no" : distanceString2)} one-hundredths ({distanceValues[0]}.{(distanceValue2Int == 0 ? "00" : distanceValue2)}) feet ");
            }
            var isFirstLetterVowelOrY = HelperCollections.VowelList.Contains(Description.First());
            stringBuilder.Append($"to {(isFirstLetterVowelOrY ? "an" : "a")} {Description}; ");

            return stringBuilder.ToString();
        }
    }
}
