import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Produto } from 'src/app/_models/produto';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  produtos: Produto[] = [];
  imagemLargura = 50;
  imagemMargem = 2;
  modalRef: BsModalRef;
  formProduto: FormGroup;
  produto: Produto;
  modoSalvar = 'post';
  bodyDeletarEvento = '';
  numPagina = 0;
  numPaginas = [1, 1];
  proximo = true;
  categoriaSelecionada = '0';
  categoriaNova = '0';
  paginaAtual = 1;
  cadastro: string;
  // filtroLista = '';


  _filtroLista: string;
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.produtosFiltrados = this.filtroLista ? this.filtrarProduto(this.filtroLista) : this.produtos;
  }
  produtosFiltrados: Produto[];


  constructor(private produtoService: ProdutoService,
              private modalService: BsModalService,
              private formbuilder: FormBuilder,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.validation();
    // this.getProdutos();
    this.getProdutosTeste(this.numPagina);
  }

  filtrarProduto(filtrarPor: string): Produto[] {
    filtrarPor = filtrarPor.toLowerCase();
    return this.produtos.filter(
      result => result.nome.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(template: any) {
    this.formProduto.reset();
    template.show();
    // this.modalRef = this.modalService.show(template);
  }

  novoProduto(template: any){
    this.modoSalvar = 'post';
    this.cadastro = 'Novo';
    this.openModal(template);
  }

  editaProduto(produto: Produto, template: any){
    this.modoSalvar = 'put';
    this.cadastro = 'Editar';

    this.openModal(template);

    // Realizo uma cópia do evento para um objeto
    this.produto = Object.assign({}, produto);
    this.formProduto.patchValue(this.produto);
  }

  excluirProduto(produto: Produto, template: any) {
    this.openModal(template);
    this.produto = produto;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Produto: ${produto.nome}`;
  }

  confirmeDelete(template: any) {
    this.produtoService.deletaProduto(this.produto.id).subscribe(
      () => {
          template.hide();
          this.getProdutosTeste(0);

          this.numPaginas = [1, 1];
          this.proximo = true;
          // console.log('Deletado com Sucesso');
          this.toastr.success('Excluído com Sucesso');
        }, error => {
          this.toastr.error('Erro ao Deletar');
          // console.log(error);
        }
    );
  }

  salvarAlteracao(template: any) {
    // console.log('SalvarAlteração foi chamado');
    if (this.formProduto.valid) {
      if (this.modoSalvar === 'post') {

        this.produto = Object.assign({}, this.formProduto.value);
        // console.log('Produto a ser salvo: ', this.produto);
         // this.uploadImagem();

        this.produtoService.createProduto(this.produto).subscribe(
          (novoProduto: Produto) => {
            // console.log(novoProduto);
            template.hide();
            this.getProdutosTeste(0);
            this.toastr.success('Inserido com Sucesso');

            // Reinicia a Paginação
            this.numPaginas = [1, 2];
            this.proximo = true;

          }, error => {
             template.hide();
             this.toastr.error(error.message , `Erro ao Inserir:`);
           }
         );
       } else {

        this.produto = Object.assign({id: this.produto.id}, this.formProduto.value);

        this.produtoService.editaProduto(this.produto).subscribe(
          (novoProduto: Produto) => {
            // console.log(novoProduto);
            template.hide();
            this.getProdutosTeste(0);
            this.toastr.success('Editado com Sucesso');

            // Reinicia a Paginação
            this.numPaginas = [1, 2];
            this.proximo = true;

           }, error => {
             template.hide();
             this.toastr.error(error, `Erro ao Editar:`);
           }
         );
       }
     }
  }

  validation() {
     this.formProduto = this.formbuilder.group({
       nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
       descricao: ['', [Validators.required, Validators.maxLength(200)]],
       url: ['', Validators.required],
       categoria: ['', Validators.required],
       valor: ['',  Validators.required],
       marca: ['', Validators.required],
     });
  }

  getProdutos() {
    this.produtoService.getAllProdutos().subscribe(
      (_produtos: Produto[]) => {
        this.produtos = _produtos;
        this.produtosFiltrados = _produtos;
        // this.produtos = response;
        // console.log('Produtos:', _produtos);
      },
      error => {
        // this.toastr.error(`Erro ao tentar carregar eventos: ${error.message}`);
        console.log('Erro ao tentar carregar Produtos', error);
    });
  }

  getProdutosTeste(numPagina: number){
    // console.log('numPagina', numPagina);
    // console.log('numPaginasfora[]', this.numPaginas.length);

    this.produtoService.getAllProdutosTeste(numPagina, this.categoriaSelecionada).subscribe(
      (_produtos: Produto[]) => {
        this.produtos = _produtos;
        // console.log('tam_produtos[]', this.produtos.length);
        this.produtosFiltrados = _produtos;

        this.paginaAtual = numPagina;
        console.log('Produtos:', _produtos);
      },
      error => {
        this.toastr.error(error.message , `Erro ao tentar carregar eventos: `);
        // console.log('Erro ao tentar carregar Produtos', error);
    });
  }

  adicionaPaginas(numPagina: number){
    // console.log('numPagina: ', numPagina);
    if (this.produtos.length < 5)
    {
      this.proximo = false;
    }

    if (numPagina > 2 && this.produtos.length >= 5)
    {
      // console.log('ENTREI NO IF: ', numPagina);
      this.getProdutosTeste(numPagina);
      this.numPaginas.push(numPagina);
    }

  }

  escolheCategoria(){
    // console.log('Categoria: ', this.categoriaSelecionada);

    this.numPaginas = [1, 1];
    this.getProdutosTeste(0);

    this.proximo = true;

    // console.log('TamProdutos: ', this.produtos.length);

  }


}
