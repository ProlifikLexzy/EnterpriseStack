using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MyApp.Shared.PubSub;
using MyApp.Shared.PubSub.KafkaImpl;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using  MyApp.Shared.Extensions;

namespace MyApp.Shared.Net.WorkerService
{
    public class EvenHubProcessorService : BackgroundService
    {
        //private readonly ILogger<MessageProcessorService> _logger;
        private readonly BoundedMessageChannel<BusMessage> _boundedMessageChannel;
        private readonly List<BusHandler> _busDelegate;

        public EvenHubProcessorService(BoundedMessageChannel<BusMessage> boundedMessageChannel, 
            Func<List<BusHandler>> busDelegate)
        {
            // _logger = logger;
            _boundedMessageChannel = boundedMessageChannel;
            _busDelegate = busDelegate();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                   var message = await _boundedMessageChannel.ReadAsync(stoppingToken);

                   if (message.Value == null)
                        continue;

                   //var busMsg = JsonConvert.DeserializeObject<BusMessage>(message.Value);
                    var busMsg = message.Value;
                   foreach(var item in _busDelegate)
                    {
                        item(busMsg);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Log an swallow as the while loop will end gracefully when cancellation has been requested
                    //_logger.OperationCancelledExceptionOccurred();
                }
                catch (Exception)
                {
                    // If errors occur, we will probably send this to a poison queue, allow the message 
                    // to be deleted and continue processing other messages.
                    //_logger.ExceptionOccurred(ex);
                    // Note: Assumes no roll back is needed due to partial success for various processing tasks.
                }
            }

            // delete the message from the main queue
            // Log.ProcessedMessage(_logger, message.MessageId);
            //Log.StoppedProcessing(_logger);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            //var sw = Stopwatch.StartNew();
            await base.StopAsync(cancellationToken);
            //Log.MillisecondsToStopProcessing(_logger, sw.ElapsedMilliseconds);
        }

        // internal static class EventIds
        // {
        //     public static readonly EventId StartedProcessing = new EventId(100, "StartedProcessing");
        //     public static readonly EventId ProcessorStopping = new EventId(101, "ProcessorStopping");
        //     public static readonly EventId StoppedProcessing = new EventId(102, "StoppedProcessing");
        //     public static readonly EventId StartedTaskInstances = new EventId(103, "StartedTaskInstances");
        //     public static readonly EventId ProcessedMessage = new EventId(110, "ProcessedMessage");
        //     public static readonly EventId StopProcessingTimer = new EventId(120, "StopProcessingTimer");
        // }

        // private static class Log
        // {
        //     private static readonly Action<ILogger, string, Exception> _processedMessage = LoggerMessage.Define<string>(
        //         LogLevel.Debug,
        //         EventIds.ProcessedMessage,
        //         "Read and processed message with ID '{MessageId}' from the channel.");

        //     private static readonly Action<ILogger, long, Exception> _millisecondsToStopProcessing = LoggerMessage.Define<long>(
        //         LogLevel.Debug,
        //         EventIds.StopProcessingTimer,
        //         "Stopped message processor after {Milliseconds} ms.");

        //     public static void StartedProcessing(ILogger logger)
        //     {
        //         if (logger.IsEnabled(LogLevel.Debug))
        //         {
        //             logger.Log(LogLevel.Trace, EventIds.StartedProcessing, "Started message processing service.");
        //         }
        //     }

        //     public static void ProcessorStopping(ILogger logger)
        //     {
        //         if (logger.IsEnabled(LogLevel.Information))
        //         {
        //             logger.Log(LogLevel.Information, EventIds.ProcessorStopping, "Message processing stopping due to app termination!");
        //         }
        //     }

        //     public static void StoppedProcessing(ILogger logger)
        //     {
        //         if (logger.IsEnabled(LogLevel.Debug))
        //         {
        //             logger.Log(LogLevel.Trace, EventIds.StoppedProcessing, "Stopped message processing service.");
        //         }
        //     }

        //     public static void ProcessedMessage(ILogger logger, string messageId)
        //     {
        //         _processedMessage(logger, messageId, null);
        //     }

        //     public static void MillisecondsToStopProcessing(ILogger logger, long milliseconds)
        //     {
        //         _millisecondsToStopProcessing(logger, milliseconds, null);
        //     }
    }
}
