using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Seedr.Controller.Interface;

namespace Seedr.Controller.Client;

public class ControllerClientFactory(ControllerClientOptions options) : IDisposable
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress(options.ControllerUrl);

    public IControllerClient Create()
    {
        var proxy = _channel.CreateGrpcService<IControllerService>();

        return new ControllerClient(proxy);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}
