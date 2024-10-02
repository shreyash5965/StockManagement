import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MatTradingComponent } from './mat-trading.component';

const routes: Routes = [{ path: '', component: MatTradingComponent }];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule, ]
})
export class MatTradingRoutingModule { }
