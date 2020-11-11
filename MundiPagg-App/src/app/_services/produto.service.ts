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

  createProduto(id: string): Observable<Produto[]>{
    return this.http.get<Produto[]>(`${this.baseUrl}/GetProduto/${id}`);
  }
}
