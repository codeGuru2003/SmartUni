using System.Text;

namespace SmartUni.Services
{
	public class RandomStringGenerator : IRandomStringGenerator
	{
		private static readonly Random random = new Random();
		public string GenerateRandomString(int length)
		{
			const string allowedChars = "0123456789";
			StringBuilder sb = new StringBuilder(length);

			for (int i = 0; i < length; i++)
			{
				int randomIndex = random.Next(0, allowedChars.Length);
				sb.Append(allowedChars[randomIndex]);
			}

			return sb.ToString();

		}
	}
}
