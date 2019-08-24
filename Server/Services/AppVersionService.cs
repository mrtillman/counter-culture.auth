using System;
using System.Reflection;

namespace CounterCulture.Services
{
  public class AppVersionService : IAppVersionService
  {
      public string Version => 
        Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
  }
}
