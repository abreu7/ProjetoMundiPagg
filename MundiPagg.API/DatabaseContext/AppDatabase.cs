using MongoDB.Driver;
using MundiPagg.API.Configurations;
using MundiPagg.API.Models;
using System.Data;

namespace MundiPagg.API.DatabaseContext
{
    public class AppDatabase : IAppDatabase
    {
        //private readonly IDbConnection _Connection;
        private readonly IMongoCollection<Produto> _produtos;
        public AppDatabase (IProdutosDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionsString);
            var database = client.GetDatabase(settings.DatabaseName);

            _produtos = database.GetCollection<Produto>(settings.ProdutosCollectionName);
        }

        public IMongoCollection<Produto> Connection { get => _produtos; }
        
    }
}