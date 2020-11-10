using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MundiPagg.API.DatabaseContext.Repository.Produto;

namespace MundiPagg.API.DatabaseContext.Repository.Produto
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoCollection<Models.Produto> _produtos;

        public ProdutoRepository(IAppDatabase produtosdb)
        {
            _produtos = produtosdb.Connection;
        }

        public List<Models.Produto> ListaProdutos()
        {
            return _produtos
                    .Find(produto => true)
                    .ToList();  
        }

        public Models.Produto BuscaProdutoByID(string id)
        {
            return _produtos
                    .Find<Models.Produto>(produto => produto.Id == id)
                    .FirstOrDefault();
        }

        public Models.Produto InsereProduto(Models.Produto produto)
        {   
            
            _produtos.InsertOne(produto);
            
            return produto;
        }

        public void AtualizaProduto(string id, Models.Produto produtoIn)
        {
            _produtos.ReplaceOne(produto => produto.Id == id, produtoIn);
        }

        public void DeletaProduto(string id)
        {
            _produtos.DeleteOne(produto => produto.Id == id);;
        }

        

    }
}