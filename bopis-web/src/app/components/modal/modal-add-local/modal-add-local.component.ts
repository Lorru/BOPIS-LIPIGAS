import { Component, OnInit } from '@angular/core';
import { LocalService } from 'src/app/services/local.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-add-local',
  templateUrl: './modal-add-local.component.html',
  styleUrls: ['./modal-add-local.component.css'],
  providers:[
    LocalService
  ]
})
export class ModalAddLocalComponent implements OnInit {

  userId: Number;

  local: any = {};

  loadingCreate: Boolean = false;

  constructor(public activeModal: NgbActiveModal, private _localService: LocalService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.userId = JSON.parse(localStorage.getItem('userExisting')).id;
    this.fillLocal();

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }

  fillLocal(){

    this.local.latitude = 0;
    this.local.length = 0;
    this.local.radio = 0;

  }

  create(){

    this.loadingCreate = true;

    this.local.latitude = this.local.latitude == null ? 0 : this.local.latitude;
    this.local.length = this.local.length == null ? 0 : this.local.length;
    this.local.radio = this.local.radio == null ? 0 : this.local.radio;

    this._localService.create(this.userId, this.local.latitude, this.local.length, this.local.name, this.local.radio).subscribe(res => {

      if(res.statusCode == 204){

        this.loadingCreate = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 201){

        this.loadingCreate = false;
        this._toastrService.success(res.message);
        this.dismiss();

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

}
