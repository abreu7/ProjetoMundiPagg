using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MundiPagg.API.DatabaseContext;
using MundiPagg.API.DatabaseContext.Repository.Produto;
using MundiPagg.API.Dtos;
using MundiPagg.API.Models;

namespace MundiPagg.API.Services
{
    public class ProdutoService : IProdutoService
    {
        //private readonly IMongoCollection<Produto> _produtos;
        private readonly IProdutoRepository _PRODUTOREP;
        public readonly IMapper _mapper;
        public ProdutoService(IProdutoRepository produtorep,
                              IMapper mapper)
        {
            _PRODUTOREP = produtorep;
            _mapper = mapper;
            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            //_books = database.GetCollection<Book>(settings.BooksCollectionName);

            //_produtos = produtosdb.Connection;
        }

        public List<ProdutoDto> GetAllProdutos(int numPagina, string categoria) 
        {

            List<ProdutoDto> result = _mapper.Map<List<Produto>, List<ProdutoDto>>(
                                      _PRODUTOREP.ListaProdutos(numPagina, 5, categoria));

            return result;
            //return _produtos.Find(produto => true)
            //        .ToList();         
        }
        
        public long CountProdutos(){
            return _PRODUTOREP.CountProdutos();
        }
        
        public ProdutoDto GetProdutoById(string id)
        {
            //_produtos.Find<Produto>(produto => produto.Id == id).FirstOrDefault();
            ProdutoDto result;
            result = _mapper.Map<Produto, ProdutoDto>(_PRODUTOREP.BuscaProdutoByID(id));

            return result;
        }

        public ProdutoDto CriaProduto(ProdutoDto produto)
        {
            //Produto a ser Inserido
            Produto resultin;
            //ProdutoDto que será retornado
            ProdutoDto resultout;

            resultin = _mapper.Map<ProdutoDto, Produto>(produto);
            resultout = _mapper.Map<Produto, ProdutoDto>(_PRODUTOREP.InsereProduto(resultin));
            
            //return _PRODUTOREP.InsereProduto(resultin);
            //return resultout;
            //_produtos.InsertOne(produto);
            return resultout;
        }

        public void Update(string id, ProdutoDto produtoIn)
        {
            //_produtos.ReplaceOne(produto => produto.Id == id, produtoIn);
            //Produto a ser Atualizado
            Produto resultin = _mapper.Map<ProdutoDto, Produto>(produtoIn);
            _PRODUTOREP.AtualizaProduto(id, resultin);
        }
        
        public void Remove(ProdutoDto produtoIn)
        {
            //Definir se esse tipo de deleção será implementado
            //Produto resultin = _mapper.Map<ProdutoDto, Produto>(produtoIn);
            //_produtos.DeleteOne(produto => produto.Id == produtoIn.Id);
        }
        
        public void Remove(string id)
        { 
            //Produto resultin = _mapper.Map<ProdutoDto, Produto>(produtoIn);
            //_produtos.DeleteOne(produto => produto.Id == id);
            _PRODUTOREP.DeletaProduto(id);
        }
    }
}