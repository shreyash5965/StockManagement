import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTradingComponent } from './mat-trading.component';
import { MatTradingRoutingModule } from './mat-trading-routing.module';
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [MatTradingComponent],
  imports: [
    CommonModule,
    MatTradingRoutingModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatIconModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatDialogModule,
    MatSelectModule
  ],
  exports:[MatTradingComponent],
  providers: [  
    MatDatepickerModule,  
  ],
})
export class MatTradingModule { }
