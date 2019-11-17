using Common;

namespace Tests.TestDoubles
{  
  static partial class Mock
  {
    private static IServerUrls mockServerUrls { get; set; }

    public static IServerUrls ServerUrls
    {
      get {
        mockServerUrls = Moq.Mock.Of<IServerUrls>(Moq.MockBehavior.Strict);
        Moq.Mock.Get(mockServerUrls)
            .Setup(urls => urls.API)
            .Returns("https://api.counter-culture.io");
        Moq.Mock.Get(mockServerUrls)
            .Setup(urls => urls.SECURE)
            .Returns("https://secure.counter-culture.io");
        return mockServerUrls;
      }
    }
  }
}
