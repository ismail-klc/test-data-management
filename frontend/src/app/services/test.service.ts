import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import exportFromJSON from 'export-from-json'

@Injectable({
  providedIn: 'root'
})
export class TestService {
  path = environment.API_URL + "tests/";
  constructor(private router: Router, private httpClient: HttpClient) { }

  getAllTests() {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");

    return this.httpClient.get(this.path);
  }

  getTestById(id: string){
    return this.httpClient.get(this.path + id);
  }

  downloadTest(exportType, id: string, fileName: string) {
    this.getTestById(id).subscribe(data => {
      data = data["data"]
      exportFromJSON({ data, fileName, exportType })
    });
  }
}

