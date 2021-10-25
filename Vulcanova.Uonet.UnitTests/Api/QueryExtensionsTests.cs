using Vulcanova.Uonet.Api;
using Xunit;

namespace Vulcanova.Uonet.UnitTests.Api
{
    public class QueryExtensionsTests
    {
        [Fact]
        public void ToQueryString_ApiQuery_ReturnsCorrectQueryString()
        {
            var query = new Query();

            var queryString = query.ToQueryString();
            
            Assert.Equal("?property=This+value", queryString);
        }

        [Fact]
        public void ToQueryString_ParameterlessApiQuery_ReturnsEmptyString()
        {
            var query = new EmptyQuery();

            var queryString = query.ToQueryString();
            
            Assert.Equal(string.Empty, queryString);
        }

        private class Query : IApiQuery
        {
            public string Property { get; set; } = "This value";
        }
        
        private class EmptyQuery : IApiQuery { }
    }
}