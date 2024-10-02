import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatStockMasterRoutingModule } from './mat-stock-master-routing.module';
import { MatStockMasterComponent } from './mat-stock-master.component';
import { MatStockMasterDialogComponent } from './mat-stock-master-dialog/mat-stock-master-dialog.component';
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
  declarations: [MatStockMasterComponent, MatStockMasterDialogComponent],
  imports: [
    CommonModule,
    MatStockMasterRoutingModule,
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
export class MatStockMasterModule { }
