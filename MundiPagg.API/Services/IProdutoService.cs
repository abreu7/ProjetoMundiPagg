using System.Collections.Generic;
using MundiPagg.API.Dtos;

namespace MundiPagg.API.Services
{
    public interface IProdutoService
    {
         List<ProdutoDto> GetAllProdutos(int numPagina, string categoria);
        long CountProdutos();
        
        ProdutoDto GetProdutoById(string id);

        ProdutoDto CriaProduto(ProdutoDto produto);

        void Update(string id, ProdutoDto produtoIn);
        
        void Remove(ProdutoDto produtoIn);

        void Remove(string id);
    }
}