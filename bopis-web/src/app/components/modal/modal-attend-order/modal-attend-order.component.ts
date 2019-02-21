import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderService } from 'src/app/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-modal-attend-order',
  templateUrl: './modal-attend-order.component.html',
  styleUrls: ['./modal-attend-order.component.css'],
  providers:[
    OrderService
  ]
})
export class ModalAttendOrderComponent implements OnInit {

  userId: Number;

  message: String;

  order: any = {};

  loadingAttendOrder: Boolean = false;

  constructor(public activeModal: NgbActiveModal, private _orderService: OrderService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.userId = JSON.parse(localStorage.getItem('userExisting')).id;
    this.message = '¿ Seguro que quiere atender la orden del cliente ' + this.order[0].clientFullName + ' ?';

  }

  dismiss() {

    this.activeModal.dismiss('Modal Dismiss');

  }


  attendOrder(){

    this.loadingAttendOrder = true;

    this._orderService.updateById(this.order[0].id, this.userId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingAttendOrder = false;
        this.dismiss();
        this._toastrService.success(res.message);

      }else if(res.statusCode == 404){

        this.loadingAttendOrder = false;
        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.loadingAttendOrder = false;
        this.dismiss();
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingAttendOrder = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingAttendOrder = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
