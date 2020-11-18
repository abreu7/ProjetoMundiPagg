import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule} from 'ngx-bootstrap/tooltip';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';


// Services
import { ProdutoService } from './_services/produto.service';

// Componentes
import { AppComponent } from './app.component';
import {ProdutosComponent} from './produtos/produtos.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [	
    AppComponent,
    ProdutosComponent,
    NavComponent,
      HomeComponent
   ],
  imports: [
    BrowserModule,
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    BrowserAnimationsModule,
      ToastrModule.forRoot({
        timeOut: 3000,
        preventDuplicates: true,
        progressBar: true
      }),
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [ProdutoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
