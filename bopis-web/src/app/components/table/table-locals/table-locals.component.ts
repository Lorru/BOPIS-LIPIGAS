import { Component, OnInit } from '@angular/core';
import { LocalService } from 'src/app/services/local.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'src/environments/environment';
import { ModalEditLocalComponent } from '../../modal/modal-edit-local/modal-edit-local.component';
import { ModalAddLocalComponent } from '../../modal/modal-add-local/modal-add-local.component';

@Component({
  selector: 'app-table-locals',
  templateUrl: './table-locals.component.html',
  styleUrls: ['./table-locals.component.css'],
  providers:[
    LocalService
  ]
})
export class TableLocalsComponent implements OnInit {

  userId: Number;
  currentPage: Number = 1;
  countRowsByPage: Number;
  countRows: Number;
  sort: Number = 1;

  loadingFindAll: Boolean = false;

  locals: Array<any> = new Array<any>();

  filter: String = null;
  column: String = 'Id';

  sorts: any = {};

  constructor(private _localService: LocalService, private _toastrService: ToastrService, private router: Router, private _modalService: NgbModal) { this.countRowsByPage = environment.countRowsByPage; }

  ngOnInit() {

    this.userId = JSON.parse(localStorage.getItem('userExisting')).id;
    this.fillSorts();
    this.findAllPaged();

  }

  returnClassSiwtch(status: Boolean){

    if(status){

      return 'switch-true';

    }else{

      return 'switch-false';
    }

  }

  returnClassSmall(status: Boolean){

    if(status){

      return 'small-true';

    }else{
      
      return 'small-false';
    }

  }

  returnStatus(status: Boolean){

    if(status){

      return 'Si';

    }else{

      return 'No';

    }

  }

  fillSorts(){

    this.sorts.Id = 'fa fa-sort';
    this.sorts.Name = 'fa fa-sort';
    this.sorts.Radio = 'fa fa-sort';

  }

  findAllPaged(){

    this.loadingFindAll = true;

    this._localService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.locals = res.locals;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.locals = res.locals;

      }else if(res.statusCode == 403){

        this.loadingFindAll = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAll = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAll = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  pageChange(e){

    this.currentPage = e;
    this.filter = this.filter == '' ? null : this.filter;
    
    this.loadingFindAll = true;

    this._localService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.locals = res.locals;
        this.currentPage = res.currentPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.locals = res.locals;

      }else if(res.statusCode == 403){

        this.loadingFindAll = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAll = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAll = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  search(){

    this.loadingFindAll = true;

    this.currentPage = 1;
    this.filter = this.filter == '' ? null : this.filter;
    this.sort = 1;
    this.column = 'Id';

    this.fillSorts();

    this._localService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.locals = res.locals;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.locals = res.locals;

      }else if(res.statusCode == 403){

        this.loadingFindAll = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAll = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAll = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  orderByAndSort(){

    this.loadingFindAll = true;

    this.currentPage = 1;
    this.filter = this.filter == '' ? null : this.filter;
    this.sort = this.sort == 1 ? 0 : 1;

    this.sortUpAndDown();

    this._localService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.locals = res.locals;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.locals = res.locals;

      }else if(res.statusCode == 403){

        this.loadingFindAll = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindAll = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindAll = false;
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

    }else if(this.column == "Name" && this.sort == 1){

      this.fillSorts();
      this.sorts.Name = 'fa fa-sort-up';

    }else if(this.column == "Name" && this.sort == 0){

      this.fillSorts();
      this.sorts.Name = 'fa fa-sort-down';

    }else if(this.column == "Radio" && this.sort == 1){

      this.fillSorts();
      this.sorts.Radio = 'fa fa-sort-up';

    }else if(this.column == "Radio" && this.sort == 0){

      this.fillSorts();
      this.sorts.Radio = 'fa fa-sort-down';

    }


  }

  updateStatusFindById(i: number){

    this.locals[i].status = this.locals[i].status == true ? false : true;

    this._localService.updateStatusFindById(this.userId, this.locals[i].id, this.locals[i].status).subscribe(res => {

      if(res.statusCode == 200){


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

  updateOrder(localId: Number){

    const modalRef = this._modalService.open(ModalEditLocalComponent, { size: 'lg' });
    modalRef.componentInstance.local = this.locals.filter(res => res.id == localId);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

  addLocal(){

    const modalRef = this._modalService.open(ModalAddLocalComponent, { size: 'lg' });
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

}
