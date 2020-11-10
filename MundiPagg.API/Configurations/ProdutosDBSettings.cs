namespace MundiPagg.API.Configurations
{
    public class ProdutosDBSettings : IProdutosDBSettings
    {
        public string ProdutosCollectionName { get; set; }
        public string ConnectionsString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IProdutosDBSettings
    {
         public string ProdutosCollectionName { get; set;}

         public string ConnectionsString { get; set;}

         public string DatabaseName { get; set;}

    }
}