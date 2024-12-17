using RetryPattern.Services.Foos;

namespace RetryPattern
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await new FooService().RetrieveNumberAsync();
        }
    }
}
