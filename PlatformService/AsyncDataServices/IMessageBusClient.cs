using PlatformService.Dtos;

namespace PlatformServices.AsyncDataServices
{
    public interface IMessageBusClient{
        void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
    }   
}