import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './Authentication/login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { HeaderInterceptor } from './interceptoers/header-interceptor';
import { ErrorInterceptor } from './interceptoers/error-interceptor';
import { DeleteDialogComponent } from './masters/delete-dialog/delete-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { SharedService } from './common/shared-service';

@NgModule({
  declarations: [
    MainComponent,
    DeleteDialogComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HeaderInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    SharedService,
  ],
  bootstrap: [MainComponent, LoginComponent]
})
export class MainModule { }
