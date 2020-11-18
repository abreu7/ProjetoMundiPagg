using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MundiPagg.API.Dtos;
using MundiPagg.API.Models;
using MundiPagg.API.Services;

namespace MundiPagg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        public readonly IMapper _mapper;
        
        public ProdutosController(ProdutoService produtoService,
                                  IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        //[HttpGet]
        [HttpGet("getprodutos/")]
        public ActionResult<List<ProdutoDto>> GetProdutos(int numeroPagina)
        {
            try
            {
                var results = _produtoService.GetAllProdutos(numeroPagina, "teste");
                //var results = _mapper.Map<List<Produto>, List<ProdutoDto>>(produtos);

                return Ok(results);    
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Banco de Dados Falhou ao retornar os Produtos : {ex.Message}");
            }
            
        }

        //[HttpGet]
        [HttpGet("getprodutos/{numPagina}")]
        public ActionResult<List<ProdutoDto>> GetProdutosPage(int numPagina, string categoria)
        {
            try
            {
                var results = _produtoService.GetAllProdutos(numPagina, categoria);
                //var results = _mapper.Map<List<Produto>, List<ProdutoDto>>(produtos);

                return Ok(results);    
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Banco de Dados Falhou ao retornar os Produtos : {ex.Message}");
            }
            
        }

        [HttpGet("{id:length(24)}", Name = "GetProduto")]
        public ActionResult<ProdutoDto> GetById(string id)
        {
            try
            {
                var result = _produtoService.GetProdutoById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (System.Exception ex )
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Banco de Dados Falhou : {ex.Message}");
            }
            
        }

        [HttpPost]
        public ActionResult<ProdutoDto> Create(ProdutoDto produtodto)
        {
            try
            {
                //var prod = _mapper.Map<ProdutoDto, Produto>(produtodto);
                var result = _produtoService.CriaProduto(produtodto);
                

                return Created($"/api/GetProduto/{produtodto.Id}", result);
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                                        $"Banco de Dados Falhou : {ex.Message}");
            }

            
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ProdutoDto produtoIn)
        {
            try
            {
               var produto = _produtoService.GetProdutoById(id);

               if (produto == null)
                {
                    return NotFound();
                }

                _produtoService.Update(id, produtoIn);

                return NoContent();
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                         $"Banco de Dados Falhou : {ex.Message}");
            }
            
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var produto = _produtoService.GetProdutoById(id);

                if (produto == null)
                {
                    return NotFound();
                }

                _produtoService.Remove(produto.Id);

                return NoContent();
            }
            catch (System.Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                         $"Banco de Dados Falhou : {ex.Message}");
            }
        }
    }
    }