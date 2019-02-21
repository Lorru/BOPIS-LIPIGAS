import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-form-recovery-password',
  templateUrl: './form-recovery-password.component.html',
  styleUrls: ['./form-recovery-password.component.css'],
  providers:[
    UserService
  ]
})
export class FormRecoveryPasswordComponent implements OnInit {

  user: any = {};
  loadingUpdatePasswordByEmailAndStatusEqualToOne: Boolean = false;

  constructor(private _userService: UserService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {
  }

  updatePasswordByEmailAndStatusEqualToOne(){

    this.loadingUpdatePasswordByEmailAndStatusEqualToOne = true;

    this._userService.updatePasswordByEmailAndStatusEqualToOne(this.user.email).subscribe(res => {

      if(res.statusCode == 204){

        this.loadingUpdatePasswordByEmailAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 404){

        this.loadingUpdatePasswordByEmailAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 200){

        this.loadingUpdatePasswordByEmailAndStatusEqualToOne = false;
        this._toastrService.success(res.message);

        this.router.navigate(['/']);

      }else if(res.statusCode == 500){

        this.loadingUpdatePasswordByEmailAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingUpdatePasswordByEmailAndStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }
}
