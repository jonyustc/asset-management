namespace Core.Model
{
    public class Photo : BaseEntity
    {
        public Photo()
        {
        }

        public Photo(string url, bool isMain,Asset asset)
        {
            Url = url;
            IsMain = isMain;
            Asset = asset;
        }

        public string Url { get; set; }
        public bool IsMain { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}