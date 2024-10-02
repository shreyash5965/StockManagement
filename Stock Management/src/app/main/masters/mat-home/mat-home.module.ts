import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatHomeRoutingModule } from './mat-home-routing.module';
import { MatHomeComponent } from './mat-home.component';
import { MatHomeDialogComponent } from './mat-home-dialog/mat-home-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';


@NgModule({
  declarations: [MatHomeComponent, MatHomeDialogComponent],
  imports: [
    CommonModule,
    MatHomeRoutingModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatIconModule,
    MatButtonModule,
  ],
  providers: [  
    MatDatepickerModule,  
  ],
})
export class MatHomeModule { }
