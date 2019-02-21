import { Component, OnInit } from '@angular/core';
import { CylinderByLocalService } from 'src/app/services/cylinder-by-local.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { StockService } from 'src/app/services/stock.service';
import { ScheduleOfAttentionService } from 'src/app/services/schedule-of-attention.service';
import { LocalService } from 'src/app/services/local.service';

@Component({
  selector: 'app-grid-options',
  templateUrl: './grid-options.component.html',
  styleUrls: ['./grid-options.component.css'],
  providers:[
    CylinderByLocalService,
    StockService,
    ScheduleOfAttentionService,
    LocalService
  ]
})
export class GridOptionsComponent implements OnInit {

  localId: Number;
  userId: Number;

  nameLocal: String;

  loadingCylinderByLocalId: Boolean = false;
  loadingScheduleOfAttentionByLocalId: Boolean = false;
  openLocal: Boolean;

  stocks: Array<any> = new Array<any>();
  scheduleOfAttentions: Array<any> = new Array<any>();

  constructor(private _localService: LocalService, private _scheduleOfAttentonService: ScheduleOfAttentionService, private _cylinderByLocalService: CylinderByLocalService, private _stockService: StockService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.localId = JSON.parse(localStorage.getItem('userExisting')).localId;
    this.userId = JSON.parse(localStorage.getItem('userExisting')).id;
    this.stockFindByLocalId();
    this.scheduleOfAttentionFindByLocalIdAndStatusEqualToOne();

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

  updateQuantityByIdAndStatusEqualToOne(i: number){

    if(this.stocks[i].quantity <= 0 || this.stocks[i].quantity == null){

      this.stocks[i].cylinderByLocal.status = false;

    }else{

      this.stocks[i].cylinderByLocal.status = true;

    }

    let quantity: Number = this.stocks[i].quantity == null ? 0 : this.stocks[i].quantity;

    this._stockService.updateQuantityByIdAndStatusEqualToOne(this.stocks[i].id, quantity).subscribe(res => {

      if(res.statusCode == 200){


      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }


  updateStatusFindById(i: number){

    this.stocks[i].cylinderByLocal.status = this.stocks[i].cylinderByLocal.status == true ? false : true;

    this._cylinderByLocalService.updateStatusFindById(this.stocks[i].cylinderByLocal.id, this.stocks[i].cylinderByLocal.status).subscribe(res => {

      if(res.statusCode == 200){


      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  stockFindByLocalId(){

    this.loadingCylinderByLocalId = true;

    this._stockService.findByLocalId(this.localId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingCylinderByLocalId = false;
        this.stocks = res.stocks;
        this.nameLocal = this.stocks[0].cylinderByLocal.local.name;
        this.openLocal = this.stocks[0].cylinderByLocal.local.open;

      }else if(res.statusCode == 204){

        this.loadingCylinderByLocalId = false;
        this.stocks = res.stocks;

      }else if(res.statusCode == 403){

        this.loadingCylinderByLocalId = false;
        this.router.navigate(['/']);
        localStorage.clear();

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

  scheduleOfAttentionFindByLocalIdAndStatusEqualToOne(){

    this.loadingScheduleOfAttentionByLocalId = true;

    this._scheduleOfAttentonService.findByLocalIdAndStatusEqualToOne(this.localId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingScheduleOfAttentionByLocalId = false;
        this.scheduleOfAttentions = res.scheduleOfAttentions;

      }else if(res.statusCode == 204){

        this.loadingScheduleOfAttentionByLocalId = false;
        this.scheduleOfAttentions = res.scheduleOfAttentions;

      }else if(res.statusCode == 403){

        this.loadingScheduleOfAttentionByLocalId = false;
        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this.loadingScheduleOfAttentionByLocalId = false;
        this._toastrService.error(res.message);

      }

    }, error =>{

      this.loadingScheduleOfAttentionByLocalId = false;
      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  updateOpenLocalFindByIdAndStatusEqualToOne(){

    this.openLocal = this.openLocal == true ? false : true;

    this._localService.updateOpenFindByIdAndStatusEqualToOne(this.userId, this.localId, this.openLocal).subscribe(res => {

      if(res.statusCode == 200){


      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }

  scheduleOfAttentionUpdateStartAndFinishFindByIdAndStatusEqualToOne(i: number){

    this._scheduleOfAttentonService.updateStartAndFinishFindByIdAndStatusEqualToOne(this.scheduleOfAttentions[i].id, this.scheduleOfAttentions[i].start, this.scheduleOfAttentions[i].finish).subscribe(res => {

      if(res.statusCode == 200){


      }else if(res.statusCode == 404){

        this._toastrService.error(res.message);

      }else if(res.statusCode == 403){

        this.router.navigate(['/']);
        localStorage.clear();

      }else if(res.statusCode == 500){

        this._toastrService.error(res.message);

      }

    }, error =>{

      this._toastrService.error(environment.messageErrorApi);
      console.clear();

    })

  }
}
