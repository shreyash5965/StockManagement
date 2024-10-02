import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { HomeComponent } from './masters/home/home.component';


const routes: Routes = [
  { 
    path: '', component: MainComponent, 
    children: [
    { path: 'home', loadChildren: () => import('./masters/home/home.module').then(m => m.HomeModule) },
    { path: 'aboutus', loadChildren: () => import('./masters/aboutus/aboutus.module').then(m => m.AboutusModule) },
    { path: 'mat-home', loadChildren: () => import('./masters/mat-home/mat-home.module').then(m => m.MatHomeModule) },
    { path: 'mat-stock-master', loadChildren: () => import('./masters/mat-stock-master/mat-stock-master.module').then(m => m.MatStockMasterModule) },
    { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard/dashboard.module').then(m => m.DashboardModule) },
    { path: 'trading-history', loadChildren: () => import('./masters/trading-history/trading-history.module').then(m => m.TradingHistoryModule) },
  ] 
},
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [RouterModule, ]
})
export class MainRoutingModule { }
