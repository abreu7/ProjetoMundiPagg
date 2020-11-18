using System.Collections.Generic;
using System.Threading.Tasks;

namespace MundiPagg.API.DatabaseContext.Repository.Produto
{
    public interface IProdutoRepository
    {
         List<Models.Produto> ListaProdutos(int pageNumber, int nPerPage, string categoria);
         
         long CountProdutos();
         
         Models.Produto BuscaProdutoByID(string id);
         
         Models.Produto InsereProduto(Models.Produto produto);
         
         void AtualizaProduto(string id, Models.Produto produto);
         
         void DeletaProduto(string id);
    }
}