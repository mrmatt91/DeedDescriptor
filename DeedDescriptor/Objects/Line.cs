using Humanizer;
using Radzen.Blazor.Rendering;
using System.Text;

namespace DeedDescriptor.Objects
{
    public class Line : Shape
    {
        public string FirstValue { get; set; }
        public string Direction1 { get; set; }
        public string Degrees { get; set; }
        public string Minutes { get; set; }
        public string Seconds { get; set; }
        public string Direction2 { get; set; }

        public decimal Distance { get; set; }

        public Line() { }
        public Line(string point, string description)
        {
            Point = point;
            Description = description;
        }
        public Line(string point, string firstValue, string direction1, string degrees, string minutes, string seconds, string direction2, decimal distance, string description)
        {
            Point = point;
            FirstValue = firstValue;
            Direction1 = direction1;
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Direction2 = direction2;
            Distance = distance;
            Description = description;
        }
        public Line(string point, string description, string lineData)
        {
            Point = point;
            Description = description;
            var preceedsCurve = lineData.Contains("To Cntr. Pt.");
            var escapedLine = lineData.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace("\v", " ").Replace("\f", " ").Replace("To Cntr. Pt.", " ");
            var lineSegments = escapedLine.Trim().Split(' ', 7, StringSplitOptions.RemoveEmptyEntries);
            FirstValue = lineSegments[0];
            Direction1 = lineSegments[1];
            Degrees = lineSegments[2].Substring(0, lineSegments[2].Length - 1);
            Minutes = lineSegments[3].Substring(0, lineSegments[3].Length - 1);
            Seconds = lineSegments[4].Substring(0, lineSegments[4].Length - 1);
            Direction2 = lineSegments[5];
            if (!preceedsCurve)
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
            Direction1 = lineObj.Direction1;
            Degrees = lineObj.Degrees;
            Minutes = lineObj.Minutes;
            Seconds = lineObj.Seconds;
            Direction2 = lineObj.Direction2;
            Distance = lineObj.Distance;
        }

        public override string ShapeToTextTranslation()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("a bearing of ");
            stringBuilder.Append($"{HelperCollections.DirectionMap[Direction1]} ");
            stringBuilder.Append($"{int.Parse(Degrees).ToWords()} ({Degrees}) degrees ");
            stringBuilder.Append($"{int.Parse(Minutes).ToWords()} ({Minutes}) minutes ");
            stringBuilder.Append($"{int.Parse(Seconds).ToWords()} ({Seconds}) seconds ");
            stringBuilder.Append($"{HelperCollections.DirectionMap[Direction2]} ");

            var distanceValues = Distance.ToString().Split('.');
            var distanceValue1 = int.Parse(distanceValues[0]);
            var distanceString1 = string.Empty;

            if (Distance > 0)
            {
                if (distanceValue1 > 100)
                {
                    var distance1Values = ((decimal)distanceValue1 / 100).ToString().Split('.');
                    var hundredsString = (int.Parse(distance1Values[0]) * 100).ToWords();
                    var decimalValue = (distance1Values.Length > 1 ? (int.Parse(distance1Values[1])).ToWords() : "");
                    distanceString1 = $"{hundredsString} {decimalValue}";
                }
                else
                {
                    distanceString1 = int.Parse(distanceValues[0]).ToWords();
                }

                var distanceString2 = string.Empty;
                decimal distanceValue2 = Decimal.Parse(distanceValues[1]);

                if (distanceValue2 > 100)
                {
                    distanceValue2 = Math.Round(((distanceValue2 / 100) * 10), MidpointRounding.AwayFromZero);
                }

                var distanceValue2Int = distanceValue2;
                distanceString2 = ((int)distanceValue2Int).ToWords();

                stringBuilder.Append($"with a distance of {distanceString1} and {(distanceValues[1] == "00" ? "no" : distanceString2)} one-hundredths ({distanceValues[0]}.{(distanceValue2Int == 0 ? "00" : distanceValue2Int)}) ");
            }
            var isFirstLetterVowelOrY = HelperCollections.VowelList.Contains(Description.First());
            stringBuilder.Append($"to {(isFirstLetterVowelOrY ? "an" : "a")} {Description}; ");

            return stringBuilder.ToString();
        }
    }
}
