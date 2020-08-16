import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(): void {
    this.httpClient.get(this.baseUrl + 'products/45').subscribe(
      response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get500Error(): void {
    this.httpClient.get(this.baseUrl + 'buggy/servererror').subscribe(
      response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400Error(): void {
    this.httpClient.get(this.baseUrl + 'buggy/badrequest').subscribe(
      response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400ValidationError(): void {
    this.httpClient.get(this.baseUrl + 'products/five').subscribe(
      response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

}
