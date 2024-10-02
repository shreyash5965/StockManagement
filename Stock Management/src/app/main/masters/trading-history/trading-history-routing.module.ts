import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TradingHistoryComponent } from './trading-history.component';

const routes: Routes = [{ path: '', component: TradingHistoryComponent }];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule, ]
})
export class TradingHistoryRoutingModule { }
