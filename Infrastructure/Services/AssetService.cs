using Core.Interfaces;
using Core.Model;

namespace Infrastructure.Services
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Asset> CreateAsset(Asset asset, string photoUrl)
        {
            //var asset = new Asset(shippingAddress, deliveryMethodId, items, 0, subTotal);

       

            var assetToReturn = await _unitOfWork.Repository<Asset>().Add(asset);

            if (!string.IsNullOrEmpty(photoUrl))
            {
                var photoToSave = new Photo(photoUrl, true, assetToReturn);

                await _unitOfWork.Repository<Photo>().Add(photoToSave);
            }



            var eventToSave = new Event(assetToReturn, true);

            await _unitOfWork.Repository<Event>().Add(eventToSave);

            if (await _unitOfWork.Complete() <= 0) return null;

            return assetToReturn;
        }
    }
}