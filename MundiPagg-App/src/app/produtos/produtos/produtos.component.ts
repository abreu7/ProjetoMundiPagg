import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  formProduto: FormGroup;
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
              private formbuilder: FormBuilder) { }

  ngOnInit() {
    this.validation();
    this.getProdutos();
  }

  filtrarProduto(filtrarPor: string): Produto[] {
    filtrarPor = filtrarPor.toLowerCase();
    return this.produtos.filter(
      result => result.nome.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(template: any) {
    // this.registerForm.reset();
    template.show();
    // this.modalRef = this.modalService.show(template);
  }

  novoProduto(template: any){
    this.openModal(template);
  }

  salvarAlteracao(template: any) {
    console.log('SalvarAlteração foi chamado');
    // if (this.registerForm.valid) {
    //   if (this.modoSalvar === 'post') {
    //     this.evento = Object.assign({}, this.registerForm.value);

    //     this.uploadImagem();

    //     this.eventoService.postEvento(this.evento).subscribe(
    //       (novoEvento: Evento) => {
    //         console.log(novoEvento);
    //         template.hide();
    //         this.getEventos();
    //         this.toastr.success('Inserido com Sucesso');
    //       }, error => {
    //         this.toastr.error(`Erro ao Inserir: ${error.message}`);
    //       }
    //     );
    //   } else {
    //     this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);

    //     this.uploadImagem();

    //     this.eventoService.putEvento(this.evento).subscribe(
    //       (novoEvento: Evento) => {
    //         console.log(novoEvento);
    //         template.hide();
    //         this.getEventos();
    //         this.toastr.success('Editado com Sucesso');
    //       }, error => {
    //         this.toastr.error(`Erro ao Editar: ${error}`);
    //       }
    //     );
    //   }
    // }
  }

  onFileChange(arquivo: any) {
    console.log('OnFileChange foi chamado', arquivo);
    const reader = new FileReader();

    // if (arquivo.target.files && arquivo.target.files.length) {
    //   this.file = arquivo.target.files;
    //   console.log('Arquivo', this.file);
    // }
  }

  validation() {
    // this.formProduto = this.formbuilder.group({
    //   nome: ['', [Validators.required,
    //               Validators.minLength(4),
    //               Validators.maxLength(50)]]
    // });
     this.formProduto = this.formbuilder.group({
       nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
       descricao: ['', [Validators.required, Validators.maxLength(20)]],
       url: ['', Validators.required],
       categoria: ['', Validators.required],
       valor: ['', Validators.required],
       marca: ['', Validators.required],
     });
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
