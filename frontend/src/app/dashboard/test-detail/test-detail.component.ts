import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Sound } from '../..//models/sound';
import { Vibration } from '../..//models/vibration';
import { TestService } from '../../services/test.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit,AfterViewInit {
  dataSourceVib1 : MatTableDataSource<Vibration>;
  dataSourceVib2 : MatTableDataSource<Vibration>;

  displayedColumnsVib: string[] = ['Ros Time','Axis','Data'];

  sound1:any;
  sound2:any;
  main:any;
  vib1:any;
  vib2:any;

  @ViewChild(MatPaginator) paginatorVib: MatPaginator;

  constructor(private activatedRoute: ActivatedRoute,private testService:TestService) { }

  ngAfterViewInit() {
 
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getTestById(params["testId"]);
    });
  }

  getTestById(testId) {
    this.testService.getTestById(testId).subscribe(data => {
      data = data["data"]

      this.sound1 = data["sound1"]
      this.sound2 = data["sound2"]
      this.main = data["main"]

      this.dataSourceVib1 = new MatTableDataSource( data["vibration1"]);
      this.dataSourceVib1.paginator = this.paginatorVib;

      this.dataSourceVib2 = new MatTableDataSource( data["vibration2"]);
      this.dataSourceVib2.paginator = this.paginatorVib;
    });
  }

}
