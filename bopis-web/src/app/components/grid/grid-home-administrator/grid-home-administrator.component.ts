import { Component, OnInit } from '@angular/core';
import { LocalService } from 'src/app/services/local.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/services/user.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-grid-home-administrator',
  templateUrl: './grid-home-administrator.component.html',
  styleUrls: ['./grid-home-administrator.component.css'],
  providers:[
    LocalService,
    UserService,
    OrderService
  ]
})
export class GridHomeAdministratorComponent implements OnInit {

  profileId: Number;
  localId: Number;

  loadingLocalFindAllCount: Boolean = false;
  loadingUserFindAllCount: Boolean = false;
  loadingLocalFindByStatusEqualToOne: Boolean = false;
  loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId: Boolean = false;

  localFindAll: Number;
  userFindAll: Number;
  localFindByStatusEqualToOne: Number;
  orderCanceled: Number;
  orderDelivered: Number;
  orderSlopes: Number;

  constructor(private _localService: LocalService, private _userService: UserService, private _orderService: OrderService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.profileId = JSON.parse(localStorage.getItem('userExisting')).profileId;
    this.localId = JSON.parse(localStorage.getItem('userExisting')).localId;

    this.findAllCount();
    this.findAllUserCount();
    this.findByStatusEqualToOneCount();
    this.findByProfileId();

  }

  findAllCount(){

    this.loadingLocalFindAllCount = true;

    this._localService.findAllCount().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingLocalFindAllCount = false;
        this.localFindAll = res.count;

      }else if(res.statusCode == 403){

        this.loadingLocalFindAllCount = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingLocalFindAllCount = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingLocalFindAllCount = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findAllUserCount(){

    this.loadingUserFindAllCount = true;

    this._userService.findAllCount().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingUserFindAllCount = false;
        this.userFindAll = res.count;

      }else if(res.statusCode == 403){

        this.loadingUserFindAllCount = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingUserFindAllCount = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingUserFindAllCount = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByStatusEqualToOneCount(){

    this.loadingLocalFindByStatusEqualToOne = true;

    this._localService.findByStatusEqualToOneCount().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingLocalFindByStatusEqualToOne = false;
        this.localFindByStatusEqualToOne = res.count;

      }else if(res.statusCode == 403){

        this.loadingLocalFindByStatusEqualToOne = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingLocalFindByStatusEqualToOne = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingLocalFindByStatusEqualToOne = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByOrderStatusIdCount(){

    this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = true;

    this._orderService.findByOrderStatusIdCount().subscribe(res => {

      if(res.statusCode == 200){

        this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this.orderCanceled = res.canceled;
        this.orderSlopes = res.slopes;
        this.orderDelivered = res.delivered;

      }else if(res.statusCode == 403){

        this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByOrderStatusIdAndLocalIdCount(){

    this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = true;

    this._orderService.findByOrderStatusIdAndLocalIdCount(this.localId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this.orderCanceled = res.canceled;
        this.orderSlopes = res.slopes;
        this.orderDelivered = res.delivered;

      }else if(res.statusCode == 403){

        this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingOrderFindByOrderStatusIdAndLocalIdOrLocalId = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  findByProfileId(){

    if(this.profileId == 3 && this.localId == null){

      this.findByOrderStatusIdCount();

    }else if(this.profileId == 1 && this.localId != null){

      this.findByOrderStatusIdAndLocalIdCount();

    }else if(this.profileId == 3 && this.localId != null){

      this.findByOrderStatusIdAndLocalIdCount();

    }else if(this.profileId == 1 && this.localId == null){

      this.findByOrderStatusIdCount();

    }

  }

}
