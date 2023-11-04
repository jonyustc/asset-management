namespace Core.Model
{
    public class Stock : BaseEntity
	{
		public int AssetId { get; set; }
		public virtual Asset Asset { get; set; }
		public int WarehouseId { get; set; }
		public virtual Warehouse Warehouse { get; set; }

		public int StockQuantity { get; set; }
		public int QuantityUnitId { get; set; }
		public QuantityUnit QuantityUnit { get; set; }
	}
}