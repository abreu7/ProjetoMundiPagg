using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MundiPagg.API.Models
{
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }

        [BsonElement("Descricao")]
        public string Descricao { get; set; }

        [BsonElement("Url")]
        public string Url { get; set; }

        [BsonElement("Categoria")]
        public string Categoria { get; set; }

        public decimal Valor { get; set; }
        
        public string Marca { get; set; }

    }
}