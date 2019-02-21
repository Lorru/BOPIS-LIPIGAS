import { Component, OnInit } from '@angular/core';
import { CylinderByLocalService } from 'src/app/services/cylinder-by-local.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-grid-prices',
  templateUrl: './grid-prices.component.html',
  styleUrls: ['./grid-prices.component.css'],
  providers:[
    CylinderByLocalService
  ]
})
export class GridPricesComponent implements OnInit {

  localId: Number;

  nameLocal: String;

  loadingCylinderByLocalId: Boolean = false;

  cylinderByLocals: Array<any> = new Array<any>();

  constructor(private _cylinderByLocalService: CylinderByLocalService, private _toastrService: ToastrService, private router: Router) { }

  ngOnInit() {

    this.localId = JSON.parse(localStorage.getItem('userExisting')).localId;
    this.findByLocalIdAndStatusEqualToOne();

  }

  updateByIdAndStatusEqualToOne(i: number){

    if(this.cylinderByLocals[i].discount == null){

      this.cylinderByLocals[i].discount = 0;

    }

    if(this.cylinderByLocals[i].zonePrice == null){

      this.cylinderByLocals[i].zonePrice = 0;

    }

    this.cylinderByLocals[i].finalPrice =  Math.round((this.cylinderByLocals[i].discount * this.cylinderByLocals[i].zonePrice) / 100);

    this._cylinderByLocalService.updateByIdAndStatusEqualToOne(this.cylinderByLocals[i].id, this.cylinderByLocals[i].zonePrice, this.cylinderByLocals[i].discount, this.cylinderByLocals[i].finalPrice).subscribe(res => {


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

  findByLocalIdAndStatusEqualToOne(){

    this.loadingCylinderByLocalId = true;

    this._cylinderByLocalService.findByLocalIdAndStatusEqualToOne(this.localId).subscribe(res => {

      if(res.statusCode == 200){

        this.loadingCylinderByLocalId = false;
        this.cylinderByLocals = res.cylinderByLocals;
        this.nameLocal = this.cylinderByLocals[0].local.name;

      }else if(res.statusCode == 204){

        this.loadingCylinderByLocalId = false;
        this.cylinderByLocals = res.cylinderByLocals;

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

}
