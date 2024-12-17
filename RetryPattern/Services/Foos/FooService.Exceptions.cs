namespace RetryPattern.Services.Foos
{
    public partial class FooService
    {
        private delegate ValueTask<int> ReturningNumberFunction();

        private FooService TryCatch(ReturningNumberFunction returningNumberFunction)
        {
            try
            {
                this.ReturningFunction = returningNumberFunction.Invoke;

                return this;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private FooService Retry(int retryCount)
        {
            var originalReturningFunction = this.ReturningFunction;

            this.ReturningFunction = async () =>
            {
                while (retryCount != 0)
                {
                    try
                    {
                        return await originalReturningFunction.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        retryCount--;

                        if (retryCount == 0)
                        {
                            throw;
                        }
                    }
                }

                throw new Exception("Retry count exceeded without success.");
            };

            return this;
        }
    }
}
