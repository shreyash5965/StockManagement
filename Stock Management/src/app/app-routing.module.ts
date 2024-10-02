import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginModule } from './main/Authentication/login/login.module';

const routes: Routes = [
  
  { path: '', redirectTo: 'login', pathMatch:'full' },
  { path: 'login', loadChildren: () => import('./main/Authentication/login/login.module').then(m => m.LoginModule) },
  { path: 'main', loadChildren: () => import('./main/main.module').then(m => m.MainModule) },
  // { path: 'aboutus', loadChildren: () => import('./main/masters/aboutus/aboutus.module').then(m => m.AboutusModule) },
  { path: '**', loadChildren: () => import('./main/not-found/not-found.module').then(m => m.NotFoundModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule, ]
})
export class AppRoutingModule { }
