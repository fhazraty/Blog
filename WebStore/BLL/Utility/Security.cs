using Org.BouncyCastle.Crypto.Digests;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Utility
{
	public static class Security
	{
		public static string HashPasswordSHA3(string password, string salt)
		{
			Sha3Digest sha3 = new Sha3Digest(512);
			byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
			sha3.BlockUpdate(saltedPasswordBytes, 0, saltedPasswordBytes.Length);
			byte[] hashBytes = new byte[sha3.GetDigestSize()];
			sha3.DoFinal(hashBytes, 0);
			StringBuilder builder = new StringBuilder();
			foreach (byte b in hashBytes)
			{
				builder.Append(b.ToString("x2"));
			}
			return builder.ToString();
		}

		public static string HashPassword512(string password, string salt)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
				byte[] hashBytes = sha512.ComputeHash(saltedPasswordBytes);
				StringBuilder builder = new StringBuilder();
				foreach (byte b in hashBytes)
				{
					builder.Append(b.ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
