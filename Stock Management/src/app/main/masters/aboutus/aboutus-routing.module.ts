import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AboutusComponent } from './aboutus.component';

const routes: Routes = [{ path: '', component: AboutusComponent }];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule, ]
})
export class AboutusRoutingModule { }
