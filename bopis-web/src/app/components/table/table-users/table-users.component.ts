import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'src/environments/environment';
import { ModalChangePasswordUserComponent } from '../../modal/modal-change-password-user/modal-change-password-user.component';
import { ModalEditUserComponent } from '../../modal/modal-edit-user/modal-edit-user.component';
import { ModalAddUserComponent } from '../../modal/modal-add-user/modal-add-user.component';

@Component({
  selector: 'app-table-users',
  templateUrl: './table-users.component.html',
  styleUrls: ['./table-users.component.css'],
  providers:[
    UserService
  ]
})
export class TableUsersComponent implements OnInit {

  currentPage: Number = 1;
  countRowsByPage: Number;
  countRows: Number;
  sort: Number = 1;

  loadingFindAll: Boolean = false;

  users: Array<any> = new Array<any>();

  filter: String = null;
  column: String = 'Id';

  sorts: any = {};

  constructor(private _userService: UserService, private _toastrService: ToastrService, private router: Router, private _modalService: NgbModal) { this.countRowsByPage = environment.countRowsByPage; }
  

  ngOnInit() {

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

  returnOptionsOfLocal(profileId: Number){

    if(profileId == 2){

      return 'Si';

    }else {

      return 'No';

    }

  }

  fillSorts(){

    this.sorts.Id = 'fa fa-sort';
    this.sorts.FullName = 'fa fa-sort';
    this.sorts.Run = 'fa fa-sort';
    this.sorts.Email = 'fa fa-sort';
    this.sorts.Profile = 'fa fa-sort';

  }

  findAllPaged(){

    this.loadingFindAll = true;

    this._userService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.users = res.users;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.users = res.users;

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

    this._userService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.users = res.users;
        this.currentPage = res.currentPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.users = res.users;

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

    this._userService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.users = res.users;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.users = res.users;

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

    this._userService.findAllPaged(this.currentPage, this.filter, this.sort, this.column).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindAll = false;
        this.users = res.users;
        this.currentPage = res.currentPage;
        this.countRows = res.countRows;
        this.countRowsByPage = res.countRowsByPage;

      }else if(res.statusCode == 204){

        this.loadingFindAll = false;
        this.users = res.users;

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

    }else if(this.column == "FullName" && this.sort == 1){

      this.fillSorts();
      this.sorts.FullName = 'fa fa-sort-up';

    }else if(this.column == "FullName" && this.sort == 0){

      this.fillSorts();
      this.sorts.FullName = 'fa fa-sort-down';

    }else if(this.column == "Run" && this.sort == 1){

      this.fillSorts();
      this.sorts.Run = 'fa fa-sort-up';

    }else if(this.column == "Run" && this.sort == 0){

      this.fillSorts();
      this.sorts.Run = 'fa fa-sort-down';

    }else if(this.column == "Email" && this.sort == 1){

      this.fillSorts();
      this.sorts.Email = 'fa fa-sort-up';

    }else if(this.column == "Email" && this.sort == 0){

      this.fillSorts();
      this.sorts.Email = 'fa fa-sort-down';

    }else if(this.column == "Profile" && this.sort == 1){

      this.fillSorts();
      this.sorts.Profile = 'fa fa-sort-up';

    }else if(this.column == "Profile" && this.sort == 0){

      this.fillSorts();
      this.sorts.Profile = 'fa fa-sort-down';

    }


  }

  updateStatusFindById(i: number){

    this.users[i].status = this.users[i].status == true ? false : true;

    this._userService.updateStatusFindById(this.users[i].id, this.users[i].status).subscribe(res => {

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

  updatePassword(userId: Number){

    const modalRef = this._modalService.open(ModalChangePasswordUserComponent);
    modalRef.componentInstance.user = this.users.filter(res => res.id == userId);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

  updateUser(userId: Number){

    const modalRef = this._modalService.open(ModalEditUserComponent);
    modalRef.componentInstance.user = this.users.filter(res => res.id == userId);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

  addUser(){

    const modalRef = this._modalService.open(ModalAddUserComponent);
    modalRef.result.then((result) => {

      this.ngOnInit();

    }).catch((error) => {

      this.ngOnInit();

    });

  }

}
