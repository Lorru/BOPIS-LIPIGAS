import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-grid-home',
  templateUrl: './grid-home.component.html',
  styleUrls: ['./grid-home.component.css'],
  providers:[
    OrderService
  ]
})
export class GridHomeComponent implements OnInit {

  localId: Number;

  loadingFindByOrderStatusIdAndLocalIdOrLocalId: Boolean = false;

  canceled: Number;
  delivered: Number;
  slopes: Number;

  constructor(private _orderService: OrderService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.localId = JSON.parse(localStorage.getItem('userExist')).localId;
    this.findByOrderStatusIdAndLocalIdCount();

  }

  findByOrderStatusIdAndLocalIdCount(){

    this.loadingFindByOrderStatusIdAndLocalIdOrLocalId = true;

    this._orderService.findByOrderStatusIdAndLocalIdCount(this.localId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this.canceled = res.canceled;
        this.slopes = res.slopes;
        this.delivered = res.delivered;

      }else if(res.statusCode == 403){

        this.loadingFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingFindByOrderStatusIdAndLocalIdOrLocalId = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

}
