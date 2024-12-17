namespace RetryPattern.Services.Foos
{
    public partial class FooService
    {
        private Func<ValueTask<int>> ReturningFunction { get; set; }

        public async ValueTask<int> RetrieveNumberAsync() =>
            await TryCatch(() =>
            {
                Console.WriteLine("Retrieving number...");

                return ValueTask.FromResult(1);
            }).Retry(retryCount: 5).ExecuteAsync();

        private async ValueTask<int> ExecuteAsync() =>
            await this.ReturningFunction();
    }
}
