import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-form-login',
  templateUrl: './form-login.component.html',
  styleUrls: ['./form-login.component.css'],
  providers:[
    UserService
  ]
})
export class FormLoginComponent implements OnInit {

  user: any = {};
  loadingLogin: Boolean = false;

  constructor(private _userService: UserService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {
  }

  login(){

    this.loadingLogin = true;

    this._userService.login(this.user.run, this.user.password).subscribe(res => {

      if(res.statusCode == 403){

        this.loadingLogin = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 204){

        this.loadingLogin = false;
        this._toastrService.error(res.message);

      }
      else if(res.statusCode == 200){

        this.loadingLogin = false;
        localStorage.setItem('userExisting', JSON.stringify(res.userExisting));
        localStorage.setItem('roles', JSON.stringify(res.roles));
        localStorage.setItem('token', res.token);
        this.router.navigate(['/menu/home']);

      }else if(res.statusCode == 500){

        this.loadingLogin = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingLogin = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }
}
