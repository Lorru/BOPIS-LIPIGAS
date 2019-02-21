import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderService } from 'src/app/services/order.service';
import { ConfigurationService } from 'src/app/services/configuration.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ModalDetailOrderComponent } from '../../modal/modal-detail-order/modal-detail-order.component';
import { ModalCancelOrderComponent } from '../../modal/modal-cancel-order/modal-cancel-order.component';
import { ModalAttendOrderComponent } from '../../modal/modal-attend-order/modal-attend-order.component';
import * as moment from 'moment';

@Component({
  selector: 'app-table-searcher',
  templateUrl: './table-searcher.component.html',
  styleUrls: ['./table-searcher.component.css'],
  providers:[
    OrderService,
    ConfigurationService
  ]
})
export class TableSearcherComponent implements OnInit {

  currentPage: Number = 1;
  countRowsByPage: Number;
  countRows: Number;
  sort: Number = 1;
  cancelattionByHours: Number;
  configurationId: Number = 9;
  id: Number;
  idSearch: Number;

  loadingFindByIdAndClientRun: Boolean = false;

  orders: Array<any> = new Array<any>();

  column: String = 'Id';
  clientRun: String;

  sorts: any = {};

  constructor(private _orderService: OrderService, private _configurationService: ConfigurationService, private _toastrService: ToastrService, private router: Router, private _modalService: NgbModal) { this.countRowsByPage = environment.countRowsByPage; }

  ngOnInit() {

    this.fillSorts();
    this.findByIdAndStatusEqualToOne();
  }

  fillSorts(){

    this.sorts.Id = 'fa fa-sort';
    this.sorts.ClientAddress = 'fa fa-sort';
    this.sorts.ClientRun = 'fa fa-sort';
    this.sorts.ClientFullName = 'fa fa-sort';
    this.sorts.DateOfDelivery = 'fa fa-sort';
    this.sorts.ScheduleOfRetirement = 'fa fa-sort';
    this.sorts.DateOfRequest = 'fa fa-sort';

  }

  validateCancelOrder(dateOfRequest: string){

    let dateNow = moment(new Date());
    let dateOfRequestConvert = moment(dateOfRequest);

    let differenceHour = dateNow.diff(dateOfRequestConvert, 'hours');

    if(differenceHour >= this.cancelattionByHours){
      
      return true;

    }else{

      return false;

    }

  }


  pageChange(e){

    this.currentPage = e;
    this.clientRun = this.clientRun == '' ? null : this.clientRun;
    this.idSearch = this.id == null ? 0 : this.id;

    this.loadingFindByIdAndClientRun = true;

    this._orderService.findByIdAndClientRunPaged(this.currentPage, this.idSearch, this.clientRun, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByIdAndClientRun = false;
        this.orders = res.orders;
        this.currentPage = res.currentPage;

      }else if(res.statusCode == 204){

        this.loadingFindByIdAndClientRun = false;
        this.orders = res.orders;

      }else if(res.statusCode == 403){

        this.loadingFindByIdAndClientRun = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindByIdAndClientRun = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByIdAndClientRun = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  search(){

    this.loadingFindByIdAndClientRun = true;

    this.currentPage = 1;
    this.clientRun = this.clientRun == '' ? null : this.clientRun;
    this.idSearch = this.id == null ? 0 : this.id;
    this.sort = 1;
    this.column = 'Id';

    this.fillSorts();

    this._orderService.findByIdAndClientRunPaged(this.currentPage, this.idSearch, this.clientRun, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByIdAndClientRun = false;
        this.orders = res.orders;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindByIdAndClientRun = false;
        this.orders = res.orders;

      }else if(res.statusCode == 403){

        this.loadingFindByIdAndClientRun = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindByIdAndClientRun = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByIdAndClientRun = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  orderByAndSort(){

    this.loadingFindByIdAndClientRun = true;

    this.currentPage = 1;
    this.clientRun = this.clientRun == '' ? null : this.clientRun;
    this.idSearch = this.id == null ? 0 : this.id;
    this.sort = this.sort == 1 ? 0 : 1;

    this.sortUpAndDown();

    this._orderService.findByIdAndClientRunPaged(this.currentPage, this.idSearch, this.clientRun, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByIdAndClientRun = false;
        this.orders = res.orders;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindByIdAndClientRun = false;
        this.orders = res.orders;

      }else if(res.statusCode == 403){

        this.loadingFindByIdAndClientRun = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindByIdAndClientRun = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByIdAndClientRun = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  sortUpAndDown(){


    if(this.column == "Id" && this.sort == 1){

      this.fillSorts();
      this.sorts.Id = 'fa fa-sort-up';

    }else if(this.column == "Id" && this.sort == 0){

      this.fillSorts();
      this.sorts.Id = 'fa fa-sort-down';

    }else if(this.column == "ClientAddress" && this.sort == 1){

      this.fillSorts();
      this.sorts.ClientAddress = 'fa fa-sort-up';

    }else if(this.column == "ClientAddress" && this.sort == 0){

      this.fillSorts();
      this.sorts.ClientAddress = 'fa fa-sort-down';

    }else if(this.column == "ClientRun" && this.sort == 1){

      this.fillSorts();
      this.sorts.ClientRun = 'fa fa-sort-up';

    }else if(this.column == "ClientRun" && this.sort == 0){

      this.fillSorts();
      this.sorts.ClientRun = 'fa fa-sort-down';

    }else if(this.column == "ClientFullName" && this.sort == 1){

      this.fillSorts();
      this.sorts.ClientFullName = 'fa fa-sort-up';

    }else if(this.column == "ClientFullName" && this.sort == 0){

      this.fillSorts();
      this.sorts.ClientFullName = 'fa fa-sort-down';

    }else if(this.column == "DateOfDelivery" && this.sort == 1){

      this.fillSorts();
      this.sorts.DateOfDelivery = 'fa fa-sort-up';

    }else if(this.column == "DateOfDelivery" && this.sort == 0){

      this.fillSorts();
      this.sorts.DateOfDelivery = 'fa fa-sort-down';

    }else if(this.column == "ScheduleOfRetirement" && this.sort == 1){

      this.fillSorts();
      this.sorts.ScheduleOfRetirement = 'fa fa-sort-up';

    }else if(this.column == "ScheduleOfRetirement" && this.sort == 0){

      this.fillSorts();
      this.sorts.ScheduleOfRetirement = 'fa fa-sort-down';

    }else if(this.column == "DateOfRequest" && this.sort == 1){

      this.fillSorts();
      this.sorts.DateOfRequest = 'fa fa-sort-up';

    }else if(this.column == "DateOfRequest" && this.sort == 0){

      this.fillSorts();
      this.sorts.DateOfRequest = 'fa fa-sort-down';

    }


  }


  attendOrder(orderId: Number){

    const modalRef = this._modalService.open(ModalAttendOrderComponent);
    modalRef.componentInstance.order = this.orders.filter(res => res.id == orderId);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

  cancelOrder(orderId: Number){

    const modalRef = this._modalService.open(ModalCancelOrderComponent);
    modalRef.componentInstance.order = this.orders.filter(res => res.id == orderId);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

  detailOrder(orderId: Number){

    const modalRef = this._modalService.open(ModalDetailOrderComponent, { size: 'lg' });
    modalRef.componentInstance.order = this.orders.filter(res => res.id == orderId);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

  findByIdAndStatusEqualToOne(){

    this._configurationService.findByIdAndStatusEqualToOne(this.configurationId).subscribe(res => {

      if(res.statusCode == 200){

        this.cancelattionByHours = parseInt(res.configuration.value);

      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }
}
