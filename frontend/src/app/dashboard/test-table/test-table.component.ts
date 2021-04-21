import { Component, AfterViewInit, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Test } from '../../models/test';
import { Router } from '@angular/router';
import { TestService } from '../../services/test.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-test-table',
  templateUrl: './test-table.component.html',
  styleUrls: ['./test-table.component.css']
})
export class TestTableComponent implements AfterViewInit,OnInit {
  displayedColumns: string[] = ['id', 'description', 'user', 'date', 'detailAction','downloadAction', 'deleteAction'];
  dataSource: MatTableDataSource<Test>;
  selection = new SelectionModel<Test>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router, private testService: TestService) {
    
  }
  ngOnInit(): void {
    this.getTests(); 
  }

  getTests() {
    this.testService.getAllTests().subscribe(data=>{
      let tests : Test[] = data["data"]
      this.dataSource = new MatTableDataSource(tests);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }

  ngAfterViewInit() {
    
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getTestDetail(id: string){
    console.log(id);
    this.router.navigateByUrl('dashboard/tests/testDetail/' + id);
  }

  download(fileFormat,id,fileName){
    this.testService.downloadTest(fileFormat,id,fileName)
  }
}

