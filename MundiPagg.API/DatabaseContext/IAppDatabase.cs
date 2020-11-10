using MongoDB.Driver;
using MundiPagg.API.Models;

namespace MundiPagg.API.DatabaseContext
{
    public interface IAppDatabase
    {
         IMongoCollection<Produto> Connection { get; } 
    }
}