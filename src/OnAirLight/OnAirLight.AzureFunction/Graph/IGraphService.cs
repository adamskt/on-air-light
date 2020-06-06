using System.Threading.Tasks;

namespace OnAirLight.AzureFunction.Graph
{
    public interface IGraphService
    {
        Task<(string Availability, string Activity)> GetPresence(string userId);
    }
}