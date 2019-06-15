using System;
using System.Text;
using System.Security.Cryptography;
using CounterCulture.Utilities;

namespace CounterCulture.Utilities
{

  public static class StringExtensions
  {
    public static string NewClientId(this string stringValue){
      var clientId = string.IsNullOrEmpty(stringValue) ?
                     Guid.NewGuid().ToString() :
                     stringValue;
      return clientId.HexEncode();
    }

    public static string NewClientSecret(this string stringValue){
      RandomNumberGenerator rng = RandomNumberGenerator.Create();
      byte[] buffer = new byte[32];
      rng.GetBytes(buffer);
      return Convert.ToBase64String(buffer).HexEncode();
    }

    public static string HexEncode(this string stringValue){
            var bytes = Encoding.Default.GetBytes(stringValue);
            var hexString = BitConverter.ToString(bytes);
            return hexString.Replace("-","");
    }
  }

}