using System.Globalization;

namespace DeedDescriptor.Objects
{
	public static class NumericExtensions
	{
		public static string Format(this decimal? number)
		{
			// Round to the second decimal place
			decimal roundedNumber = Math.Round(number.Value, 2);

			// Format the number with commas and two decimal places
			string formattedNumber = roundedNumber.ToString("N2", CultureInfo.InvariantCulture);

			return formattedNumber;
		}
	}
}
