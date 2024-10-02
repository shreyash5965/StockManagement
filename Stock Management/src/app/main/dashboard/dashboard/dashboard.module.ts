import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NgxEchartsModule } from 'ngx-echarts';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTradingModule } from "../../masters/mat-trading/mat-trading.module";

@NgModule({
    declarations: [DashboardComponent],
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MatIconModule,
        MatButtonModule,
        MatStepperModule,
        NgxEchartsModule.forRoot({
            echarts: () => import('echarts'),
        }),
        MatTradingModule
    ]
})
export class DashboardModule { }
