import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderService } from 'src/app/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-cancel-order',
  templateUrl: './modal-cancel-order.component.html',
  styleUrls: ['./modal-cancel-order.component.css'],
  providers:[
    OrderService
  ]
})
export class ModalCancelOrderComponent implements OnInit {

  message: String;

  order: any = {};

  loadingCancelOrder: Boolean = false;

  constructor(public activeModal: NgbActiveModal, private _orderService: OrderService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.message = '¿ Seguro que quiere anular la orden del cliente ' + this.order[0].clientFullName + ' ?';

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }

  cancelOrder(){

    this.loadingCancelOrder = true;

    this._orderService.deleteById(this.order[0].id).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingCancelOrder = false;
        this.dismiss();
        this._toastrService.success(res.message);

      }else if(res.statusCode == 404){

        this.loadingCancelOrder = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingCancelOrder = false;
        this.dismiss();
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingCancelOrder = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingCancelOrder = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
