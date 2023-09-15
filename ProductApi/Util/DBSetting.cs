using System;
namespace ProductApi.Util
{
	public class DBSetting
	{
        public required string ConnectionString { get; set; }
		public required string DatabaseName { get; set; }
		public required string ProductCollectionName { get; set; }
        public required string UserCollectionName { get; set; }
		public required string TransactionCollectionName { get; set; }
    }
}

