import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Variable } from '@angular/compiler/src/render3/r3_ast';

@Component({
  selector: 'fetchdata',
  templateUrl: './fetchdata.component.html',
})
export class FetchDataComponent {
  public customers: Customer[];

  constructor(http: HttpClient, @Inject('ORIGIN_URL') originURL: string) {
    http.get(originURL + '/api/customers').subscribe(result => { this.customers = result as Customer[]; })
  }

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
    image : object;
}

