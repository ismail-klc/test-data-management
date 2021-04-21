import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute } from '@angular/router';
import { Vibration } from '../../../models/vibration';
import { TestService } from '../../../services/test.service';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective, Color, Label } from 'ng2-charts';

@Component({
  selector: 'app-test-vibration',
  templateUrl: './test-vibration.component.html',
})
export class TestVibrationComponent implements OnInit {
   lineChartData: ChartDataSets[] = [];
   lineChartLabels: Label[] ;

   @Input() vibration:string;

   lineChartOptions: (ChartOptions & { annotation: any }) = {
    responsive: true,
    scales: {
      xAxes: [{}],
      yAxes: [
        {
          id: 'y-axis-0',
          position: 'left',
        } 
      ]
    },
    annotation: {
      annotations: [
        {
          type: 'line',
          mode: 'vertical',
          scaleID: 'x-axis-0',
          value: 'March',
          borderColor: 'orange',
          borderWidth: 2,
          label: {
            enabled: true,
            fontColor: 'orange',
            content: 'LineAnno'
          }
        },
      ],
    },
  };

  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';

  @ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;

  // events
  public hideOne(): void {
    const isHidden = this.chart.isDatasetHidden(1);
    this.chart.hideDataset(1, !isHidden);
  }

  constructor(private testService:TestService,private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    console.log(this.vibration)
    this.activatedRoute.params.subscribe(params => {
      this.getTestById(params["testId"]);
    });
  }

  getTestById(testId) {
    this.testService.getTestById(testId).subscribe(data => {
      data = data["data"];
      let vibration = data[this.vibration]

      this.lineChartLabels = this.getAllRosTimes(vibration)
      this.lineChartData = this.getDatas(vibration)
    });
  }

  getAllRosTimes(data){
    let labels : string[] = [];
    for (let i = 0 ; i < data.length; i+=3) {
      labels.push((i/3).toString() + ' s')
    }
    return labels;
  }

  getDatas(vib){
    let dataSet : ChartDataSets[] = [];
    let xDatas : number[] = [];
    let yDatas : number[] = [];
    let zDatas : number[] = [];

    for (let i of vib) {
      let data = i["data"]
      data = data.replace('[','').replace(']','');
      data = data.split(',').map(Number);
      
      let avgOfData = 0;
      for (let j of data){
        avgOfData += j;
      }
      avgOfData = avgOfData/data.length;
      let axis = i["axis"]

      if (axis == 'x'){
        xDatas.push(avgOfData)
      }
        else if (axis == 'y'){
          yDatas.push(avgOfData)
        }
        else if (axis == 'z'){
          zDatas.push(avgOfData)
        }
    }
    dataSet.push({data:xDatas,label:'X axis'})
    dataSet.push({data:yDatas,label:'Y axis'})
    dataSet.push({data:zDatas,label:'Z axis'})

    return dataSet;
  }

}
