using Core.Model;

namespace Core.Interfaces
{
    public interface IAssetService
    {
        Task<Asset> CreateAsset(Asset asset,string photoUrl);
    }
}