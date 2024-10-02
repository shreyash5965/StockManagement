import { Component, OnDestroy, OnInit } from '@angular/core';
import { EChartsOption } from 'echarts/types/dist/echarts';
import { Result } from 'src/app/common/Result';
import { Dashboard } from './dashboard';
import { DashboardService } from './dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements OnInit, OnDestroy{
  totalInvested:number = 0;
  profit_loss:number = 0;
  isDarkModeOn : boolean = localStorage.getItem("isDarkMode") != null ? (localStorage.getItem("isDarkMode") == 'true' ? true : false) : false;
  dashboardData: Dashboard = new Dashboard(); 
  isNew: boolean = false;
  stockNames: any = [];
  InvestmentData: any = [];
  Profit_loss_Data: any[] = [];
  Interval: any;
  investment_options: EChartsOption = {}

  profit_loss_options: EChartsOption = {}

  constructor(private dashboardService: DashboardService){}

  ngOnInit(): void {
    this.GetDashboardData();

    var self = this;
    this.Interval = window.setInterval(function(){
      self.GetDashboardData();
    }, 5000);

    this.RefreshCharts();
  }

  RefreshCharts(){

    this.investment_options = {
      tooltip: {
        trigger: 'item',
        shadowColor: 'none'
      },
      formatter: function (params: any) {
        return 'Quantity: ' + params.data.quantity + 
        '<br/>Amount: ' + params.value.toLocaleString('en-IN', {style: 'currency', currency: 'INR'});
      },
      series: [{
        // top: '-10%',
        name: 'Stock Info.',
        type: 'pie',
        radius: '50%',
        label:{
          textBorderColor: 'none',
          color: '#f59b42',
          fontWeight: 'bold'
        },
        data: this.InvestmentData,
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }]
    }

    this.profit_loss_options = {
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow'
        },
        formatter: function (params: any) {
          var color = params[0].data.value > 0 ? 'green' : 'red';
          return '<span style="color:' + color + '">' + (params[0].data.value > 0 ? "Profit: " : "Loss: ") + params[0].data.value.toLocaleString('en-IN', {style: 'currency', currency: 'INR'}) +
          '</span>'
        },
      },
      grid: {
        top: 30,
        bottom: 20
      },
      xAxis: {
        type: 'value',
        position: 'top',
        splitLine: {
          lineStyle: {
            type: 'dashed'
          }
        }
      },
      yAxis: {
        type: 'category',
        axisLine: { show: false },
        axisLabel: { show: false },
        axisTick: { show: false },
        splitLine: { show: false },
        data: this.stockNames
      },
      series: [
        {
          name: 'Cost',
          type: 'bar',
          stack: 'Total',
          label: {
            show: true,
            textBorderColor: 'none',
            color: '#f59b42',
            fontWeight: 'bold',
            formatter: '{b}'
          },
          data: this.Profit_loss_Data
        }
      ]
    };
  }

  ngOnDestroy(): void {
    clearInterval(this.Interval);
  }

  GetDashboardData()
  {
    this.dashboardService.GetDashboardData().subscribe(
    res =>
    {
      if(res.isSuccess)
      {
        this.InvestmentData = [];
        this.Profit_loss_Data = [];
        this.stockNames = [];

        this.dashboardData = JSON.parse(res.data);
        this.dashboardData.profit_loss_data.forEach(item => {
          this.stockNames.push(item.strshortname);  
        });
        
        this.dashboardData.profit_loss_data.forEach((item) => 
        { 
          this.InvestmentData.push({ quantity: item.intquantity, value: item.invested_amount, name: item.strshortname });
          this.Profit_loss_Data.push({ value: item.profit_loss_amount ?? 0.00, label: { position: 'right' }});
        });

        this.RefreshCharts();
        //this.PieChartInitialize();
        // console.log(this.stockNames);
        // console.log(this.Profit_loss_Data)
      }
    },
    err=> 
    {
    })
  }

  buy_sell_stocks(){
    this.isNew = true;
  }

  public onCancel(data:any)
  {    
    this.isNew = data;
    // this.GetData();
  }
}