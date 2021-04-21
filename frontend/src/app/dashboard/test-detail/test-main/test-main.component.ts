import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestService } from '../../../services/test.service';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective, Label } from 'ng2-charts';

@Component({
  selector: 'app-test-main',
  templateUrl: './test-main.component.html'
})
export class TestMainComponent implements OnInit {
  lineChartData: ChartDataSets[] = [];
  lineChartLabels: Label[];

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
  public hideAll(): void {
    let i: number;
    for (i = 0; i < this.chart.datasets.length; i++) {
      this.chart.hideDataset(i, true);
    }
  }


  constructor(private testService: TestService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getTestById(params["testId"]);
    });
  }

  getTestById(testId) {
    this.testService.getTestById(testId).subscribe(data => {
      data = data["data"];
      let main = data["main"]

      this.lineChartLabels = this.getAllRosTimes(main)
      this.lineChartData = this.getDatas(main)
      this.hideAll()
    });
  }
  getDatas(data) {
    let dataSet: ChartDataSets[] = [];
    let robotBasAcisiNum: number[] = [];
    let ortamSicakligiNum: number[] = [];
    let konumXNum: number[] = [];
    let konumYNum: number[] = [];
    let d_KonumXNum: number[] = [];
    let d_KonumYNum: number[] = [];
    let plabKonumXNum: number[] = [];
    let plabKonumYNum: number[] = [];
    let motor1CekimAkimiNum: number[] = [];
    let motor1TekerHiziNum: number[] = [];
    let motor1GucNum: number[] = [];
    let motor1AkuGerilimNum: number[] = [];
    let motor1SicaklikNum: number[] = [];
    let motor1NemNum: number[] = [];
    let motor2CekimAkimiNum: number[] = [];
    let motor2TekerHiziNum: number[] = [];
    let motor2GucNum: number[] = [];
    let motor2AkuGerilimNum: number[] = [];
    let motor2SicaklikNum: number[] = [];
    let motor2NemNum: number[] = [];


    for (let i of data) {
      let robotBasAcisi = i["robotBasAcisi"]
      let ortamSicakligi = i["ortamSicakligi"]
      let konumX = i["konumX"]
      let konumY = i["konumY"]
      let d_KonumX = i["d_KonumX"]
      let d_KonumY = i["d_KonumY"]
      let plabKonumX = i["plabKonumX"]
      let plabKonumY = i["plabKonumY"]
      let motor1CekimAkimi = i["motor1CekimAkimi"]
      let motor1TekerHizi = i["motor1TekerHizi"]
      let motor1Guc = i["motor1Guc"]
      let motor1AkuGerilim = i["motor1AkuGerilim"]
      let motor1Sicaklik = i["motor1Sicaklik"]
      let motor1Nem = i["motor1Nem"]
      let motor2CekimAkimi = i["motor2CekimAkimi"]
      let motor2TekerHizi = i["motor2TekerHizi"]
      let motor2Guc = i["motor2Guc"]
      let motor2AkuGerilim = i["motor2AkuGerilim"]
      let motor2Sicaklik = i["motor2Sicaklik"]
      let motor2Nem = i["motor2Nem"]

      robotBasAcisiNum.push(robotBasAcisi)
      ortamSicakligiNum.push(ortamSicakligi)
      konumXNum.push(konumX)
      konumYNum.push(konumY)
      d_KonumXNum.push(d_KonumX)
      d_KonumYNum.push(d_KonumY)
      plabKonumXNum.push(plabKonumX)
      plabKonumYNum.push(plabKonumY)
      motor1CekimAkimiNum.push(motor1CekimAkimi)
      motor1TekerHiziNum.push(motor1TekerHizi)
      motor1GucNum.push(motor1Guc)
      motor1AkuGerilimNum.push(motor1AkuGerilim)
      motor1SicaklikNum.push(motor1Sicaklik)
      motor1NemNum.push(motor1Nem)
      motor2CekimAkimiNum.push(motor2CekimAkimi)
      motor2TekerHiziNum.push(motor2TekerHizi)
      motor2GucNum.push(motor2Guc)
      motor2AkuGerilimNum.push(motor2AkuGerilim)
      motor2SicaklikNum.push(motor2Sicaklik)
      motor2NemNum.push(motor2Nem)
    }

    dataSet.push({ data: robotBasAcisiNum, label: 'robotBasAcisi' })
    dataSet.push({ data: ortamSicakligiNum, label: 'ortamSicakligi' })
    dataSet.push({ data: konumXNum, label: 'konumX' })
    dataSet.push({ data: konumYNum, label: 'konumY' })
    dataSet.push({ data: d_KonumXNum, label: 'd_KonumX' })
    dataSet.push({ data: d_KonumYNum, label: 'd_KonumY' })
    dataSet.push({ data: plabKonumXNum, label: 'plabKonumX' })
    dataSet.push({ data: plabKonumYNum, label: 'plabKonumY' })
    dataSet.push({ data: motor1CekimAkimiNum, label: 'motor1CekimAkimi' })
    dataSet.push({ data: motor1TekerHiziNum, label: 'motor1TekerHizi' })
    dataSet.push({ data: motor1GucNum, label: 'motor1Guc' })
    dataSet.push({ data: motor1AkuGerilimNum, label: 'motor1AkuGerilim' })
    dataSet.push({ data: motor1SicaklikNum, label: 'motor1Sicaklik' })
    dataSet.push({ data: motor1NemNum, label: 'motor1Nem' })
    dataSet.push({ data: motor2CekimAkimiNum, label: 'motor2CekimAkimi' })
    dataSet.push({ data: motor2TekerHiziNum, label: 'motor2TekerHizi' })
    dataSet.push({ data: motor2GucNum, label: 'motor2Guc' })
    dataSet.push({ data: motor2AkuGerilimNum, label: 'motor2AkuGerilim' })
    dataSet.push({ data: motor2SicaklikNum, label: 'motor2Sicaklik' })
    dataSet.push({ data: motor2NemNum, label: 'motor2Nem' })



    return dataSet;
  }

  getAllRosTimes(data) {
    let labels: string[] = [];
    for (let i = 0; i < data.length; i++) {
      labels.push(i.toString() + ' s')
    }
    return labels;
  }

}
