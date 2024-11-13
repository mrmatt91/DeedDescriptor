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
        public Line(string point, string description, string lineData) { }
    }
}
