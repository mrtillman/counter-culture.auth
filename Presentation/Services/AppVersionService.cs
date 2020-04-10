using System;
using System.Reflection;

namespace Presentation.Services
{
  public class AppVersionService : IAppVersionService
  {
      public string Version => 
        Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
  }
}
