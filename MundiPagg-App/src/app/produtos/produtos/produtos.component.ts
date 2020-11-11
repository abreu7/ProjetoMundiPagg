import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Produto } from 'src/app/_models/produto';
import { ProdutoService } from 'src/app/_services/produto.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  produtos: Produto[];
  imagemLargura = 50;
  imagemMargem = 2;
  modalRef: BsModalRef;
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
              private modalService: BsModalService) { }

  ngOnInit() {

    this.getProdutos();
  }

  filtrarProduto(filtrarPor: string): Produto[] {
    filtrarPor = filtrarPor.toLowerCase();
    return this.produtos.filter(
      result => result.descricao.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(template: TemplateRef<any>) {
    //this.registerForm.reset();
    //template.show();
    this.modalRef = this.modalService.show(template);
  }

  getProdutos() {
    this.produtoService.getAllProdutos().subscribe(
      (_produtos: Produto[]) => {
        this.produtos = _produtos;
        this.produtosFiltrados = _produtos;
        // this.produtos = response;
        console.log('Produtos:', _produtos);
      },
      error => {
        // this.toastr.error(`Erro ao tentar carregar eventos: ${error.message}`);
        console.log('Erro ao tentar carregar Produtos', error);
    });
  }

}
