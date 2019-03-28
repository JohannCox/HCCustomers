import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  apiValues: string[];

  constructor(private _service: HttpClient) { }

  //ngOnInit() {
  //  this._service.get("/api/names").subscribe(result => {
  //    this.apiValues = result as string[];
  //  });
  //}


  ngOnInit() {
    this._service.get("/api/customers").subscribe(result => {
      this.apiValues = result as string[];
    });
  }


  title = 'NgHC';
}

interface Customer {
  lName: string;
  fName: string;
  address: string;
  city: string;
  state: string;
  zip: string;
  dob: string;
  interests: string;
  image: object;
}
