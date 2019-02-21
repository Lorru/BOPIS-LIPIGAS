import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CylinderByLocalService } from 'src/app/services/cylinder-by-local.service';
import { environment } from 'src/environments/environment';
import { StockService } from 'src/app/services/stock.service';

@Component({
  selector: 'app-modal-add-order',
  templateUrl: './modal-add-order.component.html',
  styleUrls: ['./modal-add-order.component.css'],
  providers:[
    OrderService,
    CylinderByLocalService,
    StockService
  ]
})
export class ModalAddOrderComponent implements OnInit {

  localId: Number;
  userId: Number;

  order: any = {};

  cylinderByLocals: Array<any> = new Array<any>();

  loadingCreate: Boolean = false;
  loadingCylinderByLocalId: Boolean = false;
  loadingFindByCylinderByLocalIdAndStatusEqualToOne: Boolean = false;

  constructor(public activeModal: NgbActiveModal, private _orderService: OrderService, private _cylinderByLocalService: CylinderByLocalService, private _stockService: StockService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.localId = JSON.parse(localStorage.getItem('userExisting')).localId;
    this.userId = JSON.parse(localStorage.getItem('userExisting')).id;
    this.findByLocalIdAndStatusEqualToOne();

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }

  create(){

    this.loadingCreate = true;

    this._orderService.create(this.order.clientAddress, this.order.clientFullName, this.order.clientRun, this.order.cylinderByLocalId, this.order.quantity, this.userId).subscribe(res => {

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

  findByLocalIdAndStatusEqualToOne(){

    this.loadingCylinderByLocalId = true;

    this._cylinderByLocalService.findByLocalIdAndStatusEqualToOne(this.localId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingCylinderByLocalId = false;
        this.cylinderByLocals = res.cylinderByLocals;
        this.order.cylinderByLocalId = res.cylinderByLocals[0].id;
        this.findByCylinderByLocalIdAndStatusEqualToOne();

      }else if(res.statusCode == 204){

        this.loadingCylinderByLocalId = false;
        this.cylinderByLocals = res.cylinderByLocals;

      }else if(res.statusCode == 403){

        this.loadingCylinderByLocalId = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingCylinderByLocalId = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingCylinderByLocalId = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByCylinderByLocalIdAndStatusEqualToOne(){

    this.loadingFindByCylinderByLocalIdAndStatusEqualToOne = true;

    this._stockService.findByCylinderByLocalIdAndStatusEqualToOne(this.order.cylinderByLocalId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByCylinderByLocalIdAndStatusEqualToOne = false;
        this.order.quantity = res.stock.quantity;
        this.order.stockQuantity = res.stock.quantity;

      }else if(res.statusCode == 404){

        this.loadingFindByCylinderByLocalIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingFindByCylinderByLocalIdAndStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();
        this.dismiss();

      }else if(res.statusCode == 500){

        this.loadingFindByCylinderByLocalIdAndStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByCylinderByLocalIdAndStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
