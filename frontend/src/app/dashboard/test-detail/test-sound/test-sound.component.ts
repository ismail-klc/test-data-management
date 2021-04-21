import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestService } from '../../../services/test.service';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective, Label } from 'ng2-charts';

@Component({
  selector: 'app-test-sound',
  templateUrl: './test-sound.component.html',

})
export class TestSoundComponent implements OnInit {
  lineChartData: ChartDataSets[] = [];
  lineChartLabels: Label[];

  @Input() sound:string;

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
    console.log(this.sound)
    this.activatedRoute.params.subscribe(params => {
      this.getTestById(params["testId"]);
    });
  }

  getTestById(testId) {
    this.testService.getTestById(testId).subscribe(data => {
      data = data["data"];
      let sound = data[this.sound]

      this.lineChartLabels = this.getAllRosTimes(sound)
      this.lineChartData = this.getDatas(sound)
    });
  }
  getDatas(data) {
    let dataSet : ChartDataSets[] = [];
    let datas : number[] = [];
    
    for (let i of data) {
      let data = i["data"]
      data = data.replace('[','').replace(']','');
      data = data.split(',').map(Number);

      let avgOfData = 0;
      for (let j of data){
        avgOfData += j;
      }
      avgOfData = avgOfData/data.length;
      datas.push(avgOfData)
    }
    dataSet.push({data:datas,label:'Sounds'})

    return dataSet;
  }
  getAllRosTimes(data): Label[] {
    let labels : string[] = [];
    for (let i = 0 ; i < data.length; i++) {
      labels.push(i.toString() + ' s')
    }
    return labels;
  }

}
