import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MatStockMasterComponent } from './mat-stock-master.component';

const routes: Routes = [{ path: '', component: MatStockMasterComponent }];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule, ]
})
export class MatStockMasterRoutingModule { }
