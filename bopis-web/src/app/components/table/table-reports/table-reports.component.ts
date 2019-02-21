import { Component, OnInit } from '@angular/core';
import { LocalService } from 'src/app/services/local.service';
import { UserService } from 'src/app/services/user.service';
import { ReportService } from 'src/app/services/report.service';
import { OrderStatusService } from 'src/app/services/order-status.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-table-reports',
  templateUrl: './table-reports.component.html',
  styleUrls: ['./table-reports.component.css'],
  providers:[
    LocalService,
    UserService,
    ReportService,
    OrderStatusService
  ]
})
export class TableReportsComponent implements OnInit {

  loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId: Boolean = false;
  loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel: Boolean = false;
  loadingLocalFindAllStatusEqualToOne: Boolean = false;
  loadingUserFindAllStatusEqualToOne: Boolean = false;
  loadingOrderStatusFindAllStatusEqualToOne: Boolean = false;

  orders: Array<any> = new Array<any>();
  locals: Array<any> = new Array<any>();
  users: Array<any> = new Array<any>();
  orderStatuses: Array<any> = new Array<any>();


  startDate: String;
  finishDate: String;
  localId: String;
  userId: String;
  orderStatusId: String;

  constructor(private _localService: LocalService, private _userService: UserService, private _reportService: ReportService, private _orderStatusService: OrderStatusService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.localId = 'Todos';
    this.userId = 'Todos';
    this.orderStatusId = 'Todos';

    this.fillDate();

    this.localFindAllStatusEqualToOne();
    this.userFindAllStatusEqualToOne();
    this.orderStatusFindAllStatusEqualToOne();

  }

  fillDate(){

    let year: string = new Date().getFullYear().toString();
    let month: string = (new Date().getMonth() + 1).toString().length == 1 ? '0' + (new Date().getMonth() + 1).toString() : (new Date().getMonth() + 1).toString();
    let day: String = new Date().getDate().toString().length == 1 ? '0' + new Date().getDate().toString() : new Date().getDate().toString();

    let dateNow = year + '-' + month + '-' + day;

    this.startDate = dateNow;
    this.finishDate = dateNow; 

  }

  localFindAllStatusEqualToOne(){

    this.loadingLocalFindAllStatusEqualToOne = true;

    this._localService.findAllStatusEqualToOne().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.locals = res.locals;

      }else if(res.statusCode == 204){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.locals = res.locals;

      }else if(res.statusCode == 403){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingLocalFindAllStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  userFindAllStatusEqualToOne(){

    this.loadingUserFindAllStatusEqualToOne = true;

    this._userService.findAllStatusEqualToOne().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingUserFindAllStatusEqualToOne = false;
        this.users = res.users;

      }else if(res.statusCode == 204){

        this.loadingUserFindAllStatusEqualToOne = false;
        this.users = res.users;

      }else if(res.statusCode == 403){

        this.loadingUserFindAllStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingUserFindAllStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingUserFindAllStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  orderStatusFindAllStatusEqualToOne(){

    this.loadingOrderStatusFindAllStatusEqualToOne = true;

    this._orderStatusService.findAllStatusEqualToOne().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingOrderStatusFindAllStatusEqualToOne = false;
        this.orderStatuses = res.orderStatuses;

      }else if(res.statusCode == 204){

        this.loadingOrderStatusFindAllStatusEqualToOne = false;
        this.orderStatuses = res.orderStatuses;

      }else if(res.statusCode == 403){

        this.loadingOrderStatusFindAllStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingOrderStatusFindAllStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingOrderStatusFindAllStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId(){

    this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId = true;

    this._reportService.findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId(this.startDate, this.finishDate, this.localId, this.userId, this.orderStatusId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId = false;
        this.orders = res.orders;

      }else if(res.statusCode == 204){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId = false;
        this.orders = res.orders;

      }else if(res.statusCode == 403){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel(){

    this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel = true;

    this._reportService.findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel(this.startDate, this.finishDate, this.localId, this.userId, this.orderStatusId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel = false;
        
        let link = document.createElement("a");
        link.href = "data:application/xlsx;base64," + String(res.file);
        link.download = res.fileName + "." + res.dataType;
        link.dispatchEvent(new MouseEvent("click"));

      }else if(res.statusCode == 403){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
