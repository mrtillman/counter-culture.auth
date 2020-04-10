
using System;
using System.Text;
using System.Security.Cryptography;
using Presentation.Utilities;

namespace Presentation.Utilities
{

  public static class StringExtensions
  {
    public static string NewClientId(this string stringValue){
      var buffer = getRandomBytes(32);
      return Convert.ToBase64String(buffer).HexEncode();
    }

    public static string NewClientSecret(this string stringValue){
      var suffix = stringValue.NewClientId();
      return $"secret.{suffix.ToLower()}";
    }
    public static string HexEncode(this string stringValue){
            var bytes = Encoding.Default.GetBytes(stringValue);
            var hexString = BitConverter.ToString(bytes);
            return hexString.Replace("-","");
    }

    private static byte[] getRandomBytes(int length) {
      RandomNumberGenerator rng = RandomNumberGenerator.Create();
      byte[] buffer = new byte[length];
      rng.GetBytes(buffer);
      return buffer;
    }
  }

}