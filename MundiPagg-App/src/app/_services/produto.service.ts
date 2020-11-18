import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../_models/produto';

@Injectable()
export class ProdutoService {
  baseUrl = 'http://localhost:5000/api/produtos';

constructor(private http: HttpClient) { }

  getAllProdutos(): Observable<Produto[]>{
    return this.http.get<Produto[]>(`${this.baseUrl}/getprodutos`);
  }

  getAllProdutosTeste(numPagina: number, categoria: string){
    return this.http.get<Produto[]>(`${this.baseUrl}/getprodutos/${numPagina}?categoria=${categoria}`);
  }

  // createProduto(id: string): Observable<Produto[]>{
  //   return this.http.get<Produto[]>(`${this.baseUrl}/GetProduto/${id}`);
  // }

  createProduto(produto: Produto) {
    return this.http.post(this.baseUrl, produto);
  }

  editaProduto(produto: Produto) {
    return this.http.put(`${this.baseUrl}/${produto.id}`, produto);
  }

  deletaProduto(id: string) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
