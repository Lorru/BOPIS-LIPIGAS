import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-change-password-user',
  templateUrl: './modal-change-password-user.component.html',
  styleUrls: ['./modal-change-password-user.component.css'],
  providers:[
    UserService
  ]
})
export class ModalChangePasswordUserComponent implements OnInit {


  user: any = {};

  loadingUpdatePasswordByIdAndStatusEqualToOne: Boolean = false;

  constructor(public activeModal: NgbActiveModal, private _userService: UserService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.user[0].password = '';

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }

  updatePasswordByIdAndStatusEqualToOne(){

    this.loadingUpdatePasswordByIdAndStatusEqualToOne = true;

    this._userService.updatePasswordByIdAndStatusEqualToOne(this.user[0].id, this.user[0].password).subscribe(res => {

      if(res.statusCode == 204){

        this.loadingUpdatePasswordByIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 200){

        this.loadingUpdatePasswordByIdAndStatusEqualToOne = false;
        this._toastrService.success(res.message);
        this.dismiss();

      }else if(res.statusCode == 404){

        this.loadingUpdatePasswordByIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingUpdatePasswordByIdAndStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingUpdatePasswordByIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingUpdatePasswordByIdAndStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
