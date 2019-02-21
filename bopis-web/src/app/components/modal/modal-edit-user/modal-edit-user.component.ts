import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocalService } from 'src/app/services/local.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/services/user.service';
import { ProfileService } from 'src/app/services/profile.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-edit-user',
  templateUrl: './modal-edit-user.component.html',
  styleUrls: ['./modal-edit-user.component.css'],
  providers:[
    UserService,
    ProfileService,
    LocalService
  ]
})
export class ModalEditUserComponent implements OnInit {

  user: any = {};

  loadingProfileFindAllStatusEqualToOne: Boolean = false;
  loadingLocalFindAllStatusEqualToOne: Boolean = false;
  loadingUpdatebyIdAndStatusEqualToOne: Boolean = false;
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

      }else if(res.statusCode == 204){

        this.loadingProfileFindAllStatusEqualToOne = false;
        this.profiles = res.profiles;

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

      }else if(res.statusCode == 204){

        this.loadingLocalFindAllStatusEqualToOne = false;
        this.locals = res.locals;

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

  updatebyIdAndStatusEqualToOne(){

    this.loadingUpdatebyIdAndStatusEqualToOne = true;

    if(this.user[0].profileId == 3 && this.local == false){

      this.user[0].localId = null;

    }

    this._userService.updatebyIdAndStatusEqualToOne(this.user[0].id, this.user[0].profileId, this.user[0].localId, this.user[0].fullName, this.user[0].run, this.user[0].email).subscribe(res => {


      if(res.statusCode == 204){

        this.loadingUpdatebyIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 200){

        this.loadingUpdatebyIdAndStatusEqualToOne = false;
        this._toastrService.success(res.message);
        this.dismiss();

      }else if(res.statusCode == 404){

        this.loadingUpdatebyIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingUpdatebyIdAndStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingUpdatebyIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingUpdatebyIdAndStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  changeLocalIdByProfileId(){

    if(this.user[0].profileId == 3 && this.local == false){

      this.user[0].localId = null;

    }else if(this.user[0].profileId != 3 && this.user[0].localId == null){

      this.user[0].localId = this.locals[0].id;

    }else if(this.user[0].profileId == 3 && this.local == true && this.user[0].localId == null){

      this.user[0].localId = this.locals[0].id;

    }

  }

}
