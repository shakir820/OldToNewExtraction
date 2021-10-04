import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public deleting: boolean = false;
  public extracting:boolean = false;
  public operaion_result: boolean = false;
  public showAlert: boolean = false;
  constructor(private httpClient: HttpClient){

  }



  async onExtractClicked(){
    this.extracting = true;
    var url = environment.serverUrl + '/api/home/ExtractAll';
    var result = await this.httpClient.get<boolean>(url).toPromise();
    this.extracting = false;
    this.operaion_result = result;
    this.showAlert = true;
    this.hideAlert();

  }



  async onDeleteClicked(){
    this.deleting = true;
    var url = environment.serverUrl + '/api/home/DeleteAllRecord';
    var result = await this.httpClient.get<boolean>(url).toPromise();
    this.deleting = false;
    this.operaion_result = result;
    this.showAlert = true;
    this.hideAlert();
  }


  hideAlert(){
    setTimeout(() => {
      this.showAlert = false;
    }, 3000);
  }

}
