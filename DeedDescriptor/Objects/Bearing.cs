using Humanizer;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DeedDescriptor.Objects
{
	public class Bearing : IDescriptionTranslator
	{
		// The primary direction (N, S)
		public char PrimaryDirection { get; set; }

		// Degrees part of the bearing
		public int Degrees { get; set; }

		// Minutes part of the bearing
		public int Minutes { get; set; }

		// Seconds part of the bearing
		public int Seconds { get; set; }

		// The secondary direction (E, W)
		public char SecondaryDirection { get; set; }

		public Bearing(string bearingString)
			: this(bearingString.Split(' ')) { }

		public Bearing(string[] bearingStringTokens)
		{
			PrimaryDirection = bearingStringTokens[0].First();
            Degrees = int.Parse(bearingStringTokens[1].Substring(0, bearingStringTokens[1].Length - 1));
			Minutes = int.Parse(bearingStringTokens[2].Substring(0, bearingStringTokens[2].Length - 1));
			Seconds = int.Parse(bearingStringTokens[3].Substring(0, bearingStringTokens[3].Length - 1));
			SecondaryDirection = bearingStringTokens[4].First();
		}
		public Bearing(char primaryDirection, int degrees, int minutes, int seconds, char secondaryDirection)
		{
			PrimaryDirection = primaryDirection;
			Degrees = degrees;
			Minutes = minutes;
			Seconds = seconds;
			SecondaryDirection = secondaryDirection;
		}

		public Bearing(Bearing bearing)
		{
			PrimaryDirection = bearing.PrimaryDirection;
			Degrees = bearing.Degrees;
			Minutes = bearing.Minutes;
			Seconds = bearing.Seconds;
			SecondaryDirection = bearing.SecondaryDirection;
		}

		// Override ToString method to provide a readable format of the bearing
		public override string ToString()
		{
			return $"{PrimaryDirection} {Degrees}° {Minutes}' {Seconds}\" {SecondaryDirection}";
		}

        public double GetDegrees()
        {
            double decimalDegrees = Degrees + (Minutes / 60) + (Seconds / 3600);

            // Calculate azimuth
            double azimuth = 0.0;

            if (PrimaryDirection == 'N')
            {
                azimuth = (SecondaryDirection == 'E') ? decimalDegrees : 360 - decimalDegrees;
            }
            else if (PrimaryDirection == 'S')
            {
                azimuth = (SecondaryDirection== 'E') ? 180 - decimalDegrees : 180 + decimalDegrees;
            }
            else
            {
                throw new ArgumentException("Invalid bearing format.");
            }

            return azimuth % 360;
        }

        public string TranslateToDeedDescription()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{HelperCollections.DirectionMap[PrimaryDirection]} ");
            stringBuilder.Append($"{Degrees.ToWords()} ({Degrees}) degrees ");
            stringBuilder.Append($"{Minutes.ToWords()} ({Minutes}) minutes ");
            stringBuilder.Append($"{Seconds.ToWords()} ({Seconds}) seconds ");
            stringBuilder.Append($"{HelperCollections.DirectionMap[SecondaryDirection]} ");
			return stringBuilder.ToString();
        }
    }

}
