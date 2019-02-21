import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LocalService } from 'src/app/services/local.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-edit-local',
  templateUrl: './modal-edit-local.component.html',
  styleUrls: ['./modal-edit-local.component.css'],
  providers:[
    LocalService
  ]
})
export class ModalEditLocalComponent implements OnInit {

  userId: Number;

  local: any = {};

  loadingUpdateFindById: Boolean = false;

  constructor(public activeModal: NgbActiveModal, private _localService: LocalService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.userId = JSON.parse(localStorage.getItem('userExisting')).id;

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }

  updateFindById(){

    this.loadingUpdateFindById = true;

    this.local[0].latitude = this.local[0].latitude == null ? 0 : this.local[0].latitude;
    this.local[0].length = this.local[0].length == null ? 0 : this.local[0].length;
    this.local[0].radio = this.local[0].radio == null ? 0 : this.local[0].radio;

    this._localService.updateByIdAndStatusEqualToOne(this.userId, this.local[0].id, this.local[0].latitude, this.local[0].length, this.local[0].name, this.local[0].radio).subscribe(res => {

      if(res.statusCode == 204){

        this.loadingUpdateFindById = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 200){

        this.loadingUpdateFindById = false;
        this._toastrService.success(res.message);
        this.dismiss();

      }else if(res.statusCode == 404){

        this.loadingUpdateFindById = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingUpdateFindById = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingUpdateFindById = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingUpdateFindById = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
