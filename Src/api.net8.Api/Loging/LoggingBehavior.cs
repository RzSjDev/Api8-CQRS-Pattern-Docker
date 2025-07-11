using MediatR;
using System.Diagnostics;

namespace api_net9.Loging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("StartOfRequest: {RequestName} WithInformation: {@Request}", requestName, request);

            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            stopwatch.Stop();
            _logger.LogInformation("EndOfRequest: {RequestName} InTime: {ElapsedMilliseconds} WithResponse: {@Response}",
                requestName, stopwatch.ElapsedMilliseconds, response);

            return response;
        }
    }
}
