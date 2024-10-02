import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TradingHistoryRoutingModule } from './trading-history-routing.module';
import { TradingHistoryComponent } from './trading-history.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [TradingHistoryComponent],
  imports: [
    CommonModule,
    TradingHistoryRoutingModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatIconModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatDialogModule
  ],
  providers: [  
    MatDatepickerModule,  
  ],
})
export class TradingHistoryModule { }
