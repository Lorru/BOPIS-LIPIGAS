import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocalService } from 'src/app/services/local.service';
import { ProfileService } from 'src/app/services/profile.service';
import { UserService } from 'src/app/services/user.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-add-user',
  templateUrl: './modal-add-user.component.html',
  styleUrls: ['./modal-add-user.component.css']
})
export class ModalAddUserComponent implements OnInit {

  user: any = {};

  loadingProfileFindAllStatusEqualToOne: Boolean = false;
  loadingLocalFindAllStatusEqualToOne: Boolean = false;
  loadingCreate: Boolean = false;
  local: Boolean = false;

  profiles: Array<any> = new Array<any>();
  locals: Array<any> = new Array<any>();

  constructor(public activeModal: NgbActiveModal, private _userService: UserService, private _profileService: ProfileService, private _localService: LocalService, private _toastrService: ToastrService, private router: Router) { }
  

  ngOnInit() {

    this.profileFindAllStatusEqualToOne();
    this.localFindAllStatusEqualToOne();

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

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

  isLocal(){

    this.local = this.local == true ? false : true;

  }

  profileFindAllStatusEqualToOne(){

    this.loadingProfileFindAllStatusEqualToOne = true;

    this._profileService.findByAllStatusEqualToOne().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingProfileFindAllStatusEqualToOne = false;
        this.profiles = res.profiles;
        this.user.profileId = res.profiles[0].id;

      }else if(res.statusCode == 204){

        this.loadingProfileFindAllStatusEqualToOne = false;
        this.profiles = res.profiles;
        this.user.profileId = res.profiles[0].id;

      }else if(res.statusCode == 403){

        this.loadingProfileFindAllStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingProfileFindAllStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingProfileFindAllStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  localFindAllStatusEqualToOne(){

    this.loadingLocalFindAllStatusEqualToOne = true;

    this._localService.findAllStatusEqualToOne().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.locals = res.locals;
        this.user.localId = res.locals[0].id;

      }else if(res.statusCode == 204){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.locals = res.locals;
        this.user.localId = res.locals[0].id;

      }else if(res.statusCode == 403){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

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

  create(){

    this.loadingCreate = true;

    if(this.user.profileId == 3 && this.local == false){

      this.user.localId = null;

    }

    this._userService.create(this.user.profileId, this.user.localId, this.user.fullName, this.user.run, this.user.email, this.user.password).subscribe(res => {


      if(res.statusCode == 204){

        this.loadingCreate = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 201){

        this.loadingCreate = false;
        this._toastrService.success(res.message);
        this.dismiss();

      }else if(res.statusCode == 409){

        this.loadingCreate = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 404){

        this.loadingCreate = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingCreate = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingCreate = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingCreate = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  changeLocalIdByProfileId(){

    if(this.user.profileId == 3 && this.local == false){

      this.user.localId = null;

    }else if(this.user.profileId != 3 && this.user.localId == null){

      this.user.localId = this.locals[0].id;

    }else if(this.user.profileId == 3 && this.local == true && this.user.localId == null){

      this.user.localId = this.locals[0].id;

    }

  }

}
