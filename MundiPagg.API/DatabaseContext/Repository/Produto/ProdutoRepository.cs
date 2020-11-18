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

        public List<Models.Produto> ListaProdutos(int pageNumber, 
                                                  int nPerPage,
                                                  string categoria)
        {
            //Categorias
            //0=todas, 1=Casa e Eletrodomésticos, 2=Beleza
            //3=Casa e Eletrodomésticos, 4=Esporte e Lazer
            //5=Brinquedos, 6=Tecnologia
           

            //var count = _produtos.EstimatedDocumentCountAsync();
            //.Find( x => x.Descricao.Equals(filtro) )
            if ( categoria != "0"){
                return _produtos
                    .Find( x => x.Categoria.Equals(categoria))
                    .SortByDescending(x => x.Id)
                    .Skip( pageNumber > 0 ? ( ( pageNumber - 1 ) * nPerPage ) : 0 )
                    .Limit( nPerPage )
                    .ToList();
            } else{
                    return _produtos
                    .Find( x => true)
                    .SortByDescending(x => x.Id)
                    .Skip( pageNumber > 0 ? ( ( pageNumber - 1 ) * nPerPage ) : 0 )
                    .Limit( nPerPage )
                    .ToList();
            }
                    
             
        }

        public long CountProdutos(){
            //long qtd;

            return  _produtos.CountDocuments(x => true);
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