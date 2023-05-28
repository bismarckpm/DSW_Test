using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Consumers
{
    public class ConsumerValor : ConsumerBase, IHostedService
    {
        protected override string QueueName => "product";

        public ConsumerValor(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<ConsumerValor> logger,
            ILogger<ConsumerBase> baseLogger,
            ILogger<RabbitMqClientBase> clientLogger) :
            base(mediator, connectionFactory, baseLogger, clientLogger)
        {
            try
            {
                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += (model, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var request = JsonConvert.DeserializeObject<ValoresRequest>(message);
                    request = CambiarId(request);
                    mediator.Send(new AgregarValorPruebaCommand(request));
                    Console.WriteLine($"Product message received: {message}");
                };

                //read the message
                Channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while consuming message");
            }
        }

        private ValoresRequest CambiarId(ValoresRequest request)
        {
            request.Identificacion = request.Identificacion + 1;
            return request;
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}